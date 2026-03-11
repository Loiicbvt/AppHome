using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Lights.Models;

namespace HomeAppLBO.Modules.Lights.Services
{
    public sealed class MockLightService : ILightService
    {
        private readonly IList<LightInfo> lights;

        public MockLightService()
        {
            lights = new List<LightInfo>
            {
                new LightInfo
                {
                    Id = "light_salon",
                    Name = "Lumière principale",
                    RoomName = "Salon",
                    IsOn = true,
                    Brightness = 70,
                    IsAvailable = true
                },
                new LightInfo
                {
                    Id = "light_cuisine",
                    Name = "Plafonnier",
                    RoomName = "Cuisine",
                    IsOn = true,
                    Brightness = 85,
                    IsAvailable = true
                },
                new LightInfo
                {
                    Id = "light_chambre_1",
                    Name = "Lumière chambre 1",
                    RoomName = "Chambre 1",
                    IsOn = false,
                    Brightness = 0,
                    IsAvailable = true
                },
                new LightInfo
                {
                    Id = "light_chambre_2",
                    Name = "Lumière chambre 2",
                    RoomName = "Chambre 2",
                    IsOn = false,
                    Brightness = 0,
                    IsAvailable = true
                },
                new LightInfo
                {
                    Id = "light_chambre_3",
                    Name = "Lumière chambre 3",
                    RoomName = "Chambre 3",
                    IsOn = false,
                    Brightness = 0,
                    IsAvailable = true
                }
            };
        }

        public Task<IList<LightInfo>> GetLightsAsync()
        {
            return Task.FromResult(lights);
        }

        public Task ToggleAsync(string lightId)
        {
            LightInfo? light = FindById(lightId);

            if (light != null)
            {
                if (light.IsOn)
                {
                    light.IsOn = false;
                    light.Brightness = 0;
                }
                else
                {
                    light.IsOn = true;
                    light.Brightness = light.Brightness <= 0 ? 70 : light.Brightness;
                }
            }

            return Task.CompletedTask;
        }

        public Task TurnOnAsync(string lightId)
        {
            LightInfo? light = FindById(lightId);

            if (light != null)
            {
                light.IsOn = true;

                if (light.Brightness <= 0)
                {
                    light.Brightness = 70;
                }
            }

            return Task.CompletedTask;
        }

        public Task TurnOffAsync(string lightId)
        {
            LightInfo? light = FindById(lightId);

            if (light != null)
            {
                light.IsOn = false;
                light.Brightness = 0;
            }

            return Task.CompletedTask;
        }

        public Task SetBrightnessAsync(string lightId, int brightness)
        {
            LightInfo? light = FindById(lightId);

            if (light != null)
            {
                if (brightness <= 0)
                {
                    light.Brightness = 0;
                    light.IsOn = false;
                }
                else
                {
                    light.Brightness = brightness;
                    light.IsOn = true;
                }
            }

            return Task.CompletedTask;
        }

        public Task RunScenarioAsync(string scenarioId)
        {
            switch (scenarioId)
            {
                case "living_on":
                    SetLivingRoomAndKitchen(true, 75);
                    break;

                case "all_off":
                    foreach (LightInfo light in lights)
                    {
                        light.IsOn = false;
                        light.Brightness = 0;
                    }
                    break;
            }

            return Task.CompletedTask;
        }

        private void SetLivingRoomAndKitchen(bool isOn, int brightness)
        {
            foreach (LightInfo light in lights)
            {
                bool isLivingRoom = light.Id == "light_salon";
                bool isKitchen = light.Id == "light_cuisine";

                if (isLivingRoom || isKitchen)
                {
                    light.IsOn = isOn;
                    light.Brightness = isOn ? brightness : 0;
                }
            }
        }

        private LightInfo? FindById(string id)
        {
            return lights.FirstOrDefault(x => x.Id == id);
        }

        public Task TurnOffAllAsync()
        {
            foreach (LightInfo light in lights)
            {
                light.IsOn = false;
                light.Brightness = 0;
            }

            return Task.CompletedTask;
        }
    }
}