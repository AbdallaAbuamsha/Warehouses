using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class AddBranchViewModel : ViewModelBase, IAddBranchViewModel
    {
        private Organization _selectedOrganization;
        IMessageDialogService _messageDialogService;
        IEventAggregator _eventAggregator;
        IOrganizationDataService _organizationDataService;
        public AddBranchViewModel(IOrganizationDataService organizationDataService, 
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _organizationDataService = organizationDataService;
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            Organizations = new ObservableCollection<Organization>();
            Branch = new BranchWrapper(new Model.Branch());

            Save = new DelegateCommand<Window>(ExecuteSaveOrganizationCommand, ExecuteCanSaveOrganizationCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);

            Branch.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(OrganizationWrapper.HasErrors)))
                {


                    ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
            Branch.Name = "";
            Branch.Location = "";
        }

        private bool ExecuteCanSaveOrganizationCommand(Window window)
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
        private void ExecuteSaveOrganizationCommand(Window window)
        {
            //MessageBox.Show(
            //    SelectedOrganization.Name + "\n" +
            //    Branch.Name + "\n" +
            //    Branch.Location + "\n"
            //    );
            ResultObject resultObject = BusinessLayer.Branch_BL.Create(Branch.Name, Branch.Location, SelectedOrganization.Id, AppConstants.ARABIC);
            if (resultObject.Code <= AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            long branchId = (long)resultObject.Data;
            _eventAggregator.GetEvent<ExpandTreeItemEvent>().Publish(
                new ExpandTreeItemEventArgs
                {
                    Id = SelectedOrganization.Id,
                    ViewModelName = (nameof(OrganizationDetailViewModel))
                });
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = branchId,
                    DisplayMember = Branch.Name,
                    ViewModelName = nameof(BranchDetailViewModel)
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
            }
        }

        public BranchWrapper Branch { get; set; }

        public ICommand Save { get; set; }
        public ICommand Close { get; set; }

    }
}