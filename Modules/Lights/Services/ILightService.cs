using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Lights.Models;

namespace HomeAppLBO.Modules.Lights.Services
{
    public interface ILightService
    {
        Task<IList<LightInfo>> GetLightsAsync();
        Task ToggleAsync(string lightId);
        Task TurnOnAsync(string lightId);
        Task TurnOffAsync(string lightId);
        Task SetBrightnessAsync(string lightId, int brightness);
        Task TurnOffAllAsync();
        Task RunScenarioAsync(string scenarioID);
    }
}