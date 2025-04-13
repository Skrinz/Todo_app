using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class Register : ContentPage
{
    private readonly ApiService _apiService;
    private readonly NetworkHelper _networkHelper;

    public Register()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);

        _apiService = new ApiService();
        _networkHelper = new NetworkHelper();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void Register_OnClicked(object? sender, EventArgs e)
    {

        try
        {
            var fname = FNameEntry.Text?.Trim();
            var lname = LNameEntry.Text?.Trim();
            var email = EmailEntry.Text?.Trim();
            var password = PasswordEntry.Text;
            var confirmPassword = CPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(fname) ||
                string.IsNullOrWhiteSpace(lname) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                 await DisplayAlert("Error", "Please fill all required fields.", "OK");
                 return;

            }

            if (password != confirmPassword)
            {
                 await DisplayAlert("Error", "Passwords do not match.", "OK");
                 return;
            }

            if (!_networkHelper.HasInternet())
            {
                 await DisplayAlert("No Internet Connection!", "Please check your internet connection and try again!", "OK");
                 return;
            }

            if (!await _networkHelper.IsHostReachable())
            {
                 await DisplayAlert("Host Unreachable!", "The URL host for ToDo cannot be reached. Please try again later!", "OK");
                 return;
            }

            var (success, message) = await _apiService.RegisterUser(fname, lname, email, password);

            if (success)
            {
                await DisplayAlert("Success", message, "OK");
                await Navigation.PopAsync(false);
            }
            else
            {
                await DisplayAlert("Error", message, "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
        }
      
    }

    private async void Login_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync(false);
    }

    private async void TestConnection_OnClicked(object? sender, EventArgs e)
    {

        try
        {
            if (!_networkHelper.HasInternet())
            {
                 await DisplayAlert("No Internet Connection!", "Please check your internet connection and try again!", "OK");
                 return;
            }

            if (await _networkHelper.IsHostReachable())
            {
                await DisplayAlert("Connection Test", "Connection successful!", "OK");
            }
            else
            {
                await DisplayAlert("Connection Test", "Connection failed. Please check server URL and network connection.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Connection Test", $"Connection test error: {ex.Message}", "OK");
        }
    }
}
