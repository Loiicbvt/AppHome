using HomeAppLBO.Core;
using HomeAppLBO.Modules.Lights.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAppLBO.Modules.Lights
{
    public sealed class LightsModuleDefinition : IModuleDefinition
    {
        public string Id => "Lights";
        public string DisplayName => "Lumières";
        public string Icon => "💡";

        public Page CreatePage()
        {
            IServiceProvider services = Application.Current!.Handler!.MauiContext!.Services;
            return services.GetRequiredService<LightsPage>();
        }
    }
}