using Autofac;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Startup;
using Warehouses.UI.Views.Services;

namespace Warehouses.UI.ViewModels
{
    public class OrganizationTreeViewItemViewModel : TreeViewItemViewModel
    {
        IBranchDataService _brancheDataService;
        IEventAggregator _eventAggregator;
        IMessageDialogService _messageDialogService;
        private bool _isSelected;
        private bool _isExpanded;
        private string _detailViewModelName;
        public OrganizationTreeViewItemViewModel(
            long id,
            string displayMember,
            string detailViewModelName,
            IBranchDataService brancheDataService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            : base(id, displayMember)
        {
            _brancheDataService = brancheDataService;
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<ExpandTreeItemEvent>().Subscribe(OnExpandTreeItem);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            TreeItems = new ObservableCollection<TreeViewItemViewModel>();
            TreeItems.Add(null);
        }
        protected void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            if (TreeItems.Count == 1 && TreeItems[0] == null)
                return;
            switch (args.ViewModelName)
            {
                case nameof(BranchDetailViewModel):
                    AfterBranchDetailDeleted(TreeItems, args);
                    break;
                case nameof(WarehouseDetailViewModel):
                    AfterWarehouseDetailDeleted(TreeItems, args);
                    break;
            }
        }
        protected void AfterBranchDetailDeleted(ObservableCollection<TreeViewItemViewModel> items,
                                            AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id && f is BranchTreeViewItemViewModel);
            if (item != null)
            {
                items.Remove(item);
            }
        }
        protected void AfterWarehouseDetailDeleted(ObservableCollection<TreeViewItemViewModel> items,
                                    AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id && f is WarehouseTreeViewItemViewModel);
            if (item != null)
            {
                items.Remove(item);
            }
        }
        private void OnExpandTreeItem(ExpandTreeItemEventArgs args)
        {
            if (args.ViewModelName.Equals(_detailViewModelName) && args.Id == Id)
            {
                if ((IsExpanded == true) && (TreeItems.Count == 0))
                    IsExpanded = false;
                IsExpanded = true;
            }
        }
    
        private void OrganizationSelected(OrganizationTreeViewItemViewModel organization)
        {
            if(organization.Id == Id)
            {
                IsSelected = true;
                IsExpanded = true;
            }
        }      


        public ObservableCollection<TreeViewItemViewModel> TreeItems { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                    if (_isSelected)
                    {
                        // _eventAggregator.GetEvent<OrganizationTreeItemSelectedEvent>().Publish(this);
                        _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(
                            new OpenDetailViewEventArgs
                            {
                                Id = this.Id,
                                ViewModelName = _detailViewModelName
                            });
                    }
                }
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                    if (_isExpanded)
                    {
                        //var branches = _brancheDataService.GetByParentId(Id);
                        ResultObject branchResult = BusinessLayer.Branch_BL.GetAllByOrganizationId(Id, AppConstants.ARABIC);
                        if (branchResult.Code == AppConstants.ERROR_CODE)
                        {
                            _messageDialogService.ShowInfoDialog(branchResult.Message);
                            return;
                        }
                        ResultList<Branch> branchListResult = (ResultList<Branch>)branchResult.Data;
                        List<Branch> branches = branchListResult.List;

                        ResultObject warehouseResult = BusinessLayer.Warehouse_BL.GetAllByOrganizationId(Id, AppConstants.ARABIC);
                        if (warehouseResult.Code == AppConstants.ERROR_CODE)
                        {
                            _messageDialogService.ShowInfoDialog(warehouseResult.Message);
                            return;
                        }
                        ResultList<Warehouse> warehouseListResult = (ResultList<Warehouse>)warehouseResult.Data;
                        List<Warehouse> warehouses = warehouseListResult.List;


                        TreeItems.Clear();
                        foreach (var branch in branches)
                        {
                            var branchItem = new BranchTreeViewItemViewModel(branch.Id, branch.Name, nameof(BranchDetailViewModel), new WarehouseDataService(), _eventAggregator, _messageDialogService);
                            TreeItems.Add(branchItem);   
                        }

                        foreach (var warehouse in warehouses)
                        {
                            var branchItem = new WarehouseTreeViewItemViewModel(warehouse.Id, warehouse.Name, nameof(WarehouseDetailViewModel), new WarehouseDataService(), _eventAggregator, _messageDialogService);
                            TreeItems.Add(branchItem);
                        }
                    }
                }
            }
        }
    }
}
