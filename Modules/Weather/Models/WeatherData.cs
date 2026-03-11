using System;
using System.Collections.Generic;

namespace HomeAppLBO.Modules.Weather.Models
{
    public sealed class WeatherData
    {
        public WeatherCurrent Current { get; set; }
        public List<WeatherHourlyForecast> Hourly { get; set; }
        public List<WeatherDailyForecast> Daily { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        public WeatherData()
        {
            Current = new WeatherCurrent();
            Hourly = new List<WeatherHourlyForecast>();
            Daily = new List<WeatherDailyForecast>();
            Sunrise = DateTime.Now;
            Sunset = DateTime.Now;
        }
    }
}