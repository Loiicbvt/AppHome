using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Shutters.Models;

namespace HomeAppLBO.Modules.Shutters.Services
{
    public sealed class MockShutterService : IShutterService
    {
        private readonly IList<ShutterInfo> shutters;

        public MockShutterService()
        {
            shutters = new List<ShutterInfo>
            {
                new ShutterInfo
                {
                    Id = "shutter_chambre_1",
                    Name = "Volet chambre 1",
                    RoomName = "Chambre 1",
                    Position = 0,
                    IsAvailable = true
                },
                new ShutterInfo
                {
                    Id = "shutter_chambre_2",
                    Name = "Volet chambre 2",
                    RoomName = "Chambre 2",
                    Position = 0,
                    IsAvailable = true
                },
                new ShutterInfo
                {
                    Id = "shutter_chambre_3",
                    Name = "Volet chambre 3",
                    RoomName = "Chambre 3",
                    Position = 0,
                    IsAvailable = true
                },
                new ShutterInfo
                {
                    Id = "shutter_salon",
                    Name = "Volet salon",
                    RoomName = "Salon",
                    Position = 100,
                    IsAvailable = true
                },
                new ShutterInfo
                {
                    Id = "shutter_cuisine",
                    Name = "Volet cuisine",
                    RoomName = "Cuisine",
                    Position = 100,
                    IsAvailable = true
                }
            };
        }

        public Task<IList<ShutterInfo>> GetShuttersAsync()
        {
            return Task.FromResult(shutters);
        }

        public Task OpenAsync(string shutterId)
        {
            ShutterInfo? shutter = FindById(shutterId);
            if (shutter != null)
            {
                shutter.IsMoving = true;
                shutter.Position = 100;
                shutter.IsMoving = false;
            }

            return Task.CompletedTask;
        }

        public Task CloseAsync(string shutterId)
        {
            ShutterInfo? shutter = FindById(shutterId);
            if (shutter != null)
            {
                shutter.IsMoving = true;
                shutter.Position = 0;
                shutter.IsMoving = false;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(string shutterId)
        {
            ShutterInfo? shutter = FindById(shutterId);
            if (shutter != null)
            {
                shutter.IsMoving = false;
            }

            return Task.CompletedTask;
        }

        public Task SetPositionAsync(string shutterId, int percent)
        {
            ShutterInfo? shutter = FindById(shutterId);
            if (shutter != null)
            {
                shutter.IsMoving = true;
                shutter.Position = percent;
                shutter.IsMoving = false;
            }

            return Task.CompletedTask;
        }

        public Task RunScenarioAsync(string scenarioId)
        {
            switch (scenarioId)
            {
                case "morning":
                    SetAll(100);
                    break;

                case "night":
                    SetAll(0);
                    break;

                case "away":
                    SetAll(0);
                    break;

                case "living":
                    SetOnlySalonCuisineOpen();
                    break;
            }

            return Task.CompletedTask;
        }

        private void SetAll(int position)
        {
            foreach (ShutterInfo shutter in shutters)
            {
                shutter.IsMoving = true;
                shutter.Position = position;
                shutter.IsMoving = false;
            }
        }

        private void SetOnlySalonCuisineOpen()
        {
            foreach (ShutterInfo shutter in shutters)
            {
                shutter.IsMoving = true;

                bool isLivingArea =
                    shutter.Id == "shutter_salon" ||
                    shutter.Id == "shutter_cuisine";

                shutter.Position = isLivingArea ? 100 : 0;
                shutter.IsMoving = false;
            }
        }

        private ShutterInfo? FindById(string id)
        {
            return shutters.FirstOrDefault(x => x.Id == id);
        }
    }
}