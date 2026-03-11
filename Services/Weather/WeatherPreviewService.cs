using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAppLBO.Services.Weather
{
    public sealed class WeatherPreviewInfo
    {
        public float Temperature { get; set; }
        public string Condition { get; set; }

        public WeatherPreviewInfo()
        {
            Temperature = 0;
            Condition = string.Empty;
        }
    }

    public interface IWeatherPreviewService
    {
        Task<WeatherPreviewInfo> GetCurrentWeatherAsync(double latitude, double longitude);
    }

    public sealed class WeatherPreviewService : IWeatherPreviewService
    {
        private readonly HttpClient client;

        public WeatherPreviewService()
        {
            client = new HttpClient();
        }

        public async Task<WeatherPreviewInfo> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            string url = "https://api.open-meteo.com/v1/forecast"
                         + "?latitude=" + latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)
                         + "&longitude=" + longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)
                         + "&current_weather=true";

            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            JsonDocument doc = JsonDocument.Parse(json);

            WeatherPreviewInfo info = new WeatherPreviewInfo();
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("current_weather", out JsonElement cw))
            {
                info.Temperature = cw.GetProperty("temperature").GetSingle();
                info.Condition = ConvertCodeToText(cw.GetProperty("weathercode").GetInt32());
            }

            return info;
        }

        private string ConvertCodeToText(int code)
        {
            if (code == 0) return "Ciel clair";
            if (code == 1 || code == 2 || code == 3) return "Nuageux";
            if (code >= 45 && code <= 48) return "Brouillard";
            if (code >= 51 && code <= 57) return "Bruine";
            if (code >= 61 && code <= 67) return "Pluie";
            if (code >= 71 && code <= 77) return "Neige";
            if (code >= 80 && code <= 82) return "Averses";
            if (code >= 95) return "Orage";

            return "Inconnu";
        }
    }
}