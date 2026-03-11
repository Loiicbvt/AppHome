using System;

namespace HomeAppLBO.Modules.Weather.Models
{
    public sealed class WeatherHourlyForecast
    {
        public DateTime Time { get; set; }
        public float TemperatureCelsius { get; set; }
        public string WeatherIcon { get; set; }

        public WeatherHourlyForecast()
        {
            Time = DateTime.Now;
            TemperatureCelsius = 0;
            WeatherIcon = "🌡️";
        }
    }
}