﻿using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Warehouses.BusinessLayer;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Popups;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Wrappers;
using System;

namespace Warehouses.UI.ViewModels
{
    public class WarehouseDetailViewModel : DetailViewModelBase
    {
        private IWarehouseDataService _warehouseService;
        private WarehouseWrapper _warehouseWrapper;
        private IMessageDialogService _messageDialogService;
        private Organization _selectedOrganization;
        private Branch _selectedBranch;
        private Warehouse _selectedWarehouse;

        public WarehouseDetailViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IWarehouseDataService warehouseService) : base(eventAggregator, messageDialogService)
        {
            _warehouseService = warehouseService;
            _messageDialogService = messageDialogService;
            EventAggregator.GetEvent<GetVoidReasonEvent>().Subscribe(OnGetVoidReason);

            Organizations = new ObservableCollection<Organization>();
            Branches = new ObservableCollection<Branch>();
            Warehouses = new ObservableCollection<Model.Warehouse>();

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

        public ObservableCollection<Warehouse> Warehouses { get; set; }


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
        public Warehouse SelectedWarehouse
        {
            get { return _selectedWarehouse; }
            set
            {
                _selectedWarehouse = value;
                OnPropertyChanged();
                if (Warehouse.Model.ParentWarehouseId.HasValue)
                    Warehouse.Model.ParentWarehouseId = SelectedWarehouse.Id;
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
                if (resultObject.Code < AppConstants.ERROR_CODE)
                {
                    _messageDialogService.ShowInfoDialog(resultObject.Message);
                    return;
                }
                warehouse = (Warehouse)resultObject.Data;
            }
            else
                warehouse = new Model.Warehouse();
            InitializeWarehouse(warehouse);

            GetOrganizations();
            SelectedOrganization =  Organizations.FirstOrDefault(o => o.Id == warehouse.OrganizationID);

            GetBranches();
            GetWarehouses();
            SelectedBranch = (warehouse.BranchId.HasValue) ? Branches.FirstOrDefault(o => o.Id == warehouse.BranchId) : null;
            SelectedWarehouse = (warehouse.ParentWarehouseId.HasValue) ? Warehouses.FirstOrDefault(w => w.Id == warehouse.ParentWarehouseId) : null;


        }

        private void GetWarehouses()
        {
            ResultObject resultObject;
            if (SelectedBranch != null)
                resultObject = BusinessLayer.Warehouse_BL.GetAllByBranchId(SelectedBranch.Id, AppConstants.ARABIC);            
            else
                resultObject = BusinessLayer.Warehouse_BL.GetAllByOrganizationId(SelectedOrganization.Id, AppConstants.ARABIC);
                
            //ResultObject resultObject = BusinessLayer.Warehouse_BL.GetAllByOrganizationId(pareintId, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Warehouse> warehouseResultList = (ResultList<Warehouse>)resultObject.Data;
            var warehouses = warehouseResultList.List;
            Warehouses.Clear();
            FillLists(Warehouses, warehouses);
        }

        private void GetBranches()
        {
            ResultObject resultObject = BusinessLayer.Branch_BL.GetAllByOrganizationId(Warehouse.Model.OrganizationID, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Branch> branchResultList = (ResultList<Branch>)resultObject.Data;
            var branches = branchResultList.List;
            Branches.Clear();
            FillLists(Branches, branches);
        }

        private void GetOrganizations()
        {
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
            Organizations.Clear();
            FillLists(Organizations, organizations);
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
            new GetReasonWindow(EventAggregator).ShowDialog();
        }
        private void OnGetVoidReason(string voidReason)
        {
            ResultObject resultObject = Warehouse_BL.Delete(Warehouse.Id, voidReason, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool res = (bool)resultObject.Data;
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
            ResultObject resultObject = BusinessLayer.Warehouse_BL.Edit(Warehouse.Id, Warehouse.Name, Warehouse.LatinName, Warehouse.Location, Warehouse.Code, AppConstants.ARABIC);
            if (resultObject.Code <= AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool editStatus = (bool)resultObject.Data;
            if (editStatus == true)
            {
                MessageDialogService.ShowInfoDialog("Saved Seccessfully");
                RaiseDetailSavedEvent(Warehouse.Id, $"{Warehouse.Name}");
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Saved Failed");
            }
            if (SelectedBranch != null)
                EventAggregator.GetEvent<ExpandTreeItemEvent>().Publish(
                new ExpandTreeItemEventArgs
                {
                    Id = SelectedBranch.Id,
                    ViewModelName = (nameof(BranchDetailViewModel))
                });
            if (SelectedWarehouse != null)
                EventAggregator.GetEvent<ExpandTreeItemEvent>().Publish(
                new ExpandTreeItemEventArgs
                {
                    Id = SelectedWarehouse.Id,
                    ViewModelName = (nameof(WarehouseDetailViewModel))
                });
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = Warehouse.Id,
                    DisplayMember = Warehouse.Name,
                    ViewModelName = nameof(WarehouseDetailViewModel)
                });
        }

        private Warehouse CreateNewWarehouse()
        {
            var warehouse = new Warehouse();
            return warehouse;
        }
    }
}