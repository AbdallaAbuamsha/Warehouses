using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using System;
using Warehouses.UI.Wrappers;
using Warehouses.Model;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;
using Prism.Events;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class AddOrganizationViewModels : ViewModelBase, IAddOrganizationViewModels
    {
        //private string _organizationName;
        //private string _location;
        IEventAggregator _eventAggregator;
        private OrganizationWrapper _organizationWrapper;
        IMessageDialogService _messageDialogService;
        public AddOrganizationViewModels(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            Save = new DelegateCommand<Window>(ExecuteSaveOrganizationCommand, ExecuteCanSaveOrganizationCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);
            OrganizationWrapper = new OrganizationWrapper(new Organization { Name = "" });
            OrganizationWrapper.PropertyChanged += (s, e) =>
            {
                if(e.PropertyName.Equals(nameof(OrganizationWrapper.HasErrors)))
                {
                    ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
            OrganizationWrapper.Name = "";
            OrganizationWrapper.Location = "";

        }

        private bool ExecuteCanSaveOrganizationCommand(Window window)
        {
            return OrganizationWrapper != null && !OrganizationWrapper.HasErrors;
        }

        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteSaveOrganizationCommand(Window window)
        {
            //MessageBox.Show(OrganizationWrapper.Name + " " + OrganizationWrapper.Location);
            ResultObject resultObject = BusinessLayer.Organization_BL.Create(OrganizationWrapper.Name, OrganizationWrapper.Location, UserSingleton.GetUserId(), AppConstants.ARABIC);
            if(resultObject.Code <= AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            long organizationId = (long)resultObject.Data;
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = organizationId,
                    DisplayMember = OrganizationWrapper.Name,
                    ViewModelName = nameof(OrganizationDetailViewModel)
                });
            window.Close();

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
