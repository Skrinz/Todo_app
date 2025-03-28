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

    private void SignOut_OnClicked(object? sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new Login());
    }
}