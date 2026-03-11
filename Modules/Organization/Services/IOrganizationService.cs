using HomeAppLBO.Modules.Organization.Models;

namespace HomeAppLBO.Modules.Organization.Services
{
    public interface IOrganizationService
    {
        Task<IList<FamilyEvent>> GetEventsAsync();
        Task<IList<ShoppingItem>> GetShoppingItemsAsync();
        Task ToggleShoppingItemAsync(string itemId);
        Task AddShoppingItemAsync(string itemName);
        Task RefreshAsync();
    }
}