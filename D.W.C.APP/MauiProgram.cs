using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Charts;
using Blazorise.Icons.FontAwesome;
using D.W.C.APP.Shared;
using D.W.C.APP.Service;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

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

            // Configure Blazor and local services
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

          

            builder.Services.AddBlazoredLocalStorage();           
            // Configure authentication system
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<MicrosoftAuthService>();
            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
            builder.Services.AddRadzenComponents();
            // Register HttpClient
            builder.Services.AddScoped<HttpClient>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
