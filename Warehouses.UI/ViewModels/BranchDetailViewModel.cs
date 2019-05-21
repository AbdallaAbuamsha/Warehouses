using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouses.BusinessLayer;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Popups;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class BranchDetailViewModel : DetailViewModelBase
    {
        private IBranchDataService _branchService;
        private BranchWrapper _branchWrapper;
        private IMessageDialogService _messageDialogService;
        private Organization _selectedOrganization;

        public BranchDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IBranchDataService branchService) : base(eventAggregator, messageDialogService)
        {
            _branchService = branchService;
            _messageDialogService = messageDialogService;
            EventAggregator.GetEvent<GetVoidReasonEvent>().Subscribe(OnGetVoidReason);
            Organizations = new ObservableCollection<Organization>();
        }

        public BranchWrapper Branch
        {
            get { return _branchWrapper; }
            set
            {
                _branchWrapper = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Organization> Organizations { get; set; }

        public Organization SelectedOrganization
        {
            get { return _selectedOrganization; }
            set
            {
                _selectedOrganization = value;
                OnPropertyChanged();
                Branch.Model.ParentId = SelectedOrganization.Id;
            }
        }

        public override void Load(long id)
        {
            //var branch = id > 0
            //  ? _branchService.GetById(id)
            //  : CreateNewBranch();
            //InitializeBranch(branch);
            ResultObject resultObject;
            Branch branch;
            if (id > 0)
            {
                resultObject = Branch_BL.GetById(id, AppConstants.ARABIC);
                if (resultObject.Code == AppConstants.ERROR_CODE)
                {
                    _messageDialogService.ShowInfoDialog(resultObject.Message);
                    return;
                }
                branch = (Branch)resultObject.Data;
            }
            else
                branch = new Model.Branch();
            InitializeBranch(branch);

            //var organizations = _branchService.GetAllOrganizations();
            resultObject = BusinessLayer.Organization_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Organization> organizationResultList = (ResultList<Organization>)resultObject.Data;
            if (organizationResultList.TotalCount == 0)
            {
                _messageDialogService.ShowInfoDialog(Application.Current.FindResource("no_organizations_available").ToString());
                return;
            }
            var organizations = organizationResultList.List;
            Organizations.Clear();
            FillLists(Organizations, organizations);
            SelectedOrganization = Organizations.FirstOrDefault(o => o.Id == branch.ParentId);

        }

        private void InitializeBranch(Branch branch)
        {
            Branch = new BranchWrapper(branch);
            Branch.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Branch.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Branch.Id == 0)
            {
                // Little trick to trigger the validation
                Branch.Name = "";
            }
        }

        protected override void OnDeleteExecute()
        {
            //var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the Branch {Branch.Name}?", "Question");

            //if (result == MessageDialogResult.Cancel)
            //    return;
            new GetReasonWindow(EventAggregator).ShowDialog();
        }
        private void OnGetVoidReason(string voidReason)
        {
            ResultObject resultObject = Branch_BL.Delete(Branch.Id, voidReason, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool res = (bool)resultObject.Data;
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Deleted Seccessfully");
                RaiseDetailDeletedEvent(Branch.Id);
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Delete Failed");
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Branch != null && !Branch.HasErrors;
        }

        protected override void OnSaveExecute()
        {
            ResultObject resultObject = BusinessLayer.Branch_BL.Edit(Branch.Id, Branch.Name, Branch.Location, UserSingleton.GetUserId(), AppConstants.ARABIC);
            if (resultObject.Code <= AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool editStatus = (bool)resultObject.Data;
            if (editStatus == true)
            {
                MessageDialogService.ShowInfoDialog("Saved Seccessfully");
                RaiseDetailSavedEvent(Branch.Id, $"{Branch.Name}");
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Saved Failed");
            }
            EventAggregator.GetEvent<ExpandTreeItemEvent>().Publish(
            new ExpandTreeItemEventArgs
            {
                Id = SelectedOrganization.Id,
                ViewModelName = (nameof(OrganizationDetailViewModel))
            });
                    EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
                        new AfterDetailSavedEventArgs
                        {
                            Id = Branch.Id,
                            DisplayMember = Branch.Name,
                            ViewModelName = nameof(BranchDetailViewModel)
                        });
        }

        private Branch CreateNewBranch()
        {
            var branch = new Branch();
            return branch;
        }
    }
}
