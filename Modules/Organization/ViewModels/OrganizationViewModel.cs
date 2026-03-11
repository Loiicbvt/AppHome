using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HomeAppLBO.Modules.Organization.Models;
using HomeAppLBO.Modules.Organization.Services;
using Microsoft.Maui.ApplicationModel;

namespace HomeAppLBO.Modules.Organization.ViewModels
{
    public sealed class OrganizationViewModel : INotifyPropertyChanged
    {
        private readonly IOrganizationService organizationService;

        private string newShoppingItemName;
        private string eventsSummary;
        private string shoppingSummary;

        public ObservableCollection<FamilyEvent> Events { get; }
        public ObservableCollection<ShoppingItem> ShoppingItems { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string NewShoppingItemName
        {
            get => newShoppingItemName;
            set
            {
                if (newShoppingItemName != value)
                {
                    newShoppingItemName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EventsSummary
        {
            get => eventsSummary;
            set
            {
                if (eventsSummary != value)
                {
                    eventsSummary = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ShoppingSummary
        {
            get => shoppingSummary;
            set
            {
                if (shoppingSummary != value)
                {
                    shoppingSummary = value;
                    OnPropertyChanged();
                }
            }
        }

        public OrganizationViewModel(IOrganizationService service)
        {
            organizationService = service;

            Events = new ObservableCollection<FamilyEvent>();
            ShoppingItems = new ObservableCollection<ShoppingItem>();

            newShoppingItemName = string.Empty;
            eventsSummary = "Chargement...";
            shoppingSummary = "Chargement...";
        }

        public async Task InitializeAsync()
        {
            await ReloadAsync();
        }

        public async Task RefreshAsync()
        {
            await organizationService.RefreshAsync().ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task ToggleShoppingItemAsync(ShoppingItem? item)
        {
            if (item == null)
            {
                return;
            }

            await organizationService.ToggleShoppingItemAsync(item.Id).ConfigureAwait(false);
            await ReloadAsync().ConfigureAwait(false);
        }

        public async Task AddShoppingItemAsync()
        {
            string name = NewShoppingItemName?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            await organizationService.AddShoppingItemAsync(name).ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                NewShoppingItemName = string.Empty;
            });

            await ReloadAsync().ConfigureAwait(false);
        }

        private async Task ReloadAsync()
        {
            IList<FamilyEvent> events = await organizationService.GetEventsAsync().ConfigureAwait(false);
            IList<ShoppingItem> items = await organizationService.GetShoppingItemsAsync().ConfigureAwait(false);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Events.Clear();
                foreach (FamilyEvent familyEvent in events)
                {
                    Events.Add(familyEvent);
                }

                ShoppingItems.Clear();
                foreach (ShoppingItem item in items)
                {
                    ShoppingItems.Add(item);
                }

                EventsSummary = Events.Count == 0
                    ? "Aucun événement"
                    : $"{Events.Count} événement(s) à venir";

                int checkedCount = ShoppingItems.Count(x => x.IsChecked);
                ShoppingSummary = $"{checkedCount}/{ShoppingItems.Count} cochés";
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}