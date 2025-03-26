using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class Register : ContentPage
{
    public Register()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
    
    private async void Register_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync(false);
    }

    private async void Login_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync(false);
    }
}