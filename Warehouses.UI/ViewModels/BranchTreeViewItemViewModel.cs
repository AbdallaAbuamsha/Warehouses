using Prism.Events;
using System.Collections.ObjectModel;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using System;
using Warehouses.Model;
using Warehouses.UI.Startup;
using Autofac;

namespace Warehouses.UI.ViewModels
{
    public class BranchTreeViewItemViewModel : ViewModelBase
    {
        IWarehouseDataService _warehouseDataService;
        IEventAggregator _eventAggregator;
        private bool _isSelected;
        private bool _isExpanded;
        private Branch _branch;

        public BranchTreeViewItemViewModel(Branch branch, IWarehouseDataService warehouseDataService, IEventAggregator eventAggregator)
        {
            _warehouseDataService = warehouseDataService;
            _eventAggregator = eventAggregator;
            _branch = branch;
            Warehouses = new ObservableCollection<WarehouseTreeViewItemViewModel>();
            Warehouses.Add(null);
            eventAggregator.GetEvent<BranchComboBoxItemSelectedEvent>().Subscribe(BranchSelected);

        }

        private void BranchSelected(BranchTreeViewItemViewModel branch)
        {
            if (branch.Branch.Id == this.Branch.Id)
            {
                IsSelected = true;
                IsExpanded = true;
            }
            else
            {
                IsSelected = false;
            }
        }


        public Branch Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }

        public ObservableCollection<WarehouseTreeViewItemViewModel> Warehouses { get; set; }
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
                        _eventAggregator.GetEvent<BranchTreeItemSelectedEvent>().Publish(this);
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
                        var warehouses = _warehouseDataService.GetByParentId(Branch.Id);
                        Warehouses.Clear();
                        foreach (var warehouse in warehouses)
                        {
                            var warehouseItem = Bootstrapper.Builder.Resolve<WarehouseTreeViewItemViewModel>(new NamedParameter("warehouse", warehouse));
                            Warehouses.Add(warehouseItem);
                            /////////Warehouses.Add(new WarehouseTreeViewItemViewModel { Id = branch.Id, Name = branch.Name });
                        }
                    }
                }
            }
        }
    }
}
