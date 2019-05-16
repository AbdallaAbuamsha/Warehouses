using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    class AddUnitViewModel : ViewModelBase, IAddUnitViewModel
    {
        IMessageDialogService _messageDialogService;
        IEventAggregator _eventAggregator;
        private UnitWrapper _myUnit;
        private IUnitDataService _unitService;
        private bool _systemHasOneParentAtLeast;
        private Unit _parentUnit;
        public bool _parentSelected;

        public AddUnitViewModel(
            IUnitDataService unitService,
            IAddUnitRelationViewModel addUnitRelationViewModel,
            IMessageDialogService messageDialogService,
            IEventAggregator eventAggregator)
        {
            _unitService = unitService;
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            //MyAddUnitRelationViewModel = addUnitRelationViewModel;
            MyUnit = new UnitWrapper(new Unit());
            Save = new DelegateCommand<Window>(ExecuteSaveCommand, ExecuteCanSaveCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseCommand);
            Units = new ObservableCollection<Unit>();
            MyUnit.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(MyUnit.HasErrors)))
                {
                    ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
            MyUnit.Name = "";
            MyUnit.Factor = null;
        }

        public void Load()
        {
            //MyAddUnitRelationViewModel.Load();
            ResultObject resultObject = BusinessLayer.Unit_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
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
            Units.Clear();
            FillLists(Units, units);
        }

        //public IAddUnitRelationViewModel MyAddUnitRelationViewModel { get; set; }
        public ObservableCollection<Unit> Units { get; set; }
        public UnitWrapper MyUnit
        {
            get
            { return _myUnit; }
            set
            {
                _myUnit = value;
                OnPropertyChanged();
                if(Save != null)
                    ((DelegateCommand)Save).RaiseCanExecuteChanged();
            }
        }
            
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
            set { _parentUnit = value;
                if (_parentUnit != null)
                    ParentSelected = true;
                else
                    ParentSelected = false;
                OnPropertyChanged();
            }
        }
        public ICommand Save { get; set; }

        public ICommand Close { get; set; }

        private void ExecuteSaveCommand(Window window)
        {
            //int newUnitId = _unitService.Save(MyUnit.Model);
            long? parentId = null;
            if (ParentUnit != null) parentId = ParentUnit.Id ;
            ResultObject resultObject = BusinessLayer.Unit_BL.Create(MyUnit.Name, parentId, MyUnit.Factor, AppConstants.ARABIC);
            if (resultObject.Code <= AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            long? newUnitId = (long?)resultObject.Data;
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
            new AfterDetailSavedEventArgs
            {
                Id = newUnitId.Value,
                DisplayMember = MyUnit.Name,
                ViewModelName = nameof(UnitDetailViewModel)
            });
            window.Close();
        }

        private void ExecuteCloseCommand(Window window)
        {
            window.Close();
        }

        private bool ExecuteCanSaveCommand(Window window)
        {
            return MyUnit != null && !MyUnit.HasErrors;
        }

    }
}
