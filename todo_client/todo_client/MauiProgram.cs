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
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
        #if ANDROID
                    handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);
        #endif
                });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}