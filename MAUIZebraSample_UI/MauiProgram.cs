using Android.App;
using MAUIZebraSample_UI.Interfaces.Services;
using MAUIZebraSample_UI.Platforms.Android.Services;
using MAUIZebraSample_UI.ViewModels;

namespace MAUIZebraSample_UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();

        builder.ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IBluetoothService, BluetoothService>();
        builder.Services.AddSingleton<IScannerService, ScannerService>();
        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<MainViewModel>();


        return builder.Build();
    }
}
