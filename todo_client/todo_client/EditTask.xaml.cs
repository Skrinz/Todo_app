using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace todo_client;

public partial class EditTask : ContentPage
{
    private TaskList _taskToEdit;
    private readonly ApiService _apiService = new();

    public EditTask(TaskList task)
    {
        InitializeComponent();
        _taskToEdit = task;

        // Pre-fill the fields
        TitleEntry.Text = _taskToEdit.title;
        DescEditor.Text = _taskToEdit.details;
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
            updates["description"] = newDesc;

        if (updates.Count == 0)
        {
            await DisplayAlert("No Changes", "Nothing was changed.", "OK");
            return;
        }

        bool success = await _apiService.UpdateTaskAsync(_taskToEdit.userId, updates);

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

    private async void CancelButton_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void DeleteButton_OnClicked(object? sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Delete Task", $"Are you sure you want to delete '{_taskToEdit.title}'?", "Yes", "No");
        if (!confirm) return;

        bool response = await _apiService.DeleteTaskAsync(_taskToEdit.userId);

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
    
    private async void CompleteButton_OnClicked(object? sender, EventArgs e)
    {
        // Set the task to completed
        _taskToEdit.completed = true;

        // Create an update dictionary for the "completed" field
        var updates = new Dictionary<string, object>
        {
            { "completed", true }
        };

        bool success = await _apiService.UpdateTaskAsync(_taskToEdit.userId, updates);

        if (success)
        {
            await DisplayAlert("Success", "Task marked as completed.", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to mark task as completed.", "OK");
        }
    }
}
