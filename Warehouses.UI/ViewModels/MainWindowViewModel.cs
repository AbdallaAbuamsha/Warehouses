using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;

namespace Warehouses.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IOrganizationDataService _organizationDataService;
        private IBranchDataService _branchDataService;
        private IWarehouseDataService _warehouseDataService;

        private Organization _selectedOrganization;
        private Branch _selectedBranch;

        public MainWindowViewModel(
            IMainMenuViewModel mainMenuViewModel,
            IOrganizationDataService organizationDataService,
            IBranchDataService branchDataService,
            IWarehouseDataService warehouseDataService)
        {
            this.MainMenuViewModel = mainMenuViewModel;
            _organizationDataService = organizationDataService;
            _branchDataService = branchDataService;
            _warehouseDataService = warehouseDataService;
            Organizations = new ObservableCollection<Organization>();
            Branches = new ObservableCollection<Branch>();
            Warehouses = new ObservableCollection<Warehouse>();

        }
        public ObservableCollection<Organization> Load()
        {
            var organizations = _organizationDataService.GetAll();
            FillLists(Organizations, organizations);
            return Organizations;
        }
        public IMainMenuViewModel MainMenuViewModel { get; }
        public ObservableCollection<Organization> Organizations { get; set; }
        public Organization SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged();
                FillLists(Branches, _branchDataService.GetByParentId(SelectedOrganization.Id));
                //FillLists(Warehouses, _warehouseDataService.GetByParentId(SelectedOrganization.Id));
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
