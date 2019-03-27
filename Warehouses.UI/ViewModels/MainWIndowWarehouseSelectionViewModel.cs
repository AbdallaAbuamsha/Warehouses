using Autofac;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Startup;

namespace Warehouses.UI.ViewModels
{
    public class MainWIndowWarehouseSelectionViewModel : ViewModelBase, IMainWIndowWarehouseSelectionViewModel
    {
        private IOrganizationDataService _organizationDataService;
        private IBranchDataService _branchDataService;
        private IWarehouseDataService _warehouseDataService;
        IEventAggregator _eventAggregator;
        private OrganizationTreeViewItemViewModel _selectedOrganization;
        private BranchTreeViewItemViewModel _selectedBranch;
        private WarehouseTreeViewItemViewModel _selectedWarehouse;

        public MainWIndowWarehouseSelectionViewModel(
            IOrganizationDataService organizationDataService,
            IBranchDataService branchDataService,
            IWarehouseDataService warehouseDataService,
            IEventAggregator eventAggregator)
        {
            
            _organizationDataService = organizationDataService;
            _branchDataService = branchDataService;
            _warehouseDataService = warehouseDataService;
            _eventAggregator = eventAggregator;

            Organizations = new ObservableCollection<OrganizationTreeViewItemViewModel>();
            Branches = new ObservableCollection<BranchTreeViewItemViewModel>();
            Warehouses = new ObservableCollection<WarehouseTreeViewItemViewModel>();

            eventAggregator.GetEvent<OrganizationTreeItemSelectedEvent>().Subscribe(OrganizationSelected);
            eventAggregator.GetEvent<BranchTreeItemSelectedEvent>().Subscribe(BranchSelected);
            eventAggregator.GetEvent<WarehouseItemSelectedEvent>().Subscribe(WarehouseSelected);
        }

        public void Load()
        {
            var organizations = _organizationDataService.GetAll();
            foreach (Organization organization in organizations)
            {
                var orgItem = Bootstrapper.Builder.Resolve<OrganizationTreeViewItemViewModel>(new NamedParameter("organization", organization));
                Organizations.Add(orgItem);
            }
        }
        private void OrganizationSelected(OrganizationTreeViewItemViewModel organization)
        {
            if (SelectedOrganization != null && SelectedOrganization.Organization.Id == organization.Organization.Id)
                return;
            SelectedOrganization = organization;
        }
        private void BranchSelected(BranchTreeViewItemViewModel branch)
        {
            if (SelectedBranch.Branch.Id == branch.Branch.Id)
                return;
            if (branch.Branch.ParentId != SelectedOrganization.Organization.Id)
            {
                foreach (var org in Organizations)
                {
                    if(branch.Branch.ParentId == org.Organization.Id)
                    {
                        SelectedOrganization = org;
                    }
                }
            }
            foreach (var br in Branches)
            {
                if(br.Branch.Id == branch.Branch.Id)
                {
                    SelectedBranch = branch;
                }
            }
        }
        private void WarehouseSelected(WarehouseTreeViewItemViewModel obj)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<Branch> LoadBranches(int id)
        {
            return _branchDataService.GetByParentId(id);
        }

        internal IEnumerable LoadWarehouses(int id)
        {
            return _warehouseDataService.GetByParentId(id);
        }

        public IMainMenuViewModel MainMenuViewModel { get; }

        public ObservableCollection<OrganizationTreeViewItemViewModel> Organizations { get; set; }

        public OrganizationTreeViewItemViewModel SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                if (_selectedOrganization == value)
                    return;
                _selectedOrganization = value;
                OnPropertyChanged();
                var branches = _branchDataService.GetByParentId(SelectedOrganization.Organization.Id);
                Branches.Clear();
                foreach (var branch in branches)
                {
                    var temp = Bootstrapper.Builder.Resolve<BranchTreeViewItemViewModel>(new NamedParameter("branch", branch));
                    Branches.Add(temp);
                }
                _eventAggregator.GetEvent<OrganizationComboBoxItemSelectedEvent>().Publish(SelectedOrganization);
            }
        }

        public ObservableCollection<BranchTreeViewItemViewModel> Branches { get; set; }
        public BranchTreeViewItemViewModel SelectedBranch
        {
            get
            {
                return _selectedBranch;
            }
            set
            {
                if (_selectedBranch == value)
                    return;
                _selectedBranch = value;
                OnPropertyChanged();
                var warehouses = _warehouseDataService.GetByParentId(SelectedOrganization.Organization.Id);
                Warehouses.Clear();
                foreach (var warehouse in warehouses)
                {
                    var temp = Bootstrapper.Builder.Resolve<WarehouseTreeViewItemViewModel>(new NamedParameter("warehouse", warehouse));
                    Warehouses.Add(temp);
                }
                SelectedOrganization.IsSelected = false;
                _eventAggregator.GetEvent<BranchComboBoxItemSelectedEvent>().Publish(SelectedBranch);

            }
        }

        public ObservableCollection<WarehouseTreeViewItemViewModel> Warehouses { get; set; }

        public WarehouseTreeViewItemViewModel SelectedWarehouse
        {
            get { return _selectedWarehouse; }
            set { _selectedWarehouse = value; }
        }

    }
}
