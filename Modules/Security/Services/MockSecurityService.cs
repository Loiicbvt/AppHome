using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Security.Models;

namespace HomeAppLBO.Modules.Security.Services
{
    public sealed class MockSecurityService : ISecurityService
    {
        private readonly IList<SecurityDevice> devices;
        private readonly IList<SecurityEvent> events; 
        private readonly IList<SecurityCamera> cameras;
        private bool awayModeEnabled;

        public MockSecurityService()
        {
            awayModeEnabled = false;

            devices = new List<SecurityDevice>
            {
                new SecurityDevice
                {
                    Id = "front_door",
                    Name = "Porte d’entrée",
                    Location = "Entrée",
                    Type = "Door",
                    StatusText = "Fermée",
                    IsAlert = false,
                    IsOnline = true
                },
                new SecurityDevice
                {
                    Id = "windows",
                    Name = "Fenêtres",
                    Location = "Maison",
                    Type = "Windows",
                    StatusText = "Toutes fermées",
                    IsAlert = false,
                    IsOnline = true
                },
                new SecurityDevice
                {
                    Id = "living_motion",
                    Name = "Mouvement salon",
                    Location = "Salon",
                    Type = "Motion",
                    StatusText = "Aucun mouvement",
                    IsAlert = false,
                    IsOnline = true
                },
                new SecurityDevice
                {
                    Id = "living_camera",
                    Name = "Caméra salon",
                    Location = "Salon",
                    Type = "Camera",
                    StatusText = "En ligne",
                    IsAlert = false,
                    IsOnline = true
                },
            };
            events = new List<SecurityEvent>
            {
                new SecurityEvent
                {
                    Id = "event1",
                    Title = "Sonnette",
                    Description = "Quelqu'un a sonné",
                    Time = DateTime.Now.AddMinutes(-3),
                    Icon = "🔔"
                },
                new SecurityEvent
                {
                    Id = "event2",
                    Title = "Mouvement détecté",
                    Description = "Détection dans le salon",
                    Time = DateTime.Now.AddMinutes(-22),
                    Icon = "🚶"
                },
                new SecurityEvent
                {
                    Id = "event3",
                    Title = "Caméra active",
                    Description = "Caméra salon en ligne",
                    Time = DateTime.Now.AddHours(-1),
                    Icon = "📷"
                }
            };
            cameras = new List<SecurityCamera>
            {
                new SecurityCamera
                {
                    Id = "camera_entrance",
                    Name = "Caméra entrée",
                    Location = "Entrée",
                    StatusText = "En ligne",
                    PreviewText = "Vue sonnette / entrée",
                    IsOnline = true
                },
                new SecurityCamera
                {
                    Id = "camera_living",
                    Name = "Caméra salon",
                    Location = "Salon",
                    StatusText = "En ligne",
                    PreviewText = "Vue salon",
                    IsOnline = true
                }
            };
        }

        public Task<IList<SecurityDevice>> GetDevicesAsync()
        {
            return Task.FromResult(devices);
        }

        public Task<IList<SecurityEvent>> GetEventsAsync()
        {
            return Task.FromResult(events);
        }

        public Task<IList<SecurityCamera>> GetCamerasAsync()
        {
            return Task.FromResult(cameras);
        }

        public Task<bool> GetAwayModeAsync()
        {
            return Task.FromResult(awayModeEnabled);
        }

        public Task SetAwayModeAsync(bool enabled)
        {
            awayModeEnabled = enabled;

            if (enabled)
            {
                UpdateDevice("front_door", "Fermée", false);
                UpdateDevice("windows", "Toutes fermées", false);
                UpdateDevice("living_motion", "Surveillance active", false);
                UpdateDevice("living_camera", "Enregistrement actif", false);
            }
            else
            {
                UpdateDevice("front_door", "Fermée", false);
                UpdateDevice("windows", "Toutes fermées", false);
                UpdateDevice("living_motion", "Aucun mouvement", false);
                UpdateDevice("living_camera", "En ligne", false);
            }

            return Task.CompletedTask;
        }

        public Task RefreshAsync()
        {
            return Task.CompletedTask;
        }

        private void UpdateDevice(string id, string status, bool isAlert)
        {
            foreach (SecurityDevice device in devices)
            {
                if (device.Id == id)
                {
                    device.StatusText = status;
                    device.IsAlert = isAlert;
                    break;
                }
            }
        }
    }
}