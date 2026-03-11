using HomeAppLBO.Modules.Lights.Models;
using HomeAppLBO.Modules.Lights.ViewModels;

namespace HomeAppLBO.Modules.Lights.Views
{
    public partial class LightsPage : ContentPage
    {
        private readonly LightsViewModel viewModel;

        public LightsPage(LightsViewModel vm)
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

        private async void OnLightCardTapped(object sender, EventArgs e)
        {
            Border? border = sender as Border;
            LightInfo? light = border?.BindingContext as LightInfo;

            if (light == null)
            {
                return;
            }

            await border.ScaleTo(0.98, 80);
            await border.ScaleTo(1.0, 80);

            await viewModel.ToggleAsync(light);
        }

        private async void OnTurnOnClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            LightInfo? light = button?.BindingContext as LightInfo;

            if (light == null)
            {
                return;
            }

            await viewModel.TurnOnAsync(light);
        }

        private async void OnTurnOffClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            LightInfo? light = button?.BindingContext as LightInfo;

            if (light == null)
            {
                return;
            }

            await viewModel.TurnOffAsync(light);
        }

        private async void OnDecreaseBrightnessClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            LightInfo? light = button?.BindingContext as LightInfo;

            if (light == null)
            {
                return;
            }

            int value = light.Brightness - 10;
            if (value < 0)
            {
                value = 0;
            }

            await viewModel.SetBrightnessAsync(light, value);
        }

        private async void OnIncreaseBrightnessClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            LightInfo? light = button?.BindingContext as LightInfo;

            if (light == null)
            {
                return;
            }

            int value = light.Brightness + 10;
            if (value > 100)
            {
                value = 100;
            }

            await viewModel.SetBrightnessAsync(light, value);
        }
    }
}