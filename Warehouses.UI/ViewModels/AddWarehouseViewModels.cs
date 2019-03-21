using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;
using System.Collections.Generic;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class AddWarehouseViewModels : ViewModelBase, IAddWarehouseViewModels
    {
        private Organization _selectedOrganization;
        private Branch _selectedBranch;

        IOrganizationDataService _organizationDataService;
        IBranchDataService _branchDataService;
        public AddWarehouseViewModels(IOrganizationDataService organizationDataService, IBranchDataService branchDataService)
        {
            _organizationDataService = organizationDataService;
            _branchDataService = branchDataService;
            
            Organizations = new ObservableCollection<Organization>();
            Branches = new ObservableCollection<Branch>();
            Warehouse = new WarehouseWrapper(new Model.Warehouse());
            Save = new DelegateCommand(ExecuteSaveOrganizationCommand, ExecuteCanSaveOrganizationCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);
            Warehouse.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(Warehouse.HasErrors)))
                {
                    ((DelegateCommand)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)Save).RaiseCanExecuteChanged();
        }

        private bool ExecuteCanSaveOrganizationCommand()
        {
            return SelectedOrganization != null && !Warehouse.HasErrors;
        }

        public void Load()
        {
            var organizations = _organizationDataService.GetAll();
            var branches = _branchDataService.GetAll();

            FillLists(Organizations, organizations);
            FillLists(Branches, branches);;
        }

        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteSaveOrganizationCommand()
        {
            MessageBox.Show(
                SelectedOrganization.Name + "\n" +
                SelectedBranch.Name + "\n" +
                Warehouse.Name + "\n" +
                Warehouse.Code + "\n" +
                Warehouse.Location + "\n"
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

        public WarehouseWrapper Warehouse { get; set; }

        public ICommand Save { get; set; }
        public ICommand Close { get; set; }

    }
}
