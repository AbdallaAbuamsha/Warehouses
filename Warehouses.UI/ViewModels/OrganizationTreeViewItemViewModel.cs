using Autofac;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Startup;

namespace Warehouses.UI.ViewModels
{
    public class OrganizationTreeViewItemViewModel : ViewModelBase
    {
        IBranchDataService _brancheDataService;
        IEventAggregator _eventAggregator;
        public OrganizationTreeViewItemViewModel(IBranchDataService brancheDataService, IEventAggregator eventAggregator)
        {
            _brancheDataService = brancheDataService;
            _eventAggregator = eventAggregator;
            Branches = new ObservableCollection<BranchTreeViewItemViewModel>();
            Branches.Add(null);
            eventAggregator.GetEvent<OrganizationComboBoxItemSelectedEvent>().Subscribe(OrganizationSelected);

        }

        private void OrganizationSelected(OrganizationTreeViewItemViewModel organization)
        {
            if(organization.Id == this.Id)
            {
                IsSelected = true;
                IsExpanded = true;
            }
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
                        _eventAggregator.GetEvent<OrganizationTreeItemSelectedEvent>().Publish(this);
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
                        foreach (var branch in branches)
                        {
                            var branchItem = Bootstrapper.Builder.Resolve<BranchTreeViewItemViewModel>();
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
