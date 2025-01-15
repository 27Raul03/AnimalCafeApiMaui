using AppMAUI.Data;
using Plugin.LocalNotification;

namespace AppMAUI
{
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

            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddTransient<ProductPage>();
            builder.Services.AddTransient<AnimalPage>();
            builder.Services.AddTransient<ClientPage>();
            builder.UseLocalNotification();

            return builder.Build();
        }
    }

}