using HomeAppLBO.Core;
using HomeAppLBO.Home.ViewModels;
using HomeAppLBO.Home.Views;
using HomeAppLBO.Modules.Lights.Services;
using HomeAppLBO.Modules.Lights.ViewModels;
using HomeAppLBO.Modules.Lights.Views;
using HomeAppLBO.Modules.Shutters.Services;
using HomeAppLBO.Modules.Shutters.ViewModels;
using HomeAppLBO.Modules.Shutters.Views;
using HomeAppLBO.Modules.Weather.Services;
using HomeAppLBO.Modules.Weather.ViewModels;
using HomeAppLBO.Modules.Weather.Views;
using HomeAppLBO.Services;
using Microsoft.Extensions.Logging;
using HomeAppLBO.Services.Quotes;
using HomeAppLBO.Modules.Security.Services;
using HomeAppLBO.Modules.Security.ViewModels;
using HomeAppLBO.Modules.Security.Views;
using HomeAppLBO.Modules.Energy.Services;
using HomeAppLBO.Modules.Energy.ViewModels;
using HomeAppLBO.Modules.Energy.Views;
using HomeAppLBO.Modules.Organization.Views;
using HomeAppLBO.Modules.Organization.ViewModels;
using HomeAppLBO.Modules.Organization.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace HomeAppLBO
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            RegisterServices(builder.Services);
            RegisterViewModels(builder.Services);
            RegisterPages(builder.Services);

            return builder.Build();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<INavigationService, MauiNavigationService>();

            services.AddSingleton<IWeatherService, OpenMeteoWeatherService>();

            services.AddSingleton<IShutterService, MockShutterService>();
            services.AddSingleton<ILightService, MockLightService>();
            services.AddSingleton<IQuoteService, QuoteService>();
            services.AddSingleton<ISecurityService, MockSecurityService>();
            services.AddSingleton<IEnergyService, MockEnergyService>();
            services.AddSingleton<IOrganizationService, MockOrganizationService>();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            services.AddTransient<HomeViewModel>();
            services.AddTransient<ShuttersViewModel>();
            services.AddTransient<LightsViewModel>();
            services.AddTransient<WeatherPageViewModel>();
            services.AddTransient<SecurityViewModel>();
            services.AddTransient<EnergyViewModel>();
            services.AddTransient<OrganizationViewModel>();
        }

        private static void RegisterPages(IServiceCollection services)
        {
            services.AddTransient<HomePage>();
            services.AddTransient<ShuttersPage>();
            services.AddTransient<LightsPage>();
            services.AddTransient<WeatherPage>();
            services.AddTransient<SecurityPage>();
            services.AddTransient<EnergyPage>();
            services.AddTransient<OrganizationPage>();
        }
    }
}