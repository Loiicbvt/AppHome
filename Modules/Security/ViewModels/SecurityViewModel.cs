using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HomeAppLBO.Modules.Security.Models;
using HomeAppLBO.Modules.Security.Services;
using Microsoft.Maui.ApplicationModel;
using System.Linq;

namespace HomeAppLBO.Modules.Security.ViewModels
{
    public sealed class SecurityViewModel : INotifyPropertyChanged
    {
        private readonly ISecurityService securityService;

        private string globalStatus;
        private string globalSubtitle;
        private bool awayModeEnabled;

        public ObservableCollection<SecurityDevice> Devices { get; }

        public ObservableCollection<SecurityEvent> Events { get; }

        public ObservableCollection<SecurityCamera> Cameras { get; } 

        public event PropertyChangedEventHandler? PropertyChanged;

        public string GlobalStatus
        {
            get => globalStatus;
            set
            {
                if (globalStatus != value)
                {
                    globalStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GlobalSubtitle
        {
            get => globalSubtitle;
            set
            {
                if (globalSubtitle != value)
                {
                    globalSubtitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AwayModeEnabled
        {
            get => awayModeEnabled;
            set
            {
                if (awayModeEnabled != value)
                {
                    awayModeEnabled = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(AwayModeText));
                }
            }
        }

        public string AwayModeText => AwayModeEnabled ? "Mode absence activé" : "Mode absence désactivé";

        public SecurityViewModel(ISecurityService service)
        {
            securityService = service;
            Devices = new ObservableCollection<SecurityDevice>();
            Events = new ObservableCollection<SecurityEvent>();
            Cameras = new ObservableCollection<SecurityCamera>();
            globalStatus = "Chargement...";
            globalSubtitle = string.Empty;
        }

        public async Task InitializeAsync()
        {
            await ReloadAsync();
        }

        public async Task ToggleAwayModeAsync()
        {
            await securityService.SetAwayModeAsync(!AwayModeEnabled).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task RefreshAsync()
        {
            await securityService.RefreshAsync().ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        private async Task ReloadAsync()
        {
            IList<SecurityDevice> devices = await securityService.GetDevicesAsync().ConfigureAwait(false);
            IList<SecurityEvent> events = await securityService.GetEventsAsync().ConfigureAwait(false);
            IList<SecurityCamera> cameras = await securityService.GetCamerasAsync().ConfigureAwait(false);
            bool awayMode = await securityService.GetAwayModeAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Capteurs et autres éléments de sécurité

                Devices.Clear();

                foreach (SecurityDevice device in devices)
                {
                    Devices.Add(device);
                }

                AwayModeEnabled = awayMode;

                bool hasAlert = devices.Any(x => x.IsAlert);

                if (hasAlert)
                {
                    GlobalStatus = "Alerte sécurité";
                    GlobalSubtitle = "Un ou plusieurs éléments demandent une attention";
                }
                else if (awayMode)
                {
                    GlobalStatus = "Maison sécurisée";
                    GlobalSubtitle = "Surveillance active en mode absence";
                }
                else
                {
                    GlobalStatus = "Maison calme";
                    GlobalSubtitle = "Aucune alerte détectée";
                }

                // Événements récents

                Events.Clear();

                foreach (SecurityEvent ev in events.OrderByDescending(x => x.Time))
                {
                    Events.Add(ev);
                }

                // Caméras

                Cameras.Clear();

                foreach (SecurityCamera camera in cameras)
                {
                    Cameras.Add(camera);
                }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}