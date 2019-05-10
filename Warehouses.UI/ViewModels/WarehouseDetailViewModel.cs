using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Warehouses.BusinessLayer;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class WarehouseDetailViewModel : DetailViewModelBase
    {
        private IWarehouseDataService _warehouseService;
        private WarehouseWrapper _warehouseWrapper;
        private IMessageDialogService _messageDialogService;
        private Organization _selectedOrganization;
        private Branch _selectedBranch;

        public WarehouseDetailViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IWarehouseDataService warehouseService) : base(eventAggregator, messageDialogService)
        {
            _warehouseService = warehouseService;
            _messageDialogService = messageDialogService;
            Organizations = new ObservableCollection<Organization>();
            Branches = new ObservableCollection<Branch>();
        }

        public WarehouseWrapper Warehouse
        {
            get { return _warehouseWrapper; }
            set
            {
                _warehouseWrapper = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Organization> Organizations { get; set; }
        public ObservableCollection<Branch> Branches { get; set; }

        public Organization SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged();
                Warehouse.Model.OrganizationID = SelectedOrganization.Id;
            }
        }
        public Branch SelectedBranch
        {
            get { return _selectedBranch; }
            set
            {
                _selectedBranch = value;
                OnPropertyChanged();
                if(Warehouse.Model.BranchId.HasValue)
                    Warehouse.Model.BranchId = SelectedBranch.Id;
            }
        }
        public override void Load(long id)
        {
            //var warehouse = id > 0
            //  ? _warehouseService.GetById(id)
            //  : CreateNewWarehouse();
            //InitializeWarehouse(warehouse);
            ResultObject resultObject;
            Warehouse warehouse;
            if (id > 0)
            {
                resultObject = Warehouse_BL.GetById(id, AppConstants.ARABIC);
                if (resultObject.Code == AppConstants.ERROR_CODE)
                {
                    _messageDialogService.ShowInfoDialog(resultObject.Message);
                    return;
                }
                warehouse = (Warehouse)resultObject.Data;
            }
            else
                warehouse = new Model.Warehouse();
            InitializeWarehouse(warehouse);

            resultObject = BusinessLayer.Organization_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Organization> organizationResultList = (ResultList<Organization>)resultObject.Data;
            if (organizationResultList.TotalCount == 0)
            {
                _messageDialogService.ShowInfoDialog(Application.Current.FindResource("no_organizations_available").ToString());
                return;
            }
            var organizations = organizationResultList.List;
            Organizations.Clear();
            FillLists(Organizations, organizations);
            SelectedOrganization =  Organizations.FirstOrDefault(o => o.Id == warehouse.OrganizationID);
           
            resultObject = BusinessLayer.Branch_BL.GetAllByOrganizationId(warehouse.OrganizationID,AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Branch> branchResultList = (ResultList<Branch>)resultObject.Data;
            var branches = branchResultList.List;
            Branches.Clear();
            FillLists(Branches, branches);
            SelectedBranch = (warehouse.BranchId.HasValue) ? Branches.FirstOrDefault(o => o.Id == warehouse.BranchId) : null;
 
        }

        private void InitializeWarehouse(Warehouse warehouse)
        {
            Warehouse = new WarehouseWrapper(warehouse);
            Warehouse.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Warehouse.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Warehouse.Id == 0)
            {
                // Little trick to trigger the validation
                Warehouse.Name = "";
            }
        }

        protected override void OnDeleteExecute()
        {
            if (_warehouseService.HasSiblings())
            {
                _messageDialogService.ShowInfoDialog($"{Warehouse.Name}  can't be deleted, as this Warehouse has at least one warehouse or warehouse.");
                return;
            }
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the Warehouse {Warehouse.Name}?", "Question");

            if (result == MessageDialogResult.Cancel)
                return;
            bool res = _warehouseService.Delete(_warehouseWrapper.Model);
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Deleted Seccessfully");
                RaiseDetailDeletedEvent(Warehouse.Id);
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Delete Failed");
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Warehouse != null && !Warehouse.HasErrors;
        }

        protected override void OnSaveExecute()
        {
            bool res = _warehouseService.Save(_warehouseWrapper.Model);
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Saved Seccessfully");
                RaiseDetailSavedEvent(Warehouse.Id, $"{Warehouse.Name}");
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Saved Failed");
            }
        }

        private Warehouse CreateNewWarehouse()
        {
            var warehouse = new Warehouse();
            return warehouse;
        }
    }
}