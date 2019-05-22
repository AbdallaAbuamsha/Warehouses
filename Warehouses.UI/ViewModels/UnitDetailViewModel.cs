using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class UnitDetailViewModel : DetailViewModelBase
    {
        private IUnitDataService _unitService;
        IMessageDialogService _messageDialogService;
        private UnitWrapper _unitWrapper;
        private bool _systemHasOneParentAtLeast;
        private Unit _parentUnit;
        public bool _parentSelected;

        public UnitDetailViewModel(
            IUnitDataService unitService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
            _unitService = unitService;
            _messageDialogService = messageDialogService;
            Names = new ObservableCollection<string>();
            Units = new ObservableCollection<Unit>();
            EventAggregator.GetEvent<GetVoidReasonEvent>().Subscribe(OnGetVoidReason);

        }

        public ObservableCollection<String> Names { get; set; }
        public ObservableCollection<Unit> Units { get; set; }
        public bool SystemHasOneParentAtLeast
        {
            get { return _systemHasOneParentAtLeast; }
            set
            {
                _systemHasOneParentAtLeast = value;
                OnPropertyChanged();
            }
        }
        public bool ParentSelected
        {
            get
            {
                return _parentSelected;
            }
            set
            {
                _parentSelected = value;
                OnPropertyChanged();
            }
        }
        public Unit ParentUnit
        {
            get
            {
                return _parentUnit;
            }
            set
            {
                _parentUnit = value;
                if (_parentUnit != null)
                    ParentSelected = true;
                else
                    ParentSelected = false;
                OnPropertyChanged();
            }
        }
        public UnitWrapper UnitWrapper
        {
            get { return _unitWrapper; }
            set
            {
                _unitWrapper = value;
                OnPropertyChanged();
            }
        }
        public override void Load(long id)
        {
            ResultObject resultObject = BusinessLayer.Unit_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Unit> unitResultList = (ResultList<Unit>)resultObject.Data;
            if (unitResultList.TotalCount == 0)
            {
                SystemHasOneParentAtLeast = false;
                //_messageDialogService.ShowInfoDialog(Application.Current.FindResource("no_organizations_available").ToString());
                //return;
            }
            else
            {
                SystemHasOneParentAtLeast = true;
            }
            var units = unitResultList.List;
            //var organizations = _organizationDataService.GetAll();
            resultObject = Unit_BL.GetById(id, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            Unit unit = (Unit)resultObject.Data;
            // Remove un wanted items from the parents list (the same item and it's children)
            foreach (var item in units.ToList())
            {
                if (item.ParentUnitId == unit.Id || item.Id == unit.Id)
                    units.Remove(item);

            }
            Units.Clear();
            FillLists(Units, units);            
            InitializeUnit(unit);
        }

        private void InitializeUnit(Unit unit)
        {
            UnitWrapper = new UnitWrapper(unit);
            UnitWrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(UnitWrapper.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (UnitWrapper.Id == 0)
            {
                // Little trick to trigger the validation
                UnitWrapper.Name = "";
            }
            if(unit.ParentUnitId.HasValue)
            {
                foreach (var item in Units)
                {
                    if(item.Id.Equals(unit.ParentUnitId))
                    {
                        ParentUnit = item;
                        break;
                    }
                }
            }
        }

        protected override void OnDeleteExecute()
        {
            new GetReasonWindow(EventAggregator).ShowDialog();
        }
        private void OnGetVoidReason(string voidReason)
        {
            ResultObject resultObject = Unit_BL.Delete(UnitWrapper.Id, voidReason, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool res = (bool)resultObject.Data;
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Deleted Seccessfully");
                RaiseDetailDeletedEvent(UnitWrapper.Id);
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Delete Failed");
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return UnitWrapper != null && !UnitWrapper.HasErrors;
        }

        protected override void OnSaveExecute()
        {
        }
        private Unit CreateNewUnit()
        {
            var unit = new Unit();
            return unit;
        }


        
    }
}
