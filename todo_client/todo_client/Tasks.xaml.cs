using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace todo_client;

public partial class Tasks : ContentPage
{
    private readonly NetworkHelper networkHelper;
    private readonly ObservableCollection<TaskList> tasks = new ObservableCollection<TaskList>();
    private readonly ApiService _apiService = new ApiService();

    public Tasks()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        networkHelper = new NetworkHelper();
        tasksLV.ItemsSource = tasks;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        activityIndicator.IsRunning = true;

        if (networkHelper.HasInternet())
        {
            if (await networkHelper.IsHostReachable())
            {
                await LoadTasksAsync(); 
            }
            else
            {
                await DisplayAlert("Host Unreachable!", "The URL host for ToDo cannot be reached. Please try again later!", "OK");
            }
        }
        else
        {
            await DisplayAlert("No Internet Connection!", "Please check your internet connection and try again!", "OK");
        }

        activityIndicator.IsRunning = false;
    }

    private async Task LoadTasksAsync()
    {
        try
        {
            //get the userId from the session manager
            int userId = SessionManager.CurrentUser.Id;

            var taskList = await _apiService.GetTasksAsync(userId);

            tasks.Clear();

            foreach (var task in taskList)
            {
                tasks.Add(task);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks: {ex.Message}");
            await DisplayAlert("Error", "Failed to load tasks.", "OK");
        }

        activityIndicator.IsRunning = false;
    }
    
    private async void OnRefresh(object sender, EventArgs e)
    {
        // Show a loading indicator
        activityIndicator.IsRunning = true;

        // Refresh the tasks
        await RefreshTasksAsync();

        // Stop the refresh animation
        activityIndicator.IsRunning = false;
        refreshView.IsRefreshing = false;
    }

    private async Task RefreshTasksAsync()
    {
        if (networkHelper.HasInternet())
        {
            if (await networkHelper.IsHostReachable())
            {
                await LoadTasksAsync();
            }
            else
            {
                await DisplayAlert("Host Unreachable!", "The URL host for ToDo cannot be reached. Please try again later!", "OK");
            }
        }
        else
        {
            await DisplayAlert("No Internet Connection!", "Please check your internet connection and try again!", "OK");
        }
    }

    private void Add_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new AddTask());
    }

    private void TasksLV_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        // Only proceed if there is a newly selected item
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            // Optionally get the selected item:
            var selectedTask = e.CurrentSelection.FirstOrDefault() as TaskList;

            // Clear the selection so it doesn't fire again on deselection
            if (sender is CollectionView collectionView)
            {
                collectionView.SelectedItem = null;
            }

            // Navigate to EditTask page
            if (selectedTask != null)
            {
                Navigation.PushAsync(new EditTask(selectedTask));
            }
        }
    }
    
    private async void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkbox && checkbox.BindingContext is TaskList task)
        {
            Console.WriteLine($"[DEBUG] Test Data change: {e.Value}");
            var updates = new Dictionary<string, object>
            {
                { "completed", e.Value }
            };
            Console.WriteLine($"[DEBUG] Task ID: {task.id}");
            Console.WriteLine($"[DEBUG] Patch task: {updates}");
            
            bool success = await _apiService.UpdateTaskAsync(task.id, updates);
            if (!success)
            {
                checkbox.CheckedChanged -= OnCheckBoxChanged;
                await DisplayAlert("Update Failed", "Could not update task completion status.", "OK");
                checkbox.IsChecked = !e.Value; // revert if failed
                checkbox.CheckedChanged += OnCheckBoxChanged;
            }
            else
            {
                task.completed = e.Value; // update local state
                await DisplayAlert("Success", $"Task marked as {(e.Value ? "completed" : "incomplete")}.", "OK");
            }
        }
    }
    
    private async void OnDeleteTapped(object sender, EventArgs e)
    {
        if (sender is Image img && img.BindingContext is TaskList task)
        {
            bool confirm = await DisplayAlert("Delete Task", $"Are you sure you want to delete '{task.title}'?", "Yes", "No");
            if (!confirm) return;

            var response = await _apiService.DeleteTaskAsync(task.id);

            if (response)
            {
                tasks.Remove(task); // remove from observable collection
            }
            else
            {
                await DisplayAlert("Delete Failed", "Failed to delete the task.", "OK");
            }
        }
    }

}
