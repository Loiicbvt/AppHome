using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HomeAppLBO.Modules.Shutters.Models;
using HomeAppLBO.Modules.Shutters.Services;
using System.Linq;
using Microsoft.Maui.ApplicationModel;

namespace HomeAppLBO.Modules.Shutters.ViewModels
{
    public sealed class ShuttersViewModel : INotifyPropertyChanged
    {
        private readonly IShutterService shutterService;

        public ObservableCollection<ShutterInfo> Shutters { get; }
        public ObservableCollection<ShutterScenarioItem> Scenarios { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ShuttersViewModel(IShutterService service)
        {
            shutterService = service;
            Shutters = new ObservableCollection<ShutterInfo>();
            Scenarios = new ObservableCollection<ShutterScenarioItem>
            {
                new ShutterScenarioItem("morning", "Matin", "☀️"),
                new ShutterScenarioItem("night", "Nuit", "🌙"),
                new ShutterScenarioItem("away", "Absence", "🚪"),
                new ShutterScenarioItem("living", "Salon", "🛋️")
            };
        }

        public async Task InitializeAsync()
        {
            await ReloadAsync();
        }

        public async Task OpenAsync(ShutterInfo? shutter)
        {
            if (shutter == null)
            {
                return;
            }

            await shutterService.OpenAsync(shutter.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task CloseAsync(ShutterInfo? shutter)
        {
            if (shutter == null)
            {
                return;
            }

            await shutterService.CloseAsync(shutter.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task StopAsync(ShutterInfo? shutter)
        {
            if (shutter == null)
            {
                return;
            }

            await shutterService.StopAsync(shutter.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task RunScenarioAsync(ShutterScenarioItem? scenario)
        {
            if (scenario == null)
            {
                return;
            }

            await shutterService.RunScenarioAsync(scenario.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        private async Task ReloadAsync()
        {
            IList<ShutterInfo> list = await shutterService.GetShuttersAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Shutters.Clear();

                foreach (ShutterInfo shutter in list)
                {
                    Shutters.Add(shutter);
                }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public sealed class ShutterScenarioItem
    {
        public string Id { get; }
        public string Name { get; }
        public string Icon { get; }

        public ShutterScenarioItem(string id, string name, string icon)
        {
            Id = id;
            Name = name;
            Icon = icon;
        }
    }
}