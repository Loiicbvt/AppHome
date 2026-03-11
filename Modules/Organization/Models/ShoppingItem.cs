using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeAppLBO.Modules.Organization.Models
{
    public sealed class ShoppingItem : INotifyPropertyChanged
    {
        private string id;
        private string name;
        private int quantity;
        private bool isChecked;

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

        public int Quantity
        {
            get => quantity;
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(QuantityText));
                }
            }
        }

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        public string QuantityText => Quantity > 1 ? $"x{Quantity}" : "x1";

        public ShoppingItem()
        {
            id = string.Empty;
            name = string.Empty;
            quantity = 1;
            isChecked = false;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}