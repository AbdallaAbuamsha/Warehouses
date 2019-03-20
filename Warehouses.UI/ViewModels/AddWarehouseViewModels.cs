using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;
using System.Collections.Generic;

namespace Warehouses.UI.ViewModels
{
    public class AddWarehouseViewModels : ViewModelBase, IAddWarehouseViewModels
    {
        private Organization _selectedOrganization;
        private Branch _selectedBranch;
        private string _warehouseName;
        private string _warehouseCode;
        private string _location;

        IOrganizationDataService _organizationDataService;
        IBranchDataService _branchDataService;
        public AddWarehouseViewModels(IOrganizationDataService organizationDataService, IBranchDataService branchDataService)
        {
            _organizationDataService = organizationDataService;
            _branchDataService = branchDataService;

            Organizations = new ObservableCollection<Organization>();
            Branches = new ObservableCollection<Branch>();

            Save = new DelegateCommand(ExecuteSaveOrganizationCommand);
            Close = new DelegateCommand(ExecuteCloseOrganizationCommand);           
        }

        public void Load()
        {
            var organizations = _organizationDataService.GetAll();
            var branches = _branchDataService.GetAll();

            FillLists(Organizations, organizations);
            FillLists(Branches, branches);;
        }

        private void ExecuteCloseOrganizationCommand()
        {
            MessageBox.Show("Close");
        }

        private void ExecuteSaveOrganizationCommand()
        {
            MessageBox.Show(
                SelectedOrganization.Name + "\n" +
                SelectedBranch.Name + "\n" +
                WarehouseName + "\n" +
                WarehouseCode + "\n" +
                Location + "\n"
                );
        }

        public ObservableCollection<Organization> Organizations { get; set; }

        public Organization SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Branch> Branches { get; set; }

        public Branch SelectedBranch
        {
            get { return _selectedBranch; }
            set
            {
                _selectedBranch = value;
                OnPropertyChanged();
            }
        }

        public string WarehouseName
        {
            get { return _warehouseName; }
            set
            {
                _warehouseName = value;
                OnPropertyChanged();
            }
        }

        public string WarehouseCode
        {
            get { return _warehouseCode; }
            set
            {
                _warehouseCode = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public ICommand Save { get; set; }
        public ICommand Close { get; set; }

    }
}
