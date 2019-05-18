using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using System;
using Prism.Events;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Helper;
using System.Windows;
using System.Collections.Generic;

namespace Warehouses.UI.ViewModels
{
    class AddMaterialUnitDetailsViewModel : ViewModelBase, IAddMaterialUnitDetailsViewModel
    {
        private Unit _selecteUnit;
        private MaterialUnitListItemViewModel _defaultUnit;
        private ObservableCollection<MaterialUnitListItemViewModel> _materialUnits;
        IMessageDialogService _messageDialogService;
        IEventAggregator _eventAggregator;

        public AddMaterialUnitDetailsViewModel(IMessageDialogService messageDialogService,
            IEventAggregator eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _eventAggregator = eventAggregator;
            Units = new ObservableCollection<Unit>();
            MaterialUnits = new ObservableCollection<MaterialUnitListItemViewModel>();
            Add = new DelegateCommand(ExecuteAddCommand, ExecuteCanAddCommand);
            Delete = new DelegateCommand<MaterialUnitListItemViewModel>(ExecuteDeleteCommand);
            SetAsDefaultUnit = new DelegateCommand<MaterialUnitListItemViewModel>(ExecuteSetAsDefaultUnitCommand);
        }

        private void ExecuteSetAsDefaultUnitCommand(MaterialUnitListItemViewModel obj)
        {
            if (_defaultUnit != null)
                _defaultUnit.IsDefault = false;
            _defaultUnit = obj;
            obj.IsDefault = true;
        }

        private void ExecuteDeleteCommand(MaterialUnitListItemViewModel unit)
        {
            MaterialUnits.Remove(unit);
            Units.Insert(0, unit.Unit);
            if (unit.IsDefault && MaterialUnits.Count > 0)
                MaterialUnits[0].IsDefault = true;

        }

        public void Load(bool isRelated, List<Unit> alreadyAddedUnits = null, long defaultUnitId = -1)
        {

            //Todo: Change the get method
            //Todo: Change the groupbox data
            //Todo: Change the hint data
            //var units = _unitDataService.GetAll();
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
                return;
            }
            var units = unitResultList.List;
            FillLists(Units, units);

            //foreach (var item in alreadyAddedUnits)
            //{
            //    SelectedUnit = item;
            //    ExecuteAddCommand();
            //    if(item.Id == defaultUnitId)
            //    {
                    
            //    }

            //}
        }

        private void ExecuteAddCommand()
        {
            //MessageBox.Show(MaterialName + " " + SelectedLanguage.Name);
            bool isDefault = false;
            var matUnit = new MaterialUnitListItemViewModel(SelectedUnit, isDefault);
            if (MaterialUnits.Count == 0)
            {
                ExecuteSetAsDefaultUnitCommand(matUnit);
            }
            MaterialUnits.Add(matUnit);
            Units.Remove(SelectedUnit);

        }

        private bool ExecuteCanAddCommand()
        {
            return true;// SelectedLanguage != null && !string.IsNullOrEmpty(MaterialName);
        }

        public ObservableCollection<MaterialUnitListItemViewModel> GetUnits()
        {
            return MaterialUnits;
        }

        public ObservableCollection<Unit> Units { get; set; }

        public ObservableCollection<MaterialUnitListItemViewModel> MaterialUnits
        {
            get
            {
                return _materialUnits;
            }
            set
            {
                _materialUnits = value;
                OnPropertyChanged();
            }
        }

        public Unit SelectedUnit
        {
            get { return _selecteUnit; }
            set
            {
                _selecteUnit = value;
                OnPropertyChanged();
            }
        }

        public ICommand Add { get; set; }

        public ICommand Delete { get; set; }

        public ICommand SetAsDefaultUnit { get; set; }
    }
}