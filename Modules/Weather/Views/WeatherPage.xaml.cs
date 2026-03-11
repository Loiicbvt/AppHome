using HomeAppLBO.Modules.Weather.ViewModels;

namespace HomeAppLBO.Modules.Weather.Views
{
    public partial class WeatherPage : ContentPage
    {
        private readonly WeatherPageViewModel viewModel;

        public WeatherPage(WeatherPageViewModel vm)
        {
            InitializeComponent();

            viewModel = vm;
            BindingContext = viewModel;

            RadarWebView.Source = "https://www.rainviewer.com/map.html?loc=48.07,0.19,8&o=70&c=3&layer=radar&sm=1&sn=1";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadAsync();
        }
    }
}