using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAppLBO.Modules.Lights.Models
{
    public sealed class LightInfo : INotifyPropertyChanged
    {
        private string id;
        private string name;
        private string roomName;
        private bool isOn;
        private int brightness;
        private bool isAvailable;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RoomName
        {
            get => roomName;
            set
            {
                if (roomName != value)
                {
                    roomName = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsOn
        {
            get => isOn;
            set
            {
                if (isOn != value)
                {
                    isOn = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StatusText));
                }
            }
        }

        public int Brightness
        {
            get => brightness;
            set
            {
                int normalized = value;

                if (normalized < 0)
                {
                    normalized = 0;
                }

                if (normalized > 100)
                {
                    normalized = 100;
                }

                if (brightness != normalized)
                {
                    brightness = normalized;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StatusText));
                }
            }
        }

        public bool IsAvailable
        {
            get => isAvailable;
            set
            {
                if (isAvailable != value)
                {
                    isAvailable = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StatusText
        {
            get
            {
                if (!IsOn)
                {
                    return "Éteinte";
                }

                return $"Allumée • {Brightness}%";
            }
        }

        public LightInfo()
        {
            id = string.Empty;
            name = string.Empty;
            roomName = string.Empty;
            brightness = 0;
            isOn = false;
            isAvailable = true;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}