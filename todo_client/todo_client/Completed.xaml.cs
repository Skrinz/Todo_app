using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System;

namespace todo_client;

public partial class Completed : ContentPage
{
    private readonly NetworkHelper networkHelper;
    private readonly ApiService _apiService;
    private ObservableCollection<TaskList> tasks = new ObservableCollection<TaskList>();

    public Completed()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);

        networkHelper = new NetworkHelper();
        _apiService = new ApiService();
        completedLV.ItemsSource = tasks;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (tasks.Count == 0)
        {
            activityIndicator.IsRunning = true;
            await LoadCompletedTasksAsync();
            activityIndicator.IsRunning = false;
        }
    }

    private async Task LoadCompletedTasksAsync()
    {
        if (!networkHelper.HasInternet())
        {
            await DisplayAlert("No Internet", "Please check your internet connection.", "OK");
            return;
        }

        if (!await networkHelper.IsHostReachable())
        {
            await DisplayAlert("Host Unreachable", "Can't reach the server. Try again later.", "OK");
            return;
        }

        try
        {
            var allTasks = await _apiService.GetTasksAsync(SessionManager.CurrentUser.Id); 
            var completedTasks = allTasks.Where(t => t.completed).ToList();

            tasks.Clear();
            foreach (var task in completedTasks)
                tasks.Add(task);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading completed tasks: {ex.Message}");
            await DisplayAlert("Error", "Something went wrong while fetching tasks.", "OK");
        }
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
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            var selectedTask = e.CurrentSelection.FirstOrDefault() as TaskList;

            if (sender is CollectionView collectionView)
                collectionView.SelectedItem = null;
        }
    }
    
    private async void OnDeleteTapped(object sender, EventArgs e)
    {
        if (sender is Image img && img.BindingContext is TaskList task)
        {
            bool confirm = await DisplayAlert("Delete Task", $"Are you sure you want to delete '{task.title}'?", "Yes", "No");
            if (!confirm) return;

            var response = await _apiService.DeleteTaskAsync(task.userId);

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
