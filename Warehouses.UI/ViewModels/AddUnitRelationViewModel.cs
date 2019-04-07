using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using System;
using System.Linq;

namespace Warehouses.UI.ViewModels
{
    class AddUnitRelationViewModel : ViewModelBase, IAddUnitRelationViewModel
    {
        IUnitDataService _unitDataService;
        private Unit _selecteUnit;
        private float? _factor;
        private ObservableCollection<UnitRelationListItemViewModel> _materialUnits;

        public AddUnitRelationViewModel(IUnitDataService unitDataService)
        {
            _unitDataService = unitDataService;
            Units = new ObservableCollection<Unit>();
            UnitRelations = new ObservableCollection<UnitRelationListItemViewModel>();
            Add = new DelegateCommand(ExecuteAddRelationCommand, ExecuteCanAddCommand);
            Delete = new DelegateCommand<UnitRelationListItemViewModel>(ExecuteDeleteCommand);
        }

        private void ExecuteDeleteCommand(UnitRelationListItemViewModel unit)
        {
            UnitRelations.Remove(unit);
            Units.Insert(0, unit.MyUnit);
        }

        public void Load()
        {
            var units = _unitDataService.GetAll();
            FillLists(Units, units);
        }

        public void SaveRelations(int newUnitId)
        {
            _unitDataService.SaveUnitRelations(UnitRelations.ToList());
        }
        public ObservableCollection<Unit> Units { get; set; }

        public ObservableCollection<UnitRelationListItemViewModel> UnitRelations
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
                ((DelegateCommand)Add).RaiseCanExecuteChanged();
            }
        }

        public float? Factor
        {
            get { return _factor; }
            set
            {
                _factor = value;
                OnPropertyChanged();
                ((DelegateCommand)Add).RaiseCanExecuteChanged();
            }
        }

        public ICommand Add { get; set; }

        public ICommand Delete { get; set; }

        public ICommand SetAsDefaultUnit { get; set; }

        private void ExecuteAddRelationCommand()
        {
            var matUnit = new UnitRelationListItemViewModel { MyUnit = SelectedUnit, Factor = this.Factor.Value };
            UnitRelations.Add(matUnit);
            Units.Remove(SelectedUnit);
            Factor = null;
            ((DelegateCommand)Add).RaiseCanExecuteChanged();
        }

        private bool ExecuteCanAddCommand()
        {
            return SelectedUnit != null && Factor.HasValue;
        }

    }
}
