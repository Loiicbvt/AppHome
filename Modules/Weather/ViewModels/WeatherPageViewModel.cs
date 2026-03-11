using HomeAppLBO.Modules.Weather.Models;
using HomeAppLBO.Modules.Weather.Services;
using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HomeAppLBO.Modules.Weather.ViewModels
{
    public sealed class WeatherPageViewModel : INotifyPropertyChanged
    {
        private readonly IWeatherService weatherService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<HourlyTimelineItem> Timeline24h { get; }
        public ObservableCollection<DailyForecastItem> DailyForecasts { get; }

        public string CurrentIcon { get; private set; }
        public string CurrentTemperature { get; private set; }
        public string CurrentCondition { get; private set; }

        public string Humidity { get; private set; }
        public string Wind { get; private set; }
        public string Pressure { get; private set; }

        public string SunriseTime { get; private set; }
        public string SunsetTime { get; private set; }

        public WeatherPageViewModel(IWeatherService service)
        {
            weatherService = service;

            Timeline24h = new ObservableCollection<HourlyTimelineItem>();
            DailyForecasts = new ObservableCollection<DailyForecastItem>();

            CurrentIcon = "☀️";
            CurrentTemperature = "--°C";
            CurrentCondition = "Chargement...";
            Humidity = "--%";
            Wind = "-- km/h";
            Pressure = "-- hPa";
            SunriseTime = "--:--";
            SunsetTime = "--:--";
        }

        public async Task LoadAsync()
        {
            double latitude = 48.0723;
            double longitude = 0.1953;

            WeatherData data = await weatherService
                .GetWeatherAsync(latitude, longitude)
                .ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentTemperature = data.Current.TemperatureCelsius.ToString("0") + "°C";
                CurrentCondition = data.Current.Condition;
                CurrentIcon = ConvertConditionToIcon(data.Current.Condition);

                Humidity = data.Current.HumidityPercent.ToString("0") + "%";
                Wind = data.Current.WindSpeedKmh.ToString("0") + " km/h";
                Pressure = data.Current.PressureHpa.ToString("0") + " hPa";

                SunriseTime = data.Sunrise.ToString("HH:mm");
                SunsetTime = data.Sunset.ToString("HH:mm");

                OnPropertyChanged(nameof(CurrentTemperature));
                OnPropertyChanged(nameof(CurrentCondition));
                OnPropertyChanged(nameof(CurrentIcon));
                OnPropertyChanged(nameof(Humidity));
                OnPropertyChanged(nameof(Wind));
                OnPropertyChanged(nameof(Pressure));
                OnPropertyChanged(nameof(SunriseTime));
                OnPropertyChanged(nameof(SunsetTime));

                Timeline24h.Clear();

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

                int endIndex = Math.Min(startIndex + 12, data.Hourly.Count);

                for (int i = startIndex; i < endIndex; i++)
                {
                    WeatherHourlyForecast source = data.Hourly[i];

                    Timeline24h.Add(new HourlyTimelineItem
                    {
                        Time = source.Time,
                        TemperatureCelsius = (int)Math.Round(source.TemperatureCelsius),
                        WeatherIcon = source.WeatherIcon
                    });
                }

                DailyForecasts.Clear();

                int dailyCount = Math.Min(data.Daily.Count, 7);
                for (int i = 0; i < dailyCount; i++)
                {
                    WeatherDailyForecast source = data.Daily[i];

                    DailyForecasts.Add(new DailyForecastItem
                    {
                        Date = source.Date,
                        MinTemperatureCelsius = (int)Math.Round(source.MinTemperatureCelsius),
                        MaxTemperatureCelsius = (int)Math.Round(source.MaxTemperatureCelsius),
                        WeatherIcon = source.WeatherIcon
                    });
                }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string ConvertConditionToIcon(string condition)
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
    }
}