using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;
using Warehouses.UI.Data;

namespace Warehouses.UI.ViewModels
{
    public class WarehouseTreeViewItemViewModel : ViewModelBase
    {
        IWarehouseDataService _warehouseDataService;
        IEventAggregator _eventAggregator;
        private Warehouse _warehouse;
        private bool _isExpanded;
        private bool _isSelected;

        public WarehouseTreeViewItemViewModel(Warehouse warehouse, IWarehouseDataService warehouseDataService, IEventAggregator eventAggregator)
        {
            _warehouseDataService = warehouseDataService;
            _eventAggregator = eventAggregator;
            _warehouse = warehouse;
            Warehouses = new ObservableCollection<WarehouseTreeViewItemViewModel>();
        }

        public Warehouse Warehouse
        {
            get { return _warehouse; }
            set { _warehouse = value; }
        }


        public ObservableCollection<WarehouseTreeViewItemViewModel> Warehouses { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {

                }
            }
        }
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                    if (_isExpanded)
                    {
                        var warehouses = _warehouseDataService.GetByParentId(Warehouse.Id);
                        Warehouses.Clear();
                    }
                }
            }
        }
    }
}
