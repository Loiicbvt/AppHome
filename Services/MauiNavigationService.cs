using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using HomeAppLBO.Core;

namespace HomeAppLBO.Services
{
    public sealed class MauiNavigationService : INavigationService
    {
        public Task NavigateToAsync(Page page)
        {
            if (page == null)
            {
                throw new System.ArgumentNullException("page");
            }

            INavigation navigation = Application.Current.MainPage.Navigation;
            return navigation.PushAsync(page);
        }

        public Task GoBackAsync()
        {
            INavigation navigation = Application.Current.MainPage.Navigation;
            return navigation.PopAsync();
        }
    }
}