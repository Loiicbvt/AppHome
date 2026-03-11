using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HomeAppLBO.Core;
using HomeAppLBO.Modules.Energy.Services;
using HomeAppLBO.Modules.Lights.Models;
using HomeAppLBO.Modules.Lights.Services;
using HomeAppLBO.Modules.Organization.Models;
using HomeAppLBO.Modules.Organization.Services;
using HomeAppLBO.Modules.Security.Models;
using HomeAppLBO.Modules.Security.Services;
using HomeAppLBO.Modules.Shutters.Models;
using HomeAppLBO.Modules.Shutters.Services;
using HomeAppLBO.Modules.Weather.Models;
using HomeAppLBO.Modules.Weather.Services;
using HomeAppLBO.Services.Quotes;
using Microsoft.Maui.ApplicationModel;

namespace HomeAppLBO.Home.ViewModels
{
    public sealed class HomeViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService navigationService;
        private readonly IShutterService shutterService;
        private readonly ILightService lightService;
        private readonly ISecurityService securityService;
        private readonly IEnergyService energyService;
        private readonly IOrganizationService organizationService;
        private readonly IQuoteService quoteService;
        private readonly IWeatherService weatherService;

        private IReadOnlyList<IModuleDefinition> modules = Array.Empty<IModuleDefinition>();

        private string greeting = "Bienvenue";
        private string welcomeTitle = "Maison de Loïc et Shannon";
        private string welcomeSubtitle = "Tableau de bord connecté";

        private string quoteText = "Chargement...";
        private string quoteAuthor = string.Empty;

        private string homeWeatherIcon = "☀️";
        private string homeWeatherTemperature = "--°C";
        private string homeWeatherCondition = "Chargement...";
        private string homeWeatherSubtitle = "3 prochaines heures";

        private string shutterSummary = "Chargement...";
        private int openShuttersCount;
        private int closedShuttersCount;

        private string lightsSummary = "Chargement...";
        private int onLightsCount;
        private int totalLightsCount;

        private string securityStatus = "Chargement...";
        private string securitySubtitle = string.Empty;

        private string energyTodayConsumption = "-- kWh";
        private string energyIndoorTemperature = "--°C";
        private string energyHeatPumpStatus = "Chargement...";

        private string organizationEventsSummary = "Chargement...";
        private string organizationShoppingSummary = "Chargement...";

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<HomeWeatherPreviewItem> WeatherPreviewItems { get; }
        public ObservableCollection<FamilyEvent> OrganizationUpcomingEvents { get; }
        public ObservableCollection<HomeQuickActionItem> QuickActions { get; }

