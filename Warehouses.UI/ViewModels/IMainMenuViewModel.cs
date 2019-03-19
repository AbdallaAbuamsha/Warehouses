using System.Windows.Input;

namespace Warehouses.UI.ViewModels
{
    public interface IMainMenuViewModel
    {
        ICommand InputReceiptCommand { get; set; }
        ICommand NewBranchCommand { get; set; }
        ICommand NewOrganizationCommand { get; set; }
        ICommand NewWarehouseCommand { get; set; }
        ICommand NewMaterialCommand { get; set; }
        ICommand NewUnitCommand { get; set; }
        ICommand OutputReceiptCommand { get; set; }
        ICommand TransactionReceiptCommand { get; set; }
    }
}