using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class Login : ContentPage
{
    private readonly ApiService _apiService;
    private readonly NetworkHelper _networkHelper;
    
    public Login()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
        
        _apiService = new ApiService();
        _networkHelper = new NetworkHelper();
    }
    
    private async void SignUp_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register(), false);
    }

    private async void SignIn_OnClicked(object? sender, EventArgs e)
    {
        
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter both email and password.", "OK");
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

        try
        {
            var (success, message) = await _apiService.LoginUser(email, password);

            if (success)
            {
                await DisplayAlert("Success", "Login successful!", "OK");
                Application.Current.MainPage = new NavigationPage(new BottomTabBar());
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
}