        public string Greeting
        {
            get => greeting;
            set
            {
                if (greeting != value)
                {
                    greeting = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WelcomeTitle
        {
            get => welcomeTitle;
            set
            {
                if (welcomeTitle != value)
                {
                    welcomeTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WelcomeSubtitle
        {
            get => welcomeSubtitle;
            set
            {
                if (welcomeSubtitle != value)
                {
                    welcomeSubtitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string QuoteText
        {
            get => quoteText;
            set
            {
                if (quoteText != value)
                {
                    quoteText = value;
                    OnPropertyChanged();
                }
            }
        }

        public string QuoteAuthor
        {
            get => quoteAuthor;
            set
            {
                if (quoteAuthor != value)
                {
                    quoteAuthor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HomeWeatherIcon
        {
            get => homeWeatherIcon;
            set
            {
                if (homeWeatherIcon != value)
                {
                    homeWeatherIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HomeWeatherTemperature
        {
            get => homeWeatherTemperature;
            set
            {
                if (homeWeatherTemperature != value)
                {
                    homeWeatherTemperature = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HomeWeatherCondition
        {
            get => homeWeatherCondition;
            set
            {
                if (homeWeatherCondition != value)
                {
                    homeWeatherCondition = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HomeWeatherSubtitle
        {
            get => homeWeatherSubtitle;
            set
            {
                if (homeWeatherSubtitle != value)
                {
                    homeWeatherSubtitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ShutterSummary
        {
            get => shutterSummary;
            set
            {
                if (shutterSummary != value)
                {
                    shutterSummary = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OpenShuttersCount
        {
            get => openShuttersCount;
            set
            {
                if (openShuttersCount != value)
                {
                    openShuttersCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ClosedShuttersCount
        {
            get => closedShuttersCount;
            set
            {
                if (closedShuttersCount != value)
                {
                    closedShuttersCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LightsSummary
        {
            get => lightsSummary;
            set
            {
                if (lightsSummary != value)
                {
                    lightsSummary = value;
                    OnPropertyChanged();
                }
            }
        }

        public int OnLightsCount
        {
            get => onLightsCount;
            set
            {
                if (onLightsCount != value)
                {
                    onLightsCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TotalLightsCount
        {
            get => totalLightsCount;
            set
            {
                if (totalLightsCount != value)
                {
                    totalLightsCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecurityStatus
        {
            get => securityStatus;
            set
            {
                if (securityStatus != value)
                {
                    securityStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SecuritySubtitle
        {
            get => securitySubtitle;
            set
            {
                if (securitySubtitle != value)
                {
                    securitySubtitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EnergyTodayConsumption
        {
            get => energyTodayConsumption;
            set
            {
                if (energyTodayConsumption != value)
                {
                    energyTodayConsumption = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EnergyIndoorTemperature
        {
            get => energyIndoorTemperature;
            set
            {
                if (energyIndoorTemperature != value)
                {
                    energyIndoorTemperature = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EnergyHeatPumpStatus
        {
            get => energyHeatPumpStatus;
            set
            {
                if (energyHeatPumpStatus != value)
                {
                    energyHeatPumpStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OrganizationEventsSummary
        {
            get => organizationEventsSummary;
            set
            {
                if (organizationEventsSummary != value)
                {
                    organizationEventsSummary = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OrganizationShoppingSummary
        {
            get => organizationShoppingSummary;
            set
            {
                if (organizationShoppingSummary != value)
                {
                    organizationShoppingSummary = value;
                    OnPropertyChanged();
                }
            }
        }

        public HomeViewModel(
            INavigationService navigationService,
            IShutterService shutterService,
            ILightService lightService,
            ISecurityService securityService,
            IEnergyService energyService,
            IOrganizationService organizationService,
            IQuoteService quoteService,
            IWeatherService weatherService)
        {
            this.navigationService = navigationService;
            this.shutterService = shutterService;
            this.lightService = lightService;
            this.securityService = securityService;
            this.energyService = energyService;
            this.organizationService = organizationService;
            this.quoteService = quoteService;
            this.weatherService = weatherService;

            WeatherPreviewItems = new ObservableCollection<HomeWeatherPreviewItem>();
            OrganizationUpcomingEvents = new ObservableCollection<FamilyEvent>();
            QuickActions = new ObservableCollection<HomeQuickActionItem>();

            BuildQuickActions();
            UpdateGreeting();
        }

        public async Task InitializeAsync(IEnumerable<IModuleDefinition> modules)
        {
            this.modules = modules.ToList();

            UpdateGreeting();

            await LoadQuoteAsync();
            await LoadWeatherPreviewAsync();
            await LoadShutterSummaryAsync();
            await LoadLightsSummaryAsync();
            await LoadSecuritySummaryAsync();
            await LoadEnergySummaryAsync();
            await LoadOrganizationSummaryAsync();
        }

        public async Task OpenModuleAsync(string moduleId)
        {
            if (string.IsNullOrWhiteSpace(moduleId))
            {
                return;
            }

            IModuleDefinition? module = modules.FirstOrDefault(x => x.Id == moduleId);

            if (module == null)
            {
                return;
            }

            await navigationService.NavigateToAsync(module.CreatePage());
        }

        private void UpdateGreeting()
        {
            int hour = DateTime.Now.Hour;

            if (hour < 12)
            {
                Greeting = "Bonjour Loïc et Shannon ☀️";
            }
            else if (hour < 18)
            {
                Greeting = "Bon après-midi Loïc et Shannon 🌤";
            }
            else
            {
                Greeting = "Bonsoir Loïc et Shannon 🌙";
            }
        }

        private async Task LoadQuoteAsync()
        {
            try
            {
                QuoteResult result = await quoteService.GetRandomQuoteAsync().ConfigureAwait(false);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    QuoteText = string.IsNullOrWhiteSpace(result.Quote)
                        ? "Chaque jour est une nouvelle occasion d’avancer."
                        : result.Quote;

                    QuoteAuthor = string.IsNullOrWhiteSpace(result.Author)
                        ? "ZenQuotes"
                        : result.Author;
                });
            }
            catch
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    QuoteText = "Chaque jour est une nouvelle occasion d’avancer.";
                    QuoteAuthor = "Maison";
                });
            }
        }

        private void BuildQuickActions()
        {
            QuickActions.Clear();

            QuickActions.Add(new HomeQuickActionItem
            {
                Id = "shutters_open_all",
                Title = "Ouvrir volets",
                Subtitle = "Tout ouvrir",
                Icon = "☀️"
            });

            QuickActions.Add(new HomeQuickActionItem
            {
                Id = "shutters_close_all",
                Title = "Fermer volets",
                Subtitle = "Tout fermer",
                Icon = "🌙"
            });

            QuickActions.Add(new HomeQuickActionItem
            {
                Id = "shutters_living",
                Title = "Mode salon",
                Subtitle = "Salon + cuisine",
                Icon = "🛋️"
            });

            QuickActions.Add(new HomeQuickActionItem
            {
                Id = "lights_off_all",
                Title = "Éteindre",
                Subtitle = "Toutes les lumières",
                Icon = "💡"
            });
            QuickActions.Add(new HomeQuickActionItem
            {
                Id = "away_mode",
                Title = "Absence",
                Subtitle = "Fermer et sécuriser",
                Icon = "🚪"
            });
            QuickActions.Add(new HomeQuickActionItem
            {
                Id = "home_mode",
                Title = "Retour maison",
                Subtitle = "Salon, cuisine, sécurité",
                Icon = "🏠"
            });
        }

        public async Task RunQuickActionAsync(HomeQuickActionItem? action)
        {
            if (action == null)
            {
                return;
            }

            switch (action.Id)
            {
                case "shutters_open_all":
                    await shutterService.RunScenarioAsync("morning").ConfigureAwait(false);
                    await LoadShutterSummaryAsync().ConfigureAwait(false);
                    break;

                case "shutters_close_all":
                    await shutterService.RunScenarioAsync("night").ConfigureAwait(false);
                    await LoadShutterSummaryAsync().ConfigureAwait(false);
                    break;

                case "shutters_living":
                    await shutterService.RunScenarioAsync("living").ConfigureAwait(false);
                    await LoadShutterSummaryAsync().ConfigureAwait(false);
                    break;

                case "lights_off_all":
                    await lightService.TurnOffAllAsync().ConfigureAwait(false);
                    await LoadLightsSummaryAsync().ConfigureAwait(false);
                    break;

                case "away_mode":
                    await shutterService.RunScenarioAsync("away").ConfigureAwait(false);
                    await lightService.TurnOffAllAsync().ConfigureAwait(false);
                    await securityService.SetAwayModeAsync(true).ConfigureAwait(false);

                    await LoadShutterSummaryAsync().ConfigureAwait(false);
                    await LoadLightsSummaryAsync().ConfigureAwait(false);
                    await LoadSecuritySummaryAsync().ConfigureAwait(false);
                    break;

                case "home_mode":
                    await securityService.SetAwayModeAsync(false).ConfigureAwait(false);
                    await shutterService.RunScenarioAsync("living").ConfigureAwait(false);
                    await lightService.RunScenarioAsync("living_on").ConfigureAwait(false);

                    await LoadShutterSummaryAsync().ConfigureAwait(false);
                    await LoadLightsSummaryAsync().ConfigureAwait(false);
                    await LoadSecuritySummaryAsync().ConfigureAwait(false);
                    break;
            }
        }

        private async Task LoadWeatherPreviewAsync()
        {
            try
            {
                double latitude = 48.0723;
                double longitude = 0.1953;

                WeatherData data = await weatherService
                    .GetWeatherAsync(latitude, longitude)
                    .ConfigureAwait(false);

                DateTime now = DateTime.Now;
                int startIndex = data.Hourly.Count - 1;

                for (int i = 0; i < data.Hourly.Count; i++)
                {
                    if (data.Hourly[i].Time >= now)
                    {
                        startIndex = i;
                        break;
                    }
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    HomeWeatherTemperature = $"{data.Current.TemperatureCelsius:0}°C";
                    HomeWeatherCondition = data.Current.Condition;
                    HomeWeatherIcon = ConvertConditionToIcon(data.Current.Condition);
                    HomeWeatherSubtitle = "3 prochaines heures";

                    WeatherPreviewItems.Clear();

                    int endIndex = Math.Min(startIndex + 3, data.Hourly.Count);

                    for (int i = startIndex; i < endIndex; i++)
                    {
                        WeatherHourlyForecast source = data.Hourly[i];

                        WeatherPreviewItems.Add(new HomeWeatherPreviewItem
                        {
                            TimeLabel = source.Time.ToString("HH:mm"),
                            Icon = source.WeatherIcon,
                            Temperature = $"{Math.Round(source.TemperatureCelsius):0}°"
                        });
                    }
                });
            }
            catch
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    HomeWeatherTemperature = "--°C";
                    HomeWeatherCondition = "Météo indisponible";
                    HomeWeatherIcon = "🌤️";
                    HomeWeatherSubtitle = "3 prochaines heures";
                    WeatherPreviewItems.Clear();
                });
            }
        }

        private async Task LoadShutterSummaryAsync()
        {
            IList<ShutterInfo> shutters = await shutterService.GetShuttersAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                OpenShuttersCount = shutters.Count(x => x.Position == 100);
                ClosedShuttersCount = shutters.Count(x => x.Position == 0);

                ShutterSummary = $"{OpenShuttersCount} ouverts • {ClosedShuttersCount} fermés";
            });
        }

        private async Task LoadLightsSummaryAsync()
        {
            IList<LightInfo> lights = await lightService.GetLightsAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TotalLightsCount = lights.Count;
                OnLightsCount = lights.Count(x => x.IsOn);

                LightsSummary = $"{OnLightsCount} / {TotalLightsCount} allumées";
            });
        }

        private async Task LoadSecuritySummaryAsync()
        {
            IList<SecurityDevice> devices = await securityService.GetDevicesAsync().ConfigureAwait(false);
            bool awayMode = await securityService.GetAwayModeAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                bool hasAlert = devices.Any(x => x.IsAlert);

                if (hasAlert)
                {
                    SecurityStatus = "Alerte";
                    SecuritySubtitle = "Vérification nécessaire";
                }
                else if (awayMode)
                {
                    SecurityStatus = "Protégée";
                    SecuritySubtitle = "Mode absence actif";
                }
                else
                {
                    SecurityStatus = "Calme";
                    SecuritySubtitle = "Aucune alerte";
                }
            });
        }

        private async Task LoadEnergySummaryAsync()
        {
            double today = await energyService.GetTodayConsumptionAsync().ConfigureAwait(false);
            double indoorTemp = await energyService.GetIndoorTemperatureAsync().ConfigureAwait(false);
            string heatPump = await energyService.GetHeatPumpStatusAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                EnergyTodayConsumption = $"{today:0.0} kWh";
                EnergyIndoorTemperature = $"{indoorTemp:0.0}°C";
                EnergyHeatPumpStatus = heatPump;
            });
        }

        private async Task LoadOrganizationSummaryAsync()
        {
            IList<FamilyEvent> events = await organizationService.GetEventsAsync().ConfigureAwait(false);
            IList<ShoppingItem> shoppingItems = await organizationService.GetShoppingItemsAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                OrganizationUpcomingEvents.Clear();

                List<FamilyEvent> upcoming = events
                    .Where(x => x.StartDate >= DateTime.Now)
                    .OrderBy(x => x.StartDate)
                    .Take(3)
                    .ToList();

                foreach (FamilyEvent ev in upcoming)
                {
                    OrganizationUpcomingEvents.Add(ev);
                }

                OrganizationEventsSummary = upcoming.Count == 0
                    ? "Aucun événement à venir"
                    : $"{upcoming.Count} événement(s) à venir";

                int remainingShopping = shoppingItems.Count(x => !x.IsChecked);

                OrganizationShoppingSummary = remainingShopping == 0
                    ? "Courses terminées"
                    : $"{remainingShopping} article(s) à acheter";
            });
        }

        private static string ConvertConditionToIcon(string condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
            {
                return "🌡️";
            }

            string text = condition.ToLowerInvariant();

            if (text.Contains("neige")) return "❄️";
            if (text.Contains("pluie")) return "🌧️";
            if (text.Contains("orage")) return "⛈️";
            if (text.Contains("nuage")) return "⛅";
            if (text.Contains("clair")) return "☀️";
            if (text.Contains("brouillard")) return "🌫️";

            return "🌡️";
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public sealed class HomeWeatherPreviewItem
    {
        public string TimeLabel { get; set; } = string.Empty;
        public string Icon { get; set; } = "☀️";
        public string Temperature { get; set; } = "--°";
    }
    public sealed class HomeQuickActionItem
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}