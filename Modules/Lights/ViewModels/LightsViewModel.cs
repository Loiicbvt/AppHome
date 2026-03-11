using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HomeAppLBO.Modules.Lights.Models;
using HomeAppLBO.Modules.Lights.Services;
using Microsoft.Maui.ApplicationModel;

namespace HomeAppLBO.Modules.Lights.ViewModels
{
    public sealed class LightsViewModel : INotifyPropertyChanged
    {
        private readonly ILightService lightService;

        public ObservableCollection<LightInfo> Lights { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public LightsViewModel(ILightService service)
        {
            lightService = service;
            Lights = new ObservableCollection<LightInfo>();
        }

        public async Task InitializeAsync()
        {
            await ReloadAsync();
        }

        public async Task ToggleAsync(LightInfo? light)
        {
            if (light == null)
            {
                return;
            }

            await lightService.ToggleAsync(light.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task TurnOnAsync(LightInfo? light)
        {
            if (light == null)
            {
                return;
            }

            await lightService.TurnOnAsync(light.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task TurnOffAsync(LightInfo? light)
        {
            if (light == null)
            {
                return;
            }

            await lightService.TurnOffAsync(light.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task SetBrightnessAsync(LightInfo? light, int brightness)
        {
            if (light == null)
            {
                return;
            }

            await lightService.SetBrightnessAsync(light.Id, brightness).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        private async Task ReloadAsync()
        {
            IList<LightInfo> list = await lightService.GetLightsAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Lights.Clear();

                foreach (LightInfo light in list)
                {
                    Lights.Add(light);
                }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}