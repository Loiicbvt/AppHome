using HomeAppLBO.Modules.Security.Models;
using HomeAppLBO.Modules.Security.ViewModels;

namespace HomeAppLBO.Modules.Security.Views
{
    public partial class SecurityPage : ContentPage
    {
        private readonly SecurityViewModel viewModel;

        public SecurityPage(SecurityViewModel vm)
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

        private async void OnAwayModeClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;

            if (button != null)
            {
                await button.ScaleTo(0.98, 80);
                await button.ScaleTo(1.0, 80);
            }

            await viewModel.ToggleAwayModeAsync();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;

            if (button != null)
            {
                await button.ScaleTo(0.98, 80);
                await button.ScaleTo(1.0, 80);
            }

            await viewModel.RefreshAsync();
        }

        private async void OnCameraOpenClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;

            if (button != null)
            {
                await button.ScaleTo(0.98, 80);
                await button.ScaleTo(1.0, 80);
            }

            await DisplayAlert("Caméra", "Ouverture caméra à brancher plus tard.", "OK");
        }

        private async void OnCameraFullscreenClicked(object sender, EventArgs e)
        {
            Button? button = sender as Button;

            if (button != null)
            {
                await button.ScaleTo(0.98, 80);
                await button.ScaleTo(1.0, 80);
            }

            await DisplayAlert("Plein écran", "Affichage plein écran à brancher plus tard.", "OK");
        }
    }
}