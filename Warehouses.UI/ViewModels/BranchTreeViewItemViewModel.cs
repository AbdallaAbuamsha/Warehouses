using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;

namespace Warehouses.UI.ViewModels
{
    public class BranchTreeViewItemViewModel : TreeViewItemViewModel
    {
        IWarehouseDataService _warehouseDataService;
        IEventAggregator _eventAggregator;
        IMessageDialogService _messageDialogService;
        private bool _isSelected;
        private bool _isExpanded;
        private string _detailViewModelName;

        public BranchTreeViewItemViewModel(long id, string displayMember, string detailViewModelName, 
            IWarehouseDataService warehouseDataService, 
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            :base(id, displayMember)
        {
            _warehouseDataService = warehouseDataService;
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
            _messageDialogService = messageDialogService;
            TreeItems = new ObservableCollection<TreeViewItemViewModel>();
            TreeItems.Add(null);
        }

        public ObservableCollection<TreeViewItemViewModel> TreeItems { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    if (IsSelected)
                    {
                        _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(
                        new OpenDetailViewEventArgs
                        {
                            Id = this.Id,
                            ViewModelName = _detailViewModelName
                        });
                    }
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
                        //var warehouses = _warehouseDataService.GetByParentId(Id);
                        ResultObject warehouseResult = BusinessLayer.Warehouse_BL.GetAllByBranchId(Id, AppConstants.ARABIC);
                        if (warehouseResult.Code == AppConstants.ERROR_CODE)
                        {
                            _messageDialogService.ShowInfoDialog(warehouseResult.Message);
                            return;
                        }
                        ResultList<Warehouse> warehouseListResult = (ResultList<Warehouse>)warehouseResult.Data;
                        List<Warehouse> warehouses = warehouseListResult.List;
                        TreeItems.Clear();
                        foreach (var warehouse in warehouses)
                        {
                            var warehouseItem = new WarehouseTreeViewItemViewModel(warehouse.Id, warehouse.Name, nameof(WarehouseDetailViewModel), new WarehouseDataService(), _eventAggregator, _messageDialogService);
                            TreeItems.Add(warehouseItem);
                            /////////Warehouses.Add(new WarehouseTreeViewItemViewModel { Id = branch.Id, Name = branch.Name });
                        }
                    }
                }
            }
        }
    }
}
