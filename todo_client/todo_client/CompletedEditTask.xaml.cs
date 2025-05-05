using System;
using System.Collections.Generic;
using System.Linq;

namespace todo_client;

public partial class CompletedEditTask : ContentPage
{
    private readonly TaskList _taskToEdit;
    private readonly ApiService _apiService = new ApiService();

    public CompletedEditTask(TaskList task)
    {
        InitializeComponent();
        _taskToEdit = task;

        // Populate the form with the task data
        TitleEntry.Text = _taskToEdit.title;
        DescEditor.Text = _taskToEdit.details;
    }

    private async void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void SaveButton_OnClicked(object? sender, EventArgs e)
    {
        string newTitle = TitleEntry.Text?.Trim() ?? "";
        string newDesc = DescEditor.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(newTitle))
        {
            await DisplayAlert("Validation", "Title cannot be empty.", "OK");
            return;
        }

        var updates = new Dictionary<string, object>();

        if (newTitle != _taskToEdit.title)
            updates["title"] = newTitle;

        if (newDesc != _taskToEdit.details)
            updates["details"] = newDesc;

        if (updates.Count == 0)
        {
            await DisplayAlert("No Changes", "Nothing was changed.", "OK");
            return;
        }

        bool success = await _apiService.UpdateTaskAsync(_taskToEdit.id, updates);

        if (success)
        {
            // Update local object 
            _taskToEdit.title = newTitle;
            _taskToEdit.details = newDesc;

            await DisplayAlert("Success", "Task updated.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to update task.", "OK");
        }
    }

    private async void IncompleteButton_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            // Set the task to incomplete in the local model
            _taskToEdit.completed = false;

            // Create an update dictionary for the "completed" field
            var updates = new Dictionary<string, object>
            {
                { "completed", false }  
            };

            bool success = await _apiService.UpdateTaskAsync(_taskToEdit.id, updates);

            if (success)
            {
                await DisplayAlert("Success", "Task marked as incomplete.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                Console.WriteLine("[ERROR] Failed to mark task as incomplete");
                await DisplayAlert("Error", "Failed to mark task as incomplete.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Exception in IncompleteButton_OnClicked: {ex.Message}");
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void DeleteButton_OnClicked(object? sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Delete Task", $"Are you sure you want to delete '{_taskToEdit.title}'?", "Yes", "No");
        if (!confirm) return;

        bool response = await _apiService.DeleteTaskAsync(_taskToEdit.id);

        if (response)
        {
            await DisplayAlert("Deleted", "Task deleted successfully.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to delete task.", "OK");
        }
    }
}