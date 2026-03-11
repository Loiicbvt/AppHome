using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Shutters.Models;

namespace HomeAppLBO.Modules.Shutters.Services
{
    public interface IShutterService
    {
        Task<IList<ShutterInfo>> GetShuttersAsync();
        Task OpenAsync(string shutterId);
        Task CloseAsync(string shutterId);
        Task StopAsync(string shutterId);
        Task SetPositionAsync(string shutterId, int percent);
        Task RunScenarioAsync(string scenarioId);
    }
}