using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System;
using Warehouses.UI.Wrappers;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public class AddOrganizationViewModels : ViewModelBase, IAddOrganizationViewModels
    {
        //private string _organizationName;
        //private string _location;
        private OrganizationWrapper _organizationWrapper;

        public AddOrganizationViewModels()
        {
            Save = new DelegateCommand(ExecuteSaveOrganizationCommand, ExecuteCanSaveOrganizationCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);
            OrganizationWrapper = new OrganizationWrapper(new Organization { Name = "" });
            OrganizationWrapper.PropertyChanged += (s, e) =>
            {
                if(e.PropertyName.Equals(nameof(OrganizationWrapper.HasErrors)))
                {
                    ((DelegateCommand)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)Save).RaiseCanExecuteChanged();
            OrganizationWrapper.Name = "";
            OrganizationWrapper.Location = "";

        }

        private bool ExecuteCanSaveOrganizationCommand()
        {
            return OrganizationWrapper != null && !OrganizationWrapper.HasErrors;
        }

        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteSaveOrganizationCommand()
        {
            MessageBox.Show(OrganizationWrapper.Name + " " + OrganizationWrapper.Location);
        }

        public OrganizationWrapper OrganizationWrapper
        {
            get { return _organizationWrapper; }
            set { _organizationWrapper = value; }
        }

        public ICommand Save { get; set; }
        public ICommand Close { get; set; }

    }
}
