using HomeAppLBO.Modules.Organization.Models;
using HomeAppLBO.Modules.Organization.ViewModels;

namespace HomeAppLBO.Modules.Organization.Views
{
    public partial class OrganizationPage : ContentPage
    {
        private readonly OrganizationViewModel viewModel;

        public OrganizationPage(OrganizationViewModel vm)
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
            if (sender is Button button)
            {
                await button.ScaleTo(0.98, 80);
                await button.ScaleTo(1.0, 80);
            }

            await viewModel.RefreshAsync();
        }

        private async void OnAddShoppingItemClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                await button.ScaleTo(0.98, 80);
                await button.ScaleTo(1.0, 80);
            }

            await viewModel.AddShoppingItemAsync();
        }

        private async void OnShoppingItemTapped(object sender, EventArgs e)
        {
            Border? border = sender as Border;
            ShoppingItem? item = border?.BindingContext as ShoppingItem;

            if (border != null)
            {
                await border.ScaleTo(0.98, 80);
                await border.ScaleTo(1.0, 80);
            }

            await viewModel.ToggleShoppingItemAsync(item);
        }
    }
}