using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;

#if ANDROID
    using Android.Content.Res;
    using Android.Graphics;
#endif

namespace todo_client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CursorColor", (handler, view) =>
        {
        #if __ANDROID__
                    handler.PlatformView.TextCursorDrawable.SetTint(Android.Graphics.Color.Black);
        #endif
                });
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}