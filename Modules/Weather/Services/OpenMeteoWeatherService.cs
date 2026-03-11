using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Weather.Models;

namespace HomeAppLBO.Modules.Weather.Services
{
    public sealed class OpenMeteoWeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;

        public OpenMeteoWeatherService()
        {
            httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherAsync(double latitude, double longitude)
        {
            string url =
                "https://api.open-meteo.com/v1/forecast"
                + "?latitude=" + latitude
                + "&longitude=" + longitude
                + "&current=temperature_2m,relative_humidity_2m,pressure_msl,wind_speed_10m,weather_code"
                + "&hourly=temperature_2m,weather_code"
                + "&daily=weather_code,temperature_2m_min,temperature_2m_max,sunrise,sunset"
                + "&timezone=auto"
                + "&forecast_days=7";

            string json = await httpClient.GetStringAsync(url).ConfigureAwait(false);
            return ParseWeatherJson(json);
        }

        private WeatherData ParseWeatherJson(string json)
        {
            WeatherData result = new WeatherData();

            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;

            ParseCurrent(root, result);
            ParseHourly(root, result);
            ParseDaily(root, result);

            return result;
        }

        private void ParseCurrent(JsonElement root, WeatherData result)
        {
            JsonElement current = root.GetProperty("current");

            result.Current.TemperatureCelsius = current.GetProperty("temperature_2m").GetSingle();
            result.Current.HumidityPercent = current.GetProperty("relative_humidity_2m").GetSingle();
            result.Current.PressureHpa = current.GetProperty("pressure_msl").GetSingle();
            result.Current.WindSpeedKmh = current.GetProperty("wind_speed_10m").GetSingle();

            int code = current.GetProperty("weather_code").GetInt32();
            result.Current.Condition = ConvertWeatherCode(code);
        }

        private void ParseHourly(JsonElement root, WeatherData result)
        {
            JsonElement hourly = root.GetProperty("hourly");
            JsonElement times = hourly.GetProperty("time");
            JsonElement temperatures = hourly.GetProperty("temperature_2m");
            JsonElement codes = hourly.GetProperty("weather_code");

            int count = times.GetArrayLength();

            for (int i = 0; i < count; i++)
            {
                WeatherHourlyForecast item = new WeatherHourlyForecast
                {
                    Time = DateTime.Parse(times[i].GetString() ?? string.Empty),
                    TemperatureCelsius = temperatures[i].GetSingle(),
                    WeatherIcon = ConvertWeatherCodeToIcon(codes[i].GetInt32())
                };

                result.Hourly.Add(item);
            }
        }

        private void ParseDaily(JsonElement root, WeatherData result)
        {
            JsonElement daily = root.GetProperty("daily");
            JsonElement times = daily.GetProperty("time");
            JsonElement mins = daily.GetProperty("temperature_2m_min");
            JsonElement maxs = daily.GetProperty("temperature_2m_max");
            JsonElement codes = daily.GetProperty("weather_code");
            JsonElement sunrises = daily.GetProperty("sunrise");
            JsonElement sunsets = daily.GetProperty("sunset");

            int count = times.GetArrayLength();

            for (int i = 0; i < count; i++)
            {
                WeatherDailyForecast item = new WeatherDailyForecast
                {
                    Date = DateTime.Parse(times[i].GetString() ?? string.Empty),
                    MinTemperatureCelsius = mins[i].GetSingle(),
                    MaxTemperatureCelsius = maxs[i].GetSingle(),
                    WeatherIcon = ConvertWeatherCodeToIcon(codes[i].GetInt32())
                };

                result.Daily.Add(item);
            }

            if (sunrises.GetArrayLength() > 0)
            {
                result.Sunrise = DateTime.Parse(sunrises[0].GetString() ?? string.Empty);
            }

            if (sunsets.GetArrayLength() > 0)
            {
                result.Sunset = DateTime.Parse(sunsets[0].GetString() ?? string.Empty);
            }
        }

        private string ConvertWeatherCode(int code)
        {
            if (code == 0) return "Ciel clair";
            if (code <= 3) return "Partiellement nuageux";
            if (code == 45 || code == 48) return "Brouillard";
            if (code >= 51 && code <= 67) return "Pluie";
            if (code >= 71 && code <= 86) return "Neige";
            if (code >= 95) return "Orage";

            return "Météo inconnue";
        }

        private string ConvertWeatherCodeToIcon(int code)
        {
            if (code == 0) return "☀️";
            if (code <= 3) return "⛅";
            if (code == 45 || code == 48) return "🌫️";
            if (code >= 51 && code <= 67) return "🌧️";
            if (code >= 71 && code <= 86) return "❄️";
            if (code >= 95) return "⛈️";

            return "🌡️";
        }
    }
}