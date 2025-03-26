using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Graphics;
using Android.Views;

namespace todo_client;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ScreenOrientation = ScreenOrientation.Portrait,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        ApplyTransparentStatusBar();
    }
    
    protected override void OnResume()
    {
        base.OnResume();
        ApplyTransparentStatusBar();
    }

    public override void OnWindowFocusChanged(bool hasFocus)
    {
        base.OnWindowFocusChanged(hasFocus);
        if (hasFocus)
        {
            Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                SystemUiFlags.LayoutStable 
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation 
                | SystemUiFlags.ImmersiveSticky 
                | SystemUiFlags.LayoutHideNavigation
            );
        }
    }

    // Transparent Phone Status bar
    private void ApplyTransparentStatusBar()
    {
        // Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        // Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
        //     SystemUiFlags.LayoutStable 
        //     | SystemUiFlags.LayoutFullscreen
        //     | SystemUiFlags.HideNavigation 
        //     | SystemUiFlags.ImmersiveSticky 
        //     | SystemUiFlags.LayoutHideNavigation
        // );
        Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
            SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutStable
        );
    }
}