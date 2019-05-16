using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;

namespace Warehouses.UI.ViewModels
{
    class UnitNavigationViewModel : NavigationViewModel
    {
        private IUnitDataService _unitService;
        private ObservableCollection<NavigationItemViewModel> _units;
        private IMessageDialogService _messageDialogService;
        public UnitNavigationViewModel(
            IEventAggregator eventAggregator,
            IUnitDataService unitService,
            IMessageDialogService messageDialogService)
            :base(eventAggregator)
        {
            _unitService = unitService;
            _messageDialogService = messageDialogService;
            Units = new ObservableCollection<NavigationItemViewModel>();
        }

        public override void Load()
        {
            //var units = _unitService.GetAll();
            ResultObject resultObject = BusinessLayer.Unit_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Unit> unitResultList = (ResultList<Unit>)resultObject.Data;
            if (unitResultList.TotalCount == 0)
            {
                _messageDialogService.ShowInfoDialog(Application.Current.FindResource("no_units_available").ToString());
                //return;
            }
            var units = unitResultList.List;
            Units.Clear();
            foreach (var unit in units)
            {
                NavigationItemViewModel temp = new NavigationItemViewModel(unit.Id, unit.Name, nameof(UnitDetailViewModel), eventAggregator);
                Units.Add(temp);
            }
        }

        public ObservableCollection<NavigationItemViewModel> Units
        {
            get
            {
                return _units;
            }
            set
            {
                _units = value;
                OnPropertyChanged();
            }
        }
        protected override void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(UnitDetailViewModel):
                    var item = Units.SingleOrDefault(f => f.Id == args.Id);
                    if (item != null) Units.Remove(item);
                    break;
            }
        }

        protected override void AfterDetailDeleted(ObservableCollection<TreeViewItemViewModel> items, AfterDetailDeletedEventArgs args)
        {
            //var item = items.SingleOrDefault(f => f.Id == args.Id);
            //if (item != null) items.Remove(item);
        }

        protected override void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            var lookupItem = Units.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                var newItem = new NavigationItemViewModel(
                    args.Id,
                    args.DisplayMember,
                    args.ViewModelName,
                    eventAggregator);
                Units.Add(newItem);
            }
            else
            {
                lookupItem.Name  = args.DisplayMember;
            }
        }

        protected override void AfterDetailSaved(ObservableCollection<TreeViewItemViewModel> items, AfterDetailSavedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
