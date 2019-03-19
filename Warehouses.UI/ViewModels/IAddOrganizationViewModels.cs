using System.Windows.Input;

namespace Warehouses.UI.ViewModels
{
    public interface IAddOrganizationViewModels
    {
        ICommand Save { get; set; }
    }
}