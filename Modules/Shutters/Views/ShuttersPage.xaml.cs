using HomeAppLBO.Modules.Shutters.Models;
using HomeAppLBO.Modules.Shutters.ViewModels;

namespace HomeAppLBO.Modules.Shutters.Views
{
    public partial class ShuttersPage : ContentPage
    {
        private readonly ShuttersViewModel viewModel;

        public ShuttersPage(ShuttersViewModel vm)
        {
            InitializeComponent();

            viewModel = vm;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.InitializeAsync();
        }

        private async void OnOpenClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            ShutterInfo? shutter = button?.BindingContext as ShutterInfo;

            if (shutter == null)
            {
                return;
            }

            await viewModel.OpenAsync(shutter);
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            ShutterInfo? shutter = button?.BindingContext as ShutterInfo;

            if (shutter == null)
            {
                return;
            }

            await viewModel.CloseAsync(shutter);
        }

        private async void OnStopClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            ShutterInfo? shutter = button?.BindingContext as ShutterInfo;

            if (shutter == null)
            {
                return;
            }

            await viewModel.StopAsync(shutter);
        }

        private async void OnScenarioClicked(object sender, EventArgs e)
        {
            Border? border = sender as Border;
            ShutterScenarioItem? scenario = border?.BindingContext as ShutterScenarioItem;

            if (scenario == null)
            {
                return;
            }

            await border.ScaleTo(0.98, 80);
            await border.ScaleTo(1.0, 80);

            await viewModel.RunScenarioAsync(scenario);
        }
    }
}