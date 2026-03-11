using HomeAppLBO.Core;
using HomeAppLBO.Modules.Weather.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAppLBO.Modules.Weather
{
    public sealed class WeatherModuleDefinition : IModuleDefinition
    {
        public string Id => "Weather";
        public string DisplayName => "Météo";
        public string Icon => "☀️";

        public Page CreatePage()
        {
            IServiceProvider services = Application.Current!.Handler!.MauiContext!.Services;
            return services.GetRequiredService<WeatherPage>();
        }
    }
}