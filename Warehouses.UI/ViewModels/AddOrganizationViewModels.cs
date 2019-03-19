using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System;

namespace Warehouses.UI.ViewModels
{
    public class AddOrganizationViewModels : ViewModelBase, IAddOrganizationViewModels
    {
        private string _organizationName;
        private string _location;

        public AddOrganizationViewModels()
        {
            Save = new DelegateCommand(ExecuteSaveOrganizationCommand);
            Close = new DelegateCommand(ExecuteCloseOrganizationCommand);
        }

        private void ExecuteCloseOrganizationCommand()
        {
            MessageBox.Show("Close");
        }

        private void ExecuteSaveOrganizationCommand()
        {
            MessageBox.Show(OrganizationName + " " + Location);
        }

        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                _organizationName = value;
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
