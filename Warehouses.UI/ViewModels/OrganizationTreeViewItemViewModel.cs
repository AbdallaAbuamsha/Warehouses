using Autofac;
using Prism.Events;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Startup;

namespace Warehouses.UI.ViewModels
{
    public class OrganizationTreeViewItemViewModel : TreeViewItemViewModel
    {
        IBranchDataService _brancheDataService;
        IEventAggregator _eventAggregator;
        private bool _isSelected;
        private bool _isExpanded;
        private string _detailViewModelName;
        public OrganizationTreeViewItemViewModel(
            long id,
            string displayMember,
            string detailViewModelName,
            IBranchDataService brancheDataService,
            IEventAggregator eventAggregator)
            :base(id, displayMember)
        {
            _brancheDataService = brancheDataService;
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
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
                        var branches = _brancheDataService.GetByParentId(Id);
                        TreeItems.Clear();
                        foreach (var branch in branches)
                        {
                            var branchItem = new BranchTreeViewItemViewModel(branch.Id, branch.Name, nameof(BranchDetailViewModel), new WarehouseDataService(), _eventAggregator);
                            TreeItems.Add(branchItem);   
                        }
                    }
                }
            }
        }
    }
}
