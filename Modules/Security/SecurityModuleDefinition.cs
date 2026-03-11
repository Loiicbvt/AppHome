using HomeAppLBO.Core;
using HomeAppLBO.Modules.Security.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAppLBO.Modules.Security
{
    public sealed class SecurityModuleDefinition : IModuleDefinition
    {
        public string Id => "Security";
        public string DisplayName => "Sécurité";
        public string Icon => "🔒";

        public Page CreatePage()
        {
            IServiceProvider services = Application.Current!.Handler!.MauiContext!.Services;
            return services.GetRequiredService<SecurityPage>();
        }
    }
}