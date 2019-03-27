using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class ReceiptViewModel : ViewModelBase, IReceiptViewModel
    {
        private IWarehouseDataService _warehouseService;
        private Warehouse _selectedFromWarehouse;
        private IEventAggregator _eventAggregator;

        public ReceiptViewModel(ReceiptTableViewModel receiptTable, IWarehouseDataService warehouseService, IEventAggregator eventAggregator)
        {
            _warehouseService = warehouseService;
            _eventAggregator = eventAggregator;
            ReceiptTable = receiptTable;
            Warehouses = new ObservableCollection<Warehouse>();
            AddRow = new DelegateCommand(ExecuteAddRowCommand, ExecuteCanAddRowCommand);

        }
        private bool ExecuteCanAddRowCommand()
        {
            return true;
        }

        private void ExecuteAddRowCommand()
        {
            _eventAggregator.GetEvent<AddReceiptRowEvent>().Publish();
        }
        public void Load()
        {
            var warehouses = _warehouseService.GetAll();
            FillLists(Warehouses, warehouses);
        }
        public ReceiptTableViewModel ReceiptTable { get; set; }
        public ObservableCollection<Warehouse> Warehouses { get; set; }

        public Warehouse SelectedFromWarehouse
        {
            get { return _selectedFromWarehouse; }
            set
            {
                _selectedFromWarehouse = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddRow { get; set; }

    }
}
