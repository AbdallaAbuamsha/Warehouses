using Autofac;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Startup;

namespace Warehouses.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IOrganizationDataService _organizationDataService;
        private IBranchDataService _branchDataService;
        private IWarehouseDataService _warehouseDataService;
        IEventAggregator _eventAggregator;
        private OrganizationTreeViewItemViewModel _selectedOrganization;
        private Branch _selectedBranch;

        public MainWindowViewModel(
            IMainMenuViewModel mainMenuViewModel,
            IOrganizationDataService organizationDataService,
            IBranchDataService branchDataService,
            IWarehouseDataService warehouseDataService,
            IEventAggregator eventAggregator)
        {
            MainMenuViewModel = mainMenuViewModel;
            _organizationDataService = organizationDataService;
            _branchDataService = branchDataService;
            _warehouseDataService = warehouseDataService;
            _eventAggregator = eventAggregator;

            Organizations = new ObservableCollection<OrganizationTreeViewItemViewModel>();
            Branches = new ObservableCollection<Branch>();
            Warehouses = new ObservableCollection<Warehouse>();

            eventAggregator.GetEvent<OrganizationSelectedEvent>().Subscribe(OrganizationSelected);

        }


        public void OrganizationSelected(OrganizationTreeViewItemViewModel organization)
        {
            SelectedOrganization = organization;
        }

        public ObservableCollection<OrganizationTreeViewItemViewModel> Load()
        {
            var organizations = _organizationDataService.GetAll();
            foreach (Organization organization in organizations)
            {
                var orgItem = Bootstrapper.Builder.Resolve<OrganizationTreeViewItemViewModel>();
                orgItem.Id = organization.Id;
                orgItem.Name = organization.Name;
                Organizations.Add(orgItem);
            }
            return Organizations;
        }

        internal IEnumerable<Branch> LoadBranches(int id)
        {
            return _branchDataService.GetByParentId(id);
        }

        public IMainMenuViewModel MainMenuViewModel { get; }

        internal IEnumerable LoadWarehouses(int id)
        {
            return _warehouseDataService.GetByParentId(id);
        }

        public ObservableCollection<OrganizationTreeViewItemViewModel> Organizations { get; set; }
        public OrganizationTreeViewItemViewModel SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Branch> Branches { get; set; }
        public Branch SelectedBranch {
            get
            {
                return _selectedBranch;
            }
            set
            {
                _selectedBranch = value;
                OnPropertyChanged();
                FillLists(Warehouses, _warehouseDataService.GetByParentId(SelectedOrganization.Id));

            }
        }

        public ObservableCollection<Warehouse> Warehouses { get; set; }
        public Warehouse SelectedWarehouse { get; set; }
    }
}
