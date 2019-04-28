using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Windows;
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
        private bool _isFromWarehouse;

        public ReceiptViewModel(ReceiptTableViewModel receiptTable, IWarehouseDataService warehouseService, IEventAggregator eventAggregator)
        {
            _warehouseService = warehouseService;
            _eventAggregator = eventAggregator;
            ReceiptTable = receiptTable;
            Warehouses = new ObservableCollection<Warehouse>();
            AddRow = new DelegateCommand(ExecuteAddRowCommand, ExecuteCanAddRowCommand);
            Close = new DelegateCommand<Window>(ExecuteCloesCommand);
            Save = new DelegateCommand<Window>(ExecuteSaveCommand);
            Date = DateTime.Now;
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

        public DateTime Date { get; set; }

        public bool IsFromWarehouse
        { get
            {
                return _isFromWarehouse;
            }
            set
            {
                _isFromWarehouse = value;
                OnPropertyChanged
            } }
        public bool IsFromSupplier { get; set; }
        public bool IsToWarehouse { get; set; }
        public bool IsToCostumer { get; set; }

        public ICommand AddRow { get; set; }

        public ICommand Close { get; set; }

        public ICommand Save { get; set; }

        private bool ExecuteCanAddRowCommand()
        {
            return true;
        }

        private void ExecuteAddRowCommand()
        {
            _eventAggregator.GetEvent<AddReceiptRowEvent>().Publish();
        }

        private void ExecuteCloesCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteSaveCommand(Window window)
        {
            MessageBox.Show("Save");
            window.Close();
        }

    }
}
