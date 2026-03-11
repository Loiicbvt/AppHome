using System;

namespace HomeAppLBO.Modules.Weather.Models
{
    public sealed class WeatherDailyForecast
    {
        public DateTime Date { get; set; }
        public float MinTemperatureCelsius { get; set; }
        public float MaxTemperatureCelsius { get; set; }
        public string WeatherIcon { get; set; }

        public WeatherDailyForecast()
        {
            Date = DateTime.Now;
            MinTemperatureCelsius = 0;
            MaxTemperatureCelsius = 0;
            WeatherIcon = "🌡️";
        }
    }
}