using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.UI.Data;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    class UnitNavigationViewModel : NavigationViewModel
    {
        private IUnitDataService _unitService;
        private ObservableCollection<NavigationItemViewModel> _units;

        public UnitNavigationViewModel(
            IEventAggregator eventAggregator,
            IUnitDataService unitService)
            : base(eventAggregator)
        {
            _unitService = unitService;
            Units = new ObservableCollection<NavigationItemViewModel>();
        }

        public override void Load()
        {
            var units = _unitService.GetAll();
            Units.Clear();
            foreach (var mat in units)
            {
                Units.Add(new NavigationItemViewModel(mat.Id, mat.Name, nameof(UnitDetailViewModel), eventAggregator));
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
            eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(
            new OpenDetailViewEventArgs
            {
                ViewModelName = nameof(UnitDetailViewModel)
            });
        }

        protected override void AfterDetailDeleted(ObservableCollection<TreeViewItemViewModel> items, AfterDetailDeletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void AfterDetailSaved(ObservableCollection<TreeViewItemViewModel> items, AfterDetailSavedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
