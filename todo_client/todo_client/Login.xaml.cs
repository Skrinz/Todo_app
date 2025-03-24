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
    }

    private async void SignUp_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Register());
    }
}