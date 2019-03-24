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
    public class BranchTreeViewItemViewModel : ViewModelBase
    {
        IWarehouseDataService _warehouseDataService;
        public BranchTreeViewItemViewModel(IWarehouseDataService warehouseDataService)
        {
            _warehouseDataService = warehouseDataService;
            Warehouses = new ObservableCollection<WarehouseTreeViewItemViewModel>();
            Warehouses.Add(null);
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public ObservableCollection<WarehouseTreeViewItemViewModel> Warehouses { get; set; }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    if (_isSelected)
                    {
                        var branches = _warehouseDataService.GetByParentId(Id);
                        Warehouses.Clear();
                        foreach (var branch in branches)
                        {
                            Warehouses.Add(new WarehouseTreeViewItemViewModel { Id = branch.Id, Name = branch.Name });
                        }
                    }
                }
            }
        }
        private bool _isExpanded;
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
                        var branches = _warehouseDataService.GetByParentId(Id);
                        Warehouses.Clear();
                        foreach (var branch in branches)
                        {
                            Warehouses.Add(new WarehouseTreeViewItemViewModel { Id = branch.Id, Name = branch.Name });
                        }
                    }
                }
            }
        }
    }
}
