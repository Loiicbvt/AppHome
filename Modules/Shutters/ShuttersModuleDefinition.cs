using HomeAppLBO.Core;
using HomeAppLBO.Modules.Shutters.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAppLBO.Modules.Shutters
{
    public sealed class ShuttersModuleDefinition : IModuleDefinition
    {
        public string Id => "Shutters";
        public string DisplayName => "Volets";
        public string Icon => "🪟";

        public Page CreatePage()
        {
            IServiceProvider services = Application.Current!.Handler!.MauiContext!.Services;
            return services.GetRequiredService<ShuttersPage>();
        }
    }
}