using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeAppLBO.Modules.Energy.Models;

namespace HomeAppLBO.Modules.Energy.Services
{
    public sealed class MockEnergyService : IEnergyService
    {
        private readonly IList<EnergyDevice> devices;
        private readonly IList<EnergyHistoryPoint> history;

        public MockEnergyService()
        {
            devices = new List<EnergyDevice>
            {
                new EnergyDevice
                {
                    Id = "pac",
                    Name = "Pompe à chaleur",
                    Location = "Local technique",
                    StatusText = "Active",
                    DetailText = "Chauffage en cours • 32°C départ",
                    Icon = "🌡️"
                },
                new EnergyDevice
                {
                    Id = "heating",
                    Name = "Température maison",
                    Location = "Maison",
                    StatusText = "21.5°C",
                    DetailText = "Consigne confortable",
                    Icon = "🏠"
                },
                new EnergyDevice
                {
                    Id = "grid",
                    Name = "Consommation générale",
                    Location = "Maison",
                    StatusText = "2.1 kW",
                    DetailText = "Charge actuelle estimée",
                    Icon = "⚡"
                }
            };

            history = new List<EnergyHistoryPoint>
            {
                new EnergyHistoryPoint { Date = DateTime.Today.AddDays(-6), ValueKwh = 18.4 },
                new EnergyHistoryPoint { Date = DateTime.Today.AddDays(-5), ValueKwh = 21.7 },
                new EnergyHistoryPoint { Date = DateTime.Today.AddDays(-4), ValueKwh = 19.8 },
                new EnergyHistoryPoint { Date = DateTime.Today.AddDays(-3), ValueKwh = 23.1 },
                new EnergyHistoryPoint { Date = DateTime.Today.AddDays(-2), ValueKwh = 20.6 },
                new EnergyHistoryPoint { Date = DateTime.Today.AddDays(-1), ValueKwh = 17.9 },
                new EnergyHistoryPoint { Date = DateTime.Today, ValueKwh = 12.4 }
            };
        }

        public Task<double> GetTodayConsumptionAsync()
        {
            return Task.FromResult(12.4);
        }

        public Task<double> GetWeekConsumptionAsync()
        {
            return Task.FromResult(133.9);
        }

        public Task<double> GetIndoorTemperatureAsync()
        {
            return Task.FromResult(21.5);
        }

        public Task<string> GetHeatPumpStatusAsync()
        {
            return Task.FromResult("PAC active");
        }

        public Task<IList<EnergyDevice>> GetDevicesAsync()
        {
            return Task.FromResult(devices);
        }

        public Task<IList<EnergyHistoryPoint>> GetHistoryAsync()
        {
            return Task.FromResult(history);
        }

        public Task RefreshAsync()
        {
            return Task.CompletedTask;
        }
    }
}