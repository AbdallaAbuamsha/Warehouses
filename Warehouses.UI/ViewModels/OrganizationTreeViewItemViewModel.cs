using Autofac;
using Prism.Events;
using System.Collections.ObjectModel;
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
        private Organization _organization;
        public OrganizationTreeViewItemViewModel(Organization organization, IBranchDataService brancheDataService, IEventAggregator eventAggregator)
        {
            _brancheDataService = brancheDataService;
            _eventAggregator = eventAggregator;
            _organization = organization;
            Branches = new ObservableCollection<BranchTreeViewItemViewModel>();
            Branches.Add(null);
            eventAggregator.GetEvent<OrganizationComboBoxItemSelectedEvent>().Subscribe(OrganizationSelected);

        }

        private void OrganizationSelected(OrganizationTreeViewItemViewModel organization)
        {
            if(organization.Organization.Id == this.Organization.Id)
            {
                IsSelected = true;
                IsExpanded = true;
            }
        }
        

        public Organization Organization
        {
            get { return _organization; }
            set
            {
                _organization = value;
                OnPropertyChanged();
            }
        }


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
                        var branches = _brancheDataService.GetByParentId(Organization.Id);
                        Branches.Clear();
                        foreach (var branch in branches)
                        {
                            var branchItem = Bootstrapper.Builder.Resolve<BranchTreeViewItemViewModel>(new NamedParameter("branch", branch));
                            Branches.Add(branchItem);   
                        }
                    }
                }
            }
        }
    }
}
