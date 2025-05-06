namespace todo_client;
using Newtonsoft.Json;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Check if the user is logged in
        bool isLoggedIn = Preferences.Get("IsLoggedIn", false);
        Console.WriteLine(isLoggedIn);

        // If the user is logged in, navigate to the home page (BottomTabBar)
        if (isLoggedIn)
        {
            string userJson = Preferences.Get("User", null);
            Console.WriteLine(userJson);
            
            if (!string.IsNullOrEmpty(userJson))
            {
                var currentUser = JsonConvert.DeserializeObject<UserData>(userJson);
                SessionManager.SetUser(currentUser);

                // If user is logged in and data is valid, set home page to BottomTabBar
                MainPage = new NavigationPage(new BottomTabBar());
            }
            else
            {
                // If user data is corrupted or not available, force them to log in again
                MainPage = new NavigationPage(new Login());
            }
        }
        else
        {
            // If not logged in, navigate to the login page
            MainPage = new NavigationPage(new Login());
        }
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(MainPage); 
    }
}