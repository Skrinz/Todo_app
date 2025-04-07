using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class AddTask : ContentPage
{
    public AddTask()
    {
        InitializeComponent();
    }

    private async void BackBtn_OnClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}