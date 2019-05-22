using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;
using System.Collections.Generic;
using Warehouses.UI.Wrappers;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;
using Prism.Events;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class AddWarehouseViewModels : ViewModelBase, IAddWarehouseViewModels
    {
        IMessageDialogService _messageDialogService;
        IEventAggregator _eventAggregator;
        private Organization _selectedOrganization;
        private Branch _selectedBranch;

        IOrganizationDataService _organizationDataService;
        IBranchDataService _branchDataService;
        private Warehouse _selectedWarehouse;

        public AddWarehouseViewModels(
            IOrganizationDataService organizationDataService,
            IBranchDataService branchDataService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _organizationDataService = organizationDataService;
            _branchDataService = branchDataService;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            Organizations = new ObservableCollection<Organization>();
            Branches = new ObservableCollection<Branch>();
            Warehouses = new ObservableCollection<Model.Warehouse>();
            Warehouse = new WarehouseWrapper(new Model.Warehouse());
            Save = new DelegateCommand<Window>(ExecuteSaveOrganizationCommand, ExecuteCanSaveOrganizationCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);
            Warehouse.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(Warehouse.HasErrors)))
                {
                    ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
            Warehouse.Name = "";
            Warehouse.Code = "";
            Warehouse.Location = "";
        }

        private bool ExecuteCanSaveOrganizationCommand(Window window)
        {
            return SelectedOrganization != null && !Warehouse.HasErrors;
        }

        public void Load()
        {
            //var organizations = _organizationDataService.GetAll();
            //var branches = _branchDataService.GetAll();

            //FillLists(Organizations, organizations);
            //FillLists(Branches, branches);;
            ResultObject resultObject = BusinessLayer.Organization_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
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
            FillLists(Organizations, organizations);
        }

        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteSaveOrganizationCommand(Window window)
        {
            //MessageBox.Show(
            //    SelectedOrganization.Name + "\n" +
            //    SelectedBranch.Name + "\n" +
            //    Warehouse.Name + "\n" +
            //    Warehouse.Code + "\n" +
            //    Warehouse.Location + "\n"
            //    );
            //int parentType = 
            //    (SelectedWarehouse != null)? AppConstants.WAREHOUSE_PARENT_TYPE :
            //    (SelectedBranch != null) ? AppConstants.BRANCH_PARENT_TYPE :
            //    AppConstants.ORGANIZATION_PARENT_TYPE;
            //long? branchId = (SelectedBranch != null)? SelectedBranch.Id: null;
            long? branchId = null, parentWarehouseId = null;
            byte parentType = AppConstants.ORGANIZATION_PARENT_TYPE;
            if(SelectedBranch != null)
            {
                branchId = SelectedBranch.Id;
                parentType = AppConstants.BRANCH_PARENT_TYPE;
            }
            if(SelectedWarehouse != null)
            {
                parentWarehouseId = SelectedWarehouse.Id;
                parentType = AppConstants.WAREHOUSE_PARENT_TYPE;
            }
            ResultObject resultObject = BusinessLayer.Warehouse_BL.Create(Warehouse.Name, Warehouse.LatinName, Warehouse.Location, Warehouse.Code, parentType, SelectedOrganization.Id, branchId, parentWarehouseId, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            long warehouseId = (long)resultObject.Data;
            _eventAggregator.GetEvent<ExpandTreeItemEvent>().Publish(
                new ExpandTreeItemEventArgs
                {
                    Id = SelectedOrganization.Id,
                    ViewModelName = (nameof(OrganizationDetailViewModel))
                });
            if (SelectedBranch != null)
                _eventAggregator.GetEvent<ExpandTreeItemEvent>().Publish(
                new ExpandTreeItemEventArgs
                {
                    Id = SelectedBranch.Id,
                    ViewModelName = (nameof(BranchDetailViewModel))
                });
            if (SelectedWarehouse != null)
                _eventAggregator.GetEvent<ExpandTreeItemEvent>().Publish(
                new ExpandTreeItemEventArgs
                {
                    Id = SelectedWarehouse.Id,
                    ViewModelName = (nameof(WarehouseDetailViewModel))
                });
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = warehouseId,
                    DisplayMember = Warehouse.Name,
                    ViewModelName = nameof(WarehouseDetailViewModel)
                });
            window.Close();
        }

        public ObservableCollection<Organization> Organizations { get; set; }

        public Organization SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged();
                getSelectedOrganizationBranches();
                getSelectedOrganizationWarehouses();
            }
        }

        private void getSelectedOrganizationWarehouses()
        {
            ResultObject branchResult = BusinessLayer.Warehouse_BL.GetAllByOrganizationId(SelectedOrganization.Id, AppConstants.ARABIC);
            if (branchResult.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(branchResult.Message);
                return;
            }
            ResultList<Warehouse> warehouseListResult = (ResultList<Warehouse>)branchResult.Data;
            List<Warehouse> warehouses = warehouseListResult.List;
            FillLists(Warehouses, warehouses);
        }

        private void getSelectedOrganizationBranches()
        {
            ResultObject branchResult = BusinessLayer.Branch_BL.GetAllByOrganizationId(SelectedOrganization.Id, AppConstants.ARABIC);
            if (branchResult.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(branchResult.Message);
                return;
            }
            ResultList<Branch> branchListResult = (ResultList<Branch>)branchResult.Data;
            List<Branch> branches = branchListResult.List;
            FillLists(Branches, branches);
        }

        public ObservableCollection<Branch> Branches { get; set; }

        public Branch SelectedBranch
        {
            get { return _selectedBranch; }
            set
            {
                _selectedBranch = value;
                OnPropertyChanged();
                getSelectedBranchWarehouses();

            }
        }
        private void getSelectedBranchWarehouses()
        {
            ResultObject warehouseResult = BusinessLayer.Warehouse_BL.GetAllByBranchId(SelectedBranch.Id, AppConstants.ARABIC);
            if (warehouseResult.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(warehouseResult.Message);
                return;
            }
            ResultList<Warehouse> warehouseListResult = (ResultList<Warehouse>)warehouseResult.Data;
            List<Warehouse> warehouses = warehouseListResult.List;
            FillLists(Warehouses, warehouses);
        }
        public ObservableCollection<Warehouse> Warehouses { get; set; }

        public Warehouse SelectedWarehouse
        {
            get { return _selectedWarehouse; }
            set
            {
                _selectedWarehouse = value;
                OnPropertyChanged();
            }
        }        
        public WarehouseWrapper Warehouse { get; set; }

        public ICommand Save { get; set; }
        public ICommand Close { get; set; }

    }
}
