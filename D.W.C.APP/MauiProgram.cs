using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using D.W.C.APP; 
using Blazored.LocalStorage;
using D.W.C.APP.Shared;
using D.W.C.APP.Service;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;

namespace D.W.C.APP
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
                });

            // Konfiguracja Blazor i usług lokalnych
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            // Dodanie lokalnego przechowywania danych
            builder.Services.AddBlazoredLocalStorage();

            // Konfiguracja systemu autoryzacji
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<MicrosoftAuthService>();
            builder.Services.AddScoped<AuthenticationService>();


            // Rejestracja serwisu GoogleAuthService
            //builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            // Rejestracja klienta HTTP
            builder.Services.AddScoped<HttpClient>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
