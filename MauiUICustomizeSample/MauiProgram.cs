using MauiUICustomizeSample.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MauiUICustomizeSample;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder

            // 起動するアプリクラスの指定
            .UseMauiApp<App>()

            // フォントの登録
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Dependency injection : AppSlell クラスを DI コンテナに登録
        builder.Services.AddTransient<AppShell>();

        // 全ての Label のカスタマイズ
        LabelHandler.Mapper.AppendToMapping(nameof(IView.Background), (handler, view) =>
        {
            if (view is Label)
            {
#if IOS
                handler.PlatformView.BackgroundColor = Colors.MediumSpringGreen.ToPlatform();
#elif ANDROID        
                handler.PlatformView.SetBackgroundColor(Colors.MediumSpringGreen.ToPlatform());
#endif
            }
        });

        // 特定のインスタンスの Button のカスタマイズ
        ButtonHandler.Mapper.AppendToMapping(nameof(IView.Background), (handler, view) =>
        {
            if (view is MyButton)
            {
#if IOS
                handler.PlatformView.BackgroundColor = Colors.LightCoral.ToPlatform();
                handler.PlatformView.SetTitleColor(Colors.White.ToPlatform(), UIKit.UIControlState.Normal);
                handler.PlatformView.Layer.CornerRadius = 7;
#elif ANDROID
                handler.PlatformView.SetBackgroundColor(Colors.LightCoral.ToPlatform());
#endif
            }
        });

        return builder.Build();
    }
}

