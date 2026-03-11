using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAppLBO.Modules.Security.Models
{
    public sealed class SecurityDevice : INotifyPropertyChanged
    {
        private string id;
        private string name;
        private string location;
        private string type;
        private string statusText;
        private bool isAlert;
        private bool isOnline;

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

        public string Location
        {
            get => location;
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StatusText
        {
            get => statusText;
            set
            {
                if (statusText != value)
                {
                    statusText = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsAlert
        {
            get => isAlert;
            set
            {
                if (isAlert != value)
                {
                    isAlert = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StatusColorKey));
                }
            }
        }

        public bool IsOnline
        {
            get => isOnline;
            set
            {
                if (isOnline != value)
                {
                    isOnline = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StatusColorKey => IsAlert ? "ActionDanger" : "AccentPrimary";

        public SecurityDevice()
        {
            id = string.Empty;
            name = string.Empty;
            location = string.Empty;
            type = string.Empty;
            statusText = string.Empty;
            isAlert = false;
            isOnline = true;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}