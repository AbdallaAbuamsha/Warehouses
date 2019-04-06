using Prism.Events;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class WarehouseTreeViewItemViewModel : TreeViewItemViewModel
    {
        IWarehouseDataService _warehouseDataService;
        IEventAggregator _eventAggregator;
        private Warehouse _warehouse;
        private bool _isExpanded;
        private bool _isSelected;
        private string _detailViewModelName;

        public WarehouseTreeViewItemViewModel(
            int id,
            string displayMember,
            string detailViewModelName,
            IWarehouseDataService warehouseDataService,
            IEventAggregator eventAggregator)
            :base(id, displayMember)
        {
            _warehouseDataService = warehouseDataService;
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
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
                        var warehouses = _warehouseDataService.GetByParentId(Id);
                        TreeItems.Clear();
                    }
                }
            }
        }
    }
}
