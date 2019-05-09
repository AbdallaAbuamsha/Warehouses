using Autofac;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            :base(id, displayMember)
        {
            _brancheDataService = brancheDataService;
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
            _messageDialogService = messageDialogService;
            TreeItems = new ObservableCollection<TreeViewItemViewModel>();
            TreeItems.Add(null);
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
                        if (branchResult.Code == 0)
                        {
                            _messageDialogService.ShowInfoDialog(branchResult.Message);
                            return;
                        }
                        ResultList<Branch> branchListResult = (ResultList<Branch>)branchResult.Data;
                        List<Branch> branches = branchListResult.List;

                        ResultObject warehouseResult = BusinessLayer.Warehouse_BL.GetAllByOrganizationId(Id, AppConstants.ARABIC);
                        if (warehouseResult.Code == 0)
                        {
                            _messageDialogService.ShowInfoDialog(warehouseResult.Message);
                            return;
                        }
                        ResultList<Warehouse> warehouseListResult = (ResultList<Warehouse>)warehouseResult.Data;
                        List<Warehouse> warehouses = warehouseListResult.List;


                        TreeItems.Clear();
                        foreach (var branch in branches)
                        {
                            var branchItem = new WarehouseTreeViewItemVIewModel(branch.Id, branch.Name, nameof(BranchDetailViewModel), new WarehouseDataService(), _eventAggregator, _messageDialogService);
                            TreeItems.Add(branchItem);   
                        }

                        foreach (var warehouse in warehouses)
                        {
                            var branchItem = new WarehouseTreeViewItemVIewModel(warehouse.Id, warehouse.Name, nameof(WarehouseDetailViewModel), new WarehouseDataService(), _eventAggregator, _messageDialogService);
                            TreeItems.Add(branchItem);
                        }
                    }
                }
            }
        }
    }
}
