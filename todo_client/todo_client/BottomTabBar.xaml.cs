using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_client;

public partial class BottomTabBar : Shell
{
    public BottomTabBar()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}