using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class AddBranchViewModel : ViewModelBase, IAddBranchViewModel
    {
        private Organization _selectedOrganization;


        IOrganizationDataService _organizationDataService;
        public AddBranchViewModel(IOrganizationDataService organizationDataService)
        {
            _organizationDataService = organizationDataService;

            Organizations = new ObservableCollection<Organization>();
            Branch = new BranchWrapper(new Model.Branch());

            Save = new DelegateCommand(ExecuteSaveOrganizationCommand, ExecuteCanSaveOrganizationCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);

            Branch.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(OrganizationWrapper.HasErrors)))
                {


                    ((DelegateCommand)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)Save).RaiseCanExecuteChanged();
        }

        private bool ExecuteCanSaveOrganizationCommand()
        {
         return Branch != null && !Branch.HasErrors && SelectedOrganization != null;
        }

        public void Load()
        {
            var organizations = _organizationDataService.GetAll();

            FillLists(Organizations, organizations);
        }

        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }
        private void ExecuteSaveOrganizationCommand()
        {
            MessageBox.Show(
                SelectedOrganization.Name + "\n" +
                Branch.Name + "\n" +
                Branch.Code + "\n" +
                Branch.Location + "\n"
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

        public BranchWrapper Branch { get; set; }

        public ICommand Save { get; set; }
        public ICommand Close { get; set; }

    }
}