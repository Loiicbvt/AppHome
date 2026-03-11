using HomeAppLBO.Modules.Organization.Models;

namespace HomeAppLBO.Modules.Organization.Services
{
    public sealed class MockOrganizationService : IOrganizationService
    {
        private readonly IList<FamilyEvent> events;
        private readonly IList<ShoppingItem> shoppingItems;

        public MockOrganizationService()
        {
            events = new List<FamilyEvent>
            {
                new FamilyEvent
                {
                    Id = "event_1",
                    Title = "Rendez-vous PAC",
                    Description = "Installation / contrôle Panasonic",
                    Location = "Maison",
                    StartDate = DateTime.Today.AddHours(14),
                    Icon = "🌡️"
                },
                new FamilyEvent
                {
                    Id = "event_2",
                    Title = "Courses de la semaine",
                    Description = "Prévoir le repas du week-end",
                    Location = "Supermarché",
                    StartDate = DateTime.Today.AddDays(1).AddHours(18),
                    Icon = "🛒"
                },
                new FamilyEvent
                {
                    Id = "event_3",
                    Title = "Soirée famille",
                    Description = "Dîner tranquille à la maison",
                    Location = "Maison",
                    StartDate = DateTime.Today.AddDays(2).AddHours(20),
                    Icon = "🏠"
                }
            };

            shoppingItems = new List<ShoppingItem>
            {
                new ShoppingItem { Id = "shop_1", Name = "Lait", Quantity = 2, IsChecked = false },
                new ShoppingItem { Id = "shop_2", Name = "Pain", Quantity = 1, IsChecked = false },
                new ShoppingItem { Id = "shop_3", Name = "Pâtes", Quantity = 3, IsChecked = true },
                new ShoppingItem { Id = "shop_4", Name = "Tomates", Quantity = 4, IsChecked = false }
            };
        }

        public Task<IList<FamilyEvent>> GetEventsAsync()
        {
            IList<FamilyEvent> ordered = events
                .OrderBy(x => x.StartDate)
                .ToList();

            return Task.FromResult(ordered);
        }

        public Task<IList<ShoppingItem>> GetShoppingItemsAsync()
        {
            IList<ShoppingItem> ordered = shoppingItems
                .OrderBy(x => x.IsChecked)
                .ThenBy(x => x.Name)
                .ToList();

            return Task.FromResult(ordered);
        }

        public Task ToggleShoppingItemAsync(string itemId)
        {
            ShoppingItem? item = shoppingItems.FirstOrDefault(x => x.Id == itemId);

            if (item != null)
            {
                item.IsChecked = !item.IsChecked;
            }

            return Task.CompletedTask;
        }

        public Task AddShoppingItemAsync(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return Task.CompletedTask;
            }

            shoppingItems.Add(new ShoppingItem
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = itemName.Trim(),
                Quantity = 1,
                IsChecked = false
            });

            return Task.CompletedTask;
        }

        public Task RefreshAsync()
        {
            return Task.CompletedTask;
        }
    }
}