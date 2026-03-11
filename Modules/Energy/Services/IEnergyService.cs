using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Energy.Models;

namespace HomeAppLBO.Modules.Energy.Services
{
    public interface IEnergyService
    {
        Task<double> GetTodayConsumptionAsync();
        Task<double> GetWeekConsumptionAsync();
        Task<double> GetIndoorTemperatureAsync();
        Task<string> GetHeatPumpStatusAsync();
        Task<IList<EnergyDevice>> GetDevicesAsync();
        Task<IList<EnergyHistoryPoint>> GetHistoryAsync();
        Task RefreshAsync();
    }
}