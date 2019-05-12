using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using System;

namespace Warehouses.UI.ViewModels
{
    class AddMaterialUnitDetailsViewModel : ViewModelBase, IAddMaterialUnitDetailsViewModel
    {
        IUnitDataService _unitDataService;
        private Unit _selecteUnit;
        private MaterialUnitListItemViewModel _defaultUnit;
        private ObservableCollection<MaterialUnitListItemViewModel> _materialUnits;


        public AddMaterialUnitDetailsViewModel(IUnitDataService unitDataService)
        {
            this._unitDataService = unitDataService;
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

        public void Load(bool isRelated)
        {

            //Todo: Change the get method
            //Todo: Change the groupbox data
            //Todo: Change the hint data
            var units = _unitDataService.GetAll();
            FillLists(Units, units);
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