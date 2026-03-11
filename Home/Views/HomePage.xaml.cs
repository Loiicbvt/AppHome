using HomeAppLBO.Core;
using HomeAppLBO.Home.ViewModels;

namespace HomeAppLBO.Home.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly HomeViewModel viewModel;
        private IReadOnlyList<IModuleDefinition> modules = Array.Empty<IModuleDefinition>();
        private bool hasAnimated;

        public HomePage(HomeViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            BindingContext = viewModel;

            SizeChanged += OnPageSizeChanged;
        }

        public void SetModules(IEnumerable<IModuleDefinition> moduleDefinitions)
        {
            modules = moduleDefinitions.ToList();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ApplyResponsiveLayout(Width);
            await viewModel.InitializeAsync(modules);

            if (!hasAnimated)
            {
                hasAnimated = true;
                await RunEntranceAnimationsAsync();
            }
        }

        private void OnPageSizeChanged(object? sender, EventArgs e)
        {
            ApplyResponsiveLayout(Width);
        }

        private void ApplyResponsiveLayout(double width)
        {
            if (width <= 0)
            {
                return;
            }

            bool isTabletLayout = width >= 900;

            if (isTabletLayout)
            {
                MainCardsGrid.ColumnDefinitions.Clear();
                MainCardsGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
                MainCardsGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
                MainCardsGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

                MainCardsGrid.RowDefinitions.Clear();
                MainCardsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                MainCardsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

                Grid.SetRow(WeatherCard, 0);
                Grid.SetColumn(WeatherCard, 0);
                Grid.SetColumnSpan(WeatherCard, 1);

                Grid.SetRow(ShuttersCard, 0);
                Grid.SetColumn(ShuttersCard, 1);
                Grid.SetColumnSpan(ShuttersCard, 1);

                Grid.SetRow(EnergyCard, 0);
                Grid.SetColumn(EnergyCard, 2);
                Grid.SetColumnSpan(EnergyCard, 1);

                Grid.SetRow(LightsCard, 1);
                Grid.SetColumn(LightsCard, 0);
                Grid.SetColumnSpan(LightsCard, 1);

                Grid.SetRow(SecurityCard, 1);
                Grid.SetColumn(SecurityCard, 1);
                Grid.SetColumnSpan(SecurityCard, 1);

                Grid.SetRow(OrganizationCard, 1);
                Grid.SetColumn(OrganizationCard, 2);
                Grid.SetColumnSpan(OrganizationCard, 1);
            }
            else
            {
                MainCardsGrid.ColumnDefinitions.Clear();
                MainCardsGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
                MainCardsGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

                MainCardsGrid.RowDefinitions.Clear();
                MainCardsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                MainCardsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                MainCardsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
                MainCardsGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

                Grid.SetRow(WeatherCard, 0);
                Grid.SetColumn(WeatherCard, 0);
                Grid.SetColumnSpan(WeatherCard, 1);

                Grid.SetRow(ShuttersCard, 0);
                Grid.SetColumn(ShuttersCard, 1);
                Grid.SetColumnSpan(ShuttersCard, 1);

                Grid.SetRow(LightsCard, 1);
                Grid.SetColumn(LightsCard, 0);
                Grid.SetColumnSpan(LightsCard, 1);

                Grid.SetRow(SecurityCard, 1);
                Grid.SetColumn(SecurityCard, 1);
                Grid.SetColumnSpan(SecurityCard, 1);

                Grid.SetRow(EnergyCard, 2);
                Grid.SetColumn(EnergyCard, 0);
                Grid.SetColumnSpan(EnergyCard, 2);

                Grid.SetRow(OrganizationCard, 3);
                Grid.SetColumn(OrganizationCard, 0);
                Grid.SetColumnSpan(OrganizationCard, 2);
            }
        }

        private async Task RunEntranceAnimationsAsync()
        {
            VisualElement[] elements =
            {
                HeroCard,
                WeatherCard,
                ShuttersCard,
                LightsCard,
                SecurityCard,
                EnergyCard,
                OrganizationCard
            };

            foreach (VisualElement element in elements)
            {
                _ = element.FadeTo(1, 260, Easing.CubicOut);
                await element.TranslateTo(0, 0, 260, Easing.CubicOut);
                await Task.Delay(40);
            }
        }

        private static async Task PressAnimationAsync(VisualElement element)
        {
            await element.ScaleTo(0.98, 70, Easing.CubicOut);
            await element.ScaleTo(1.0, 90, Easing.CubicIn);
        }

        private async Task AnimateAndOpenAsync(object sender, string moduleId)
        {
            if (sender is Border border)
            {
                await PressAnimationAsync(border);
            }

            await viewModel.OpenModuleAsync(moduleId);
        }

        private async void OnWeatherTapped(object sender, EventArgs e)
        {
            await AnimateAndOpenAsync(sender, "Weather");
        }

        private async void OnShuttersTapped(object sender, EventArgs e)
        {
            await AnimateAndOpenAsync(sender, "Shutters");
        }

        private async void OnLightsTapped(object sender, EventArgs e)
        {
            await AnimateAndOpenAsync(sender, "Lights");
        }

        private async void OnSecurityTapped(object sender, EventArgs e)
        {
            await AnimateAndOpenAsync(sender, "Security");
        }

        private async void OnEnergyTapped(object sender, EventArgs e)
        {
            await AnimateAndOpenAsync(sender, "Energy");
        }

        private async void OnOrganizationTapped(object sender, EventArgs e)
        {
            await AnimateAndOpenAsync(sender, "Organization");
        }
        private async void OnQuickActionTapped(object sender, EventArgs e)
        {
            Border? border = sender as Border;
            HomeQuickActionItem? action = border?.BindingContext as HomeQuickActionItem;

            if (border == null || action == null)
            {
                return;
            }

            await PressAnimationAsync(border);
            await viewModel.RunQuickActionAsync(action);
        }
    }
}