using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class Profile : ContentPage
{
    public Profile()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        NavigationPage.SetHasBackButton(this, false);
    }

    private async void SignOut_OnClicked(object? sender, EventArgs e)
    {
        // Clear all stored preferences
        Preferences.Clear();
        // Clear all stored data in the session manager
        SessionManager.ClearUser();
        // Navigate back to login and set it as the default page
        Application.Current.MainPage = new NavigationPage(new Login());
        Application.Current.Windows.FirstOrDefault().Page = new NavigationPage(new Login());
    }
}