using HomeAppLBO.Core;
using HomeAppLBO.Modules.Energy.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAppLBO.Modules.Energy
{
    public sealed class EnergyModuleDefinition : IModuleDefinition
    {
        public string Id => "Energy";
        public string DisplayName => "Énergie";
        public string Icon => "⚡";

        public Page CreatePage()
        {
            IServiceProvider services = Application.Current!.Handler!.MauiContext!.Services;
            return services.GetRequiredService<EnergyPage>();
        }
    }
}