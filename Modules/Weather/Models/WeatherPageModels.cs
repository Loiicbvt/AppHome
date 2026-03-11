using System;

namespace HomeAppLBO.Modules.Weather.Models
{
    public sealed class HourlyTimelineItem
    {
        public DateTime Time { get; set; }
        public int TemperatureCelsius { get; set; }
        public string WeatherIcon { get; set; }

        public HourlyTimelineItem()
        {
            WeatherIcon = "☀️";
        }
    }

    public sealed class DailyForecastItem
    {
        public DateTime Date { get; set; }
        public int MinTemperatureCelsius { get; set; }
        public int MaxTemperatureCelsius { get; set; }
        public string WeatherIcon { get; set; }

        public DailyForecastItem()
        {
            WeatherIcon = "☀️";
        }
    }
}