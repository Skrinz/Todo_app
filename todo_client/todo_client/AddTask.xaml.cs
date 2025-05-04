using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class AddTask : ContentPage
{
    private readonly ApiService _apiService = new ApiService();
    public AddTask()
    {
        InitializeComponent();
    }

    private async void BackBtn_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void AddButton_OnClicked(object sender, EventArgs e)
    {
        string title = TitleEntry.Text?.Trim();
        string description = DescEditor.Text?.Trim();

        if (string.IsNullOrWhiteSpace(title))
        {
            await DisplayAlert("Validation", "Title is required.", "OK");
            return;
        }

        var newTask = new TaskList
        {
            title = title,
            details = description,
            userId = SessionManager.CurrentUser.Id
        };

        bool result = await _apiService.CreateTaskAsync(newTask);

        if (result)
        {
            await DisplayAlert("Success", "Task added successfully.", "OK");
            await Navigation.PopAsync(); 
        }
        else
        {
            await DisplayAlert("Error", "Failed to add task.", "OK");
        }
    }
}