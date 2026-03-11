using HomeAppLBO.Core;
using HomeAppLBO.Modules.Organization.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAppLBO.Modules.Organization
{
    public sealed class OrganizationModuleDefinition : IModuleDefinition
    {
        public string Id => "Organization";
        public string DisplayName => "Organisation";
        public string Icon => "🗓️";

        public Page CreatePage()
        {
            IServiceProvider services = Application.Current!.Handler!.MauiContext!.Services;
            return services.GetRequiredService<OrganizationPage>();
        }
    }
}