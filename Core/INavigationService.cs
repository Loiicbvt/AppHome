using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace HomeAppLBO.Core
{
    public interface INavigationService
    {
        Task NavigateToAsync(Page page);
        Task GoBackAsync();
    }
}