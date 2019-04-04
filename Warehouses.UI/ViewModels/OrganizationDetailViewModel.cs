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

namespace Warehouses.UI.ViewModels
{
    public class OrganizationDetailViewModel : DetailViewModelBase
    {
        private IOrganizationDataService _organizationService;

        public OrganizationDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IOrganizationDataService organizationService) : base(eventAggregator, messageDialogService)
        {
            _organizationService = organizationService;
        }

        private OrganizationWrapper _organizationWrapper;

        public OrganizationWrapper OrganizationWrapper
        {
            get { return _organizationWrapper; }
            set
            {
                _organizationWrapper = value;
                OnPropertyChanged();
            }
        }


        public override void Load(int id)
        {
            var org = id > 0
              ? _organizationService.GetById(id)
              : CreateNewOrganization();
            InitializeOrganization(org);
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
            bool res = _organizationService.Delete(_organizationWrapper.Model);
            if(res == true)
            {
                MessageDialogService.ShowInfoDialog("Deleted Seccessfully");
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
