using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;

namespace Warehouses.UI.ViewModels
{
    class AddUnitRelationViewModel : ViewModelBase, IAddUnitRelationViewModel
    {
        IUnitDataService _unitDataService;
        private Unit _selecteUnit;
        private float? _factor;
        private ObservableCollection<UnitRelationListItemVIewModel> _materialUnits;

        public AddUnitRelationViewModel(IUnitDataService unitDataService)
        {
            this._unitDataService = unitDataService;
            Units = new ObservableCollection<Unit>();
            UnitRelations = new ObservableCollection<UnitRelationListItemVIewModel>();
            Add = new DelegateCommand(ExecuteAddRelationCommand, ExecuteCanAddCommand);
            Delete = new DelegateCommand<UnitRelationListItemVIewModel>(ExecuteDeleteCommand);
        }

        private void ExecuteDeleteCommand(UnitRelationListItemVIewModel unit)
        {
            UnitRelations.Remove(unit);
            Units.Insert(0, unit.MyUnit);
        }

        public void Load()
        {
            var units = _unitDataService.GetAll();
            FillLists(Units, units);
        }

        public ObservableCollection<Unit> Units { get; set; }

        public ObservableCollection<UnitRelationListItemVIewModel> UnitRelations
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
            var matUnit = new UnitRelationListItemVIewModel { MyUnit = SelectedUnit, Factor = this.Factor.Value };
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
