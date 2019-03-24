using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Startup;

namespace Warehouses.UI.ViewModels
{
    public class OrganizationTreeViewItemViewModel : ViewModelBase
    {
        IBranchDataService _brancheDataService;
        public OrganizationTreeViewItemViewModel(IBranchDataService brancheDataService)
        {
            _brancheDataService = brancheDataService;
            Branches = new ObservableCollection<BranchTreeViewItemViewModel>();
            Branches.Add(null);
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public ObservableCollection<BranchTreeViewItemViewModel> Branches { get; set; }
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
                        var branches = _brancheDataService.GetByParentId(Id);
                        Branches.Clear();
                        var bootstrapper = new Bootstrapper();
                        var builder = bootstrapper.Bootstrap();
                        foreach (var branch in branches)
                        {
                            var branchItem = builder.Resolve<BranchTreeViewItemViewModel>();
                            branchItem.Id = branch.Id;
                            branchItem.Name = branch.Name;
                            Branches.Add(branchItem);
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
                        var branches = _brancheDataService.GetByParentId(Id);
                        Branches.Clear();
                        var bootstrapper = new Bootstrapper();
                        var builder = bootstrapper.Bootstrap();
                        foreach (var branch in branches)
                        {
                            var branchItem = builder.Resolve<BranchTreeViewItemViewModel>();
                            branchItem.Id = branch.Id;
                            branchItem.Name = branch.Name;
                            Branches.Add(branchItem);
                        }
                    }
                }
            }
        }
    }
}
