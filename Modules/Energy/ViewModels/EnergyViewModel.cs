using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HomeAppLBO.Modules.Energy.Models;
using HomeAppLBO.Modules.Energy.Services;
using Microsoft.Maui.ApplicationModel;

namespace HomeAppLBO.Modules.Energy.ViewModels
{
    public sealed class EnergyViewModel : INotifyPropertyChanged
    {
        private readonly IEnergyService energyService;

        private string todayConsumption;
        private string weekConsumption;
        private string indoorTemperature;
        private string heatPumpStatus;

        public ObservableCollection<EnergyDevice> Devices { get; }
        public ObservableCollection<EnergyHistoryItem> History { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string TodayConsumption
        {
            get => todayConsumption;
            set
            {
                if (todayConsumption != value)
                {
                    todayConsumption = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WeekConsumption
        {
            get => weekConsumption;
            set
            {
                if (weekConsumption != value)
                {
                    weekConsumption = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IndoorTemperature
        {
            get => indoorTemperature;
            set
            {
                if (indoorTemperature != value)
                {
                    indoorTemperature = value;
                    OnPropertyChanged();
                }
            }
        }

        public string HeatPumpStatus
        {
            get => heatPumpStatus;
            set
            {
                if (heatPumpStatus != value)
                {
                    heatPumpStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public EnergyViewModel(IEnergyService service)
        {
            energyService = service;
            Devices = new ObservableCollection<EnergyDevice>();
            History = new ObservableCollection<EnergyHistoryItem>();

            todayConsumption = "-- kWh";
            weekConsumption = "-- kWh";
            indoorTemperature = "--°C";
            heatPumpStatus = "Chargement...";
        }

        public async Task InitializeAsync()
        {
            await ReloadAsync();
        }

        public async Task RefreshAsync()
        {
            await energyService.RefreshAsync().ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        private async Task ReloadAsync()
        {
            double today = await energyService.GetTodayConsumptionAsync().ConfigureAwait(false);
            double week = await energyService.GetWeekConsumptionAsync().ConfigureAwait(false);
            double temperature = await energyService.GetIndoorTemperatureAsync().ConfigureAwait(false);
            string pacStatus = await energyService.GetHeatPumpStatusAsync().ConfigureAwait(false);
            IList<EnergyDevice> devices = await energyService.GetDevicesAsync().ConfigureAwait(false);
            IList<EnergyHistoryPoint> history = await energyService.GetHistoryAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TodayConsumption = $"{today:0.0} kWh";
                WeekConsumption = $"{week:0.0} kWh";
                IndoorTemperature = $"{temperature:0.0}°C";
                HeatPumpStatus = pacStatus;

                Devices.Clear();
                foreach (EnergyDevice device in devices)
                {
                    Devices.Add(device);
                }

                History.Clear();
                foreach (EnergyHistoryPoint point in history)
                {
                    History.Add(new EnergyHistoryItem
                    {
                        DayLabel = point.Date.ToString("ddd"),
                        ValueText = $"{point.ValueKwh:0.0} kWh"
                    });
                }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public sealed class EnergyHistoryItem
    {
        public string DayLabel { get; set; } = "";
        public string ValueText { get; set; } = "";
    }
}