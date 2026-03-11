using HomeAppLBO.Core;
using HomeAppLBO.Home.Views;
using HomeAppLBO.Modules.Lights;
using HomeAppLBO.Modules.Shutters;
using HomeAppLBO.Modules.Weather;
using HomeAppLBO.Modules.Security;
using HomeAppLBO.Modules.Energy;
using HomeAppLBO.Modules.Organization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HomeAppLBO
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            HomePage homePage = serviceProvider.GetRequiredService<HomePage>();

            IModuleDefinition[] modules = new IModuleDefinition[]
            {
                new WeatherModuleDefinition(),
                new ShuttersModuleDefinition(),
                new LightsModuleDefinition(),
                new SecurityModuleDefinition(),
                new EnergyModuleDefinition(),
                new OrganizationModuleDefinition()
            };

            homePage.SetModules(modules);

            MainPage = new NavigationPage(homePage);
        }
    }
}