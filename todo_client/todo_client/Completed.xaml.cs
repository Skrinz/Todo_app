using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System;

namespace todo_client;

public partial class Completed : ContentPage
{
    private readonly NetworkHelper networkHelper;
    private readonly ApiService _apiService = new ApiService();
    private ObservableCollection<TaskList> tasks = new ObservableCollection<TaskList>();

    public Completed()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);

        networkHelper = new NetworkHelper();
        completedLV.ItemsSource = tasks;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        activityIndicator.IsRunning = true;

        if (networkHelper.HasInternet())
        {
            if (await networkHelper.IsHostReachable())
            {
                await LoadCompletedTasksAsync(); 
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

    private async Task LoadCompletedTasksAsync()
    {
        try
        {
            //get the userId from the session manager
            int userId = SessionManager.CurrentUser.Id;

            var taskList = await _apiService.GetTasksAsync(userId);

            tasks.Clear();

            foreach (var task in taskList.Where(t=>t.completed))
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
        activityIndicator.IsRunning = true;
        await LoadCompletedTasksAsync();
        activityIndicator.IsRunning = false;
        refreshView.IsRefreshing = false;
    }

    private void CompletedLV_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
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
                Navigation.PushAsync(new CompletedEditTask(selectedTask));
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
