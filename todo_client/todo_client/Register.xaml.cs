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
    }

    private async void Register_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void Login_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}