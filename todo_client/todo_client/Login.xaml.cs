using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
    }
    
    private async void SignUp_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register(), false);
    }

    private void SignIn_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new NavigationPage(new BottomTabBar()));
        // Application.Current.MainPage = new NavigationPage(new BottomTabBar());
    }
}