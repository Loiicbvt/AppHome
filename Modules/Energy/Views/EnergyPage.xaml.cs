using HomeAppLBO.Modules.Energy.ViewModels;

namespace HomeAppLBO.Modules.Energy.Views
{
    public partial class EnergyPage : ContentPage
    {
        private readonly EnergyViewModel viewModel;

        public EnergyPage(EnergyViewModel vm)
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
    }
}