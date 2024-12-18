﻿using Prism.Events;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Data;
using Warehouses.UI.Wrappers;
using Warehouses.Model;
using Prism.Commands;
using Warehouses.BusinessLayer;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Popups;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class OrganizationDetailViewModel : DetailViewModelBase
    {
        private IOrganizationDataService _organizationService;
        private OrganizationWrapper _organizationWrapper;
        private IMessageDialogService _messageDialogService;

        public OrganizationDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IOrganizationDataService organizationService) : base(eventAggregator, messageDialogService)
        {
            _organizationService = organizationService;
            _messageDialogService = messageDialogService;
            EventAggregator.GetEvent<GetVoidReasonEvent>().Subscribe(OnGetVoidReason);
        }
        private void OnGetVoidReason(string voidReason)
        {
            ResultObject resultObject = Organization_BL.Delete(OrganizationWrapper.Id, voidReason, AppConstants.ARABIC);
            if(resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool res = (bool)resultObject.Data;
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Deleted Seccessfully");
                RaiseDetailDeletedEvent(OrganizationWrapper.Id);
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Delete Failed");
            }
        }
        public OrganizationWrapper OrganizationWrapper
        {
            get { return _organizationWrapper; }
            set
            {
                _organizationWrapper = value;
                OnPropertyChanged();
            }
        }

        public override void Load(long id)
        {
            //var org = id > 0
            //  ? _organizationService.GetById(id)
            //  : CreateNewOrganization();
            //InitializeOrganization(org);
            Organization organization;
            if (id > 0)
            {
                ResultObject resultObject = Organization_BL.GetById(id, AppConstants.ARABIC);
                if (resultObject.Code < AppConstants.ERROR_CODE)
                {
                    _messageDialogService.ShowInfoDialog(resultObject.Message);
                    return;
                }
                organization = (Organization)resultObject.Data;
            }
            else
                organization = new Organization();
            InitializeOrganization(organization);
        }
        private void InitializeOrganization(Organization organization)
        {
            OrganizationWrapper = new OrganizationWrapper(organization);
            OrganizationWrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(OrganizationWrapper.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (OrganizationWrapper.Id == 0)
            {
                // Little trick to trigger the validation
                OrganizationWrapper.Name = "";
            }
        }

        protected override void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the Organization {OrganizationWrapper.Name}?","Question");

            if (result == MessageDialogResult.Cancel)
                return;
            new GetReasonWindow(EventAggregator).ShowDialog();
        }

        protected override bool OnSaveCanExecute()
        {
            return OrganizationWrapper != null && !OrganizationWrapper.HasErrors;
        }

        protected override void OnSaveExecute()
        {
            ResultObject resultObject = BusinessLayer.Organization_BL.Edit(OrganizationWrapper.Id, OrganizationWrapper.Name, OrganizationWrapper.Location, UserSingleton.GetUserId(), AppConstants.ARABIC);
            if (resultObject.Code <= AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool editStatus = (bool)resultObject.Data;
            if (editStatus == true)
            {
                MessageDialogService.ShowInfoDialog("Saved Seccessfully");
                RaiseDetailSavedEvent(OrganizationWrapper.Id, $"{OrganizationWrapper.Name}");
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Saved Failed");
            }

            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                new AfterDetailSavedEventArgs
                {
                    Id = OrganizationWrapper.Id,
                    DisplayMember = OrganizationWrapper.Name,
                    ViewModelName = nameof(OrganizationDetailViewModel)
                });
        }
        private Organization CreateNewOrganization()
        {
            var organization = new Organization();
            return organization;
        }
    }
}
