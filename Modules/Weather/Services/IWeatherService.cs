using System.Threading.Tasks;
using HomeAppLBO.Modules.Weather.Models;

namespace HomeAppLBO.Modules.Weather.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(double latitude, double longitude);
    }
}