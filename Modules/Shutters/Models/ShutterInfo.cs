using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAppLBO.Modules.Shutters.Models
{
    public sealed class ShutterInfo : INotifyPropertyChanged
    {
        private string id;
        private string name;
        private string roomName;
        private int position;
        private bool isMoving;
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

        public int Position
        {
            get => position;
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

                if (position != normalized)
                {
                    position = normalized;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StatusText));
                }
            }
        }

        public bool IsMoving
        {
            get => isMoving;
            set
            {
                if (isMoving != value)
                {
                    isMoving = value;
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
                if (IsMoving)
                {
                    return "En mouvement";
                }

                if (Position == 0)
                {
                    return "Fermé";
                }

                if (Position == 100)
                {
                    return "Ouvert";
                }

                return $"Ouvert à {Position}%";
            }
        }

        public ShutterInfo()
        {
            id = string.Empty;
            name = string.Empty;
            roomName = string.Empty;
            position = 0;
            isMoving = false;
            isAvailable = true;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}