using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Data;
using Warehouses.UI.Wrappers;
using Warehouses.Model;
using Prism.Commands;
using Warehouses.BusinessLayer;
using Warehouses.UI.Helper;

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
                if (resultObject.Code == AppConstants.ERROR_CODE)
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
            if (_organizationService.HasSiblings())
            {
                _messageDialogService.ShowInfoDialog($"{OrganizationWrapper.Name}  can't be deleted, as this Organization has at least one branch or warehouse.");
                return;
            }
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the Organization {OrganizationWrapper.Name}?","Question");

            if (result == MessageDialogResult.Cancel)
                return;
            bool res = _organizationService.Delete(_organizationWrapper.Model);
            if(res == true)
            {
                MessageDialogService.ShowInfoDialog("Deleted Seccessfully");
                RaiseDetailDeletedEvent(OrganizationWrapper.Id);
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Delete Failed");
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return OrganizationWrapper != null && !OrganizationWrapper.HasErrors;
        }

        protected override void OnSaveExecute()
        {
            bool res = _organizationService.Save(_organizationWrapper.Model);
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Saved Seccessfully");
                RaiseDetailSavedEvent(OrganizationWrapper.Id, $"{OrganizationWrapper.Name}");
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Saved Failed");
            }
        }
        private Organization CreateNewOrganization()
        {
            var organization = new Organization();
            return organization;
        }
    }
}
