﻿using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;
using Warehouses.UI.Data;
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
            var branch = id > 0
              ? _branchService.GetById(id)
              : CreateNewBranch();
            InitializeBranch(branch);
            var organizations = _branchService.GetAllOrganizations();
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
            if (_branchService.HasSiblings())
            {
                _messageDialogService.ShowInfoDialog($"{Branch.Name}  can't be deleted, as this Branch has at least one branch or warehouse.");
                return;
            }
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the Branch {Branch.Name}?", "Question");

            if (result == MessageDialogResult.Cancel)
                return;
            bool res = _branchService.Delete(_branchWrapper.Model);
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
            bool res = _branchService.Save(_branchWrapper.Model);
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Saved Seccessfully");
                RaiseDetailSavedEvent(Branch.Id, $"{Branch.Name}");
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Saved Failed");
            }
        }

        private Branch CreateNewBranch()
        {
            var branch = new Branch();
            return branch;
        }
    }
}