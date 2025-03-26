using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class Completed : ContentPage
{
    public Completed()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}