using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;

namespace Warehouses.UI.ViewModels
{
    public class AddBranchViewModel : ViewModelBase, IAddBranchViewModel
    {
        private Organization _selectedOrganization;
        private string _branchName;
        private string _branchCode;
        private string _location;

        IOrganizationDataService _organizationDataService;
        public AddBranchViewModel(IOrganizationDataService organizationDataService)
        {
            _organizationDataService = organizationDataService;

            Organizations = new ObservableCollection<Organization>();

            Save = new DelegateCommand(ExecuteSaveOrganizationCommand);
            Close = new DelegateCommand(ExecuteCloseOrganizationCommand);
        }

        public void Load()
        {
            var organizations = _organizationDataService.GetAll();

            FillLists(Organizations, organizations);
        }

        private void FillLists<T>(ObservableCollection<T> empty, IEnumerable<T> filled)
        {
            foreach (var item in filled)
            {
                empty.Add(item);
            }
        }
        private void ExecuteCloseOrganizationCommand()
        {
            MessageBox.Show("Close");
        }
        private void ExecuteSaveOrganizationCommand()
        {
            MessageBox.Show(
                SelectedOrganization.Name + "\n" +
                BranchName + "\n" +
                BranchCode + "\n" +
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

        public string BranchName
        {
            get { return _branchName; }
            set
            {
                _branchName = value;
                OnPropertyChanged();
            }
        }

        public string BranchCode
        {
            get { return _branchCode; }
            set
            {
                _branchCode = value;
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