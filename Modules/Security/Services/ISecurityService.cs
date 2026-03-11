using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Security.Models;

namespace HomeAppLBO.Modules.Security.Services
{
    public interface ISecurityService
    {
        Task<IList<SecurityDevice>> GetDevicesAsync();
        Task<IList<SecurityEvent>> GetEventsAsync();
        Task<IList<SecurityCamera>> GetCamerasAsync();
        Task<bool> GetAwayModeAsync();
        Task SetAwayModeAsync(bool enabled);
        Task RefreshAsync();
    }
}