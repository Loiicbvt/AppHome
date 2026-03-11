using Microsoft.Maui.Controls;

namespace HomeAppLBO.Core
{
    public interface IModuleDefinition
    {
        string Id { get; }
        string DisplayName { get; }
        string Icon { get; }

        /// <summary>
        /// Crée la page racine du module.
        /// </summary>
        Page CreatePage();
    }
}