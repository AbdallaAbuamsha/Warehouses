using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;

namespace Warehouses.UI.ViewModels
{
    class AddUnitRelationViewModel : ViewModelBase, IAddUnitRelationViewModel
    {
        IUnitDataService _unitDataService;
        private Unit _selecteUnit;
        private ObservableCollection<UnitRelationListItemVIewModel> _materialUnits;


        public AddUnitRelationViewModel(IUnitDataService unitDataService)
        {
            this._unitDataService = unitDataService;
            Units = new ObservableCollection<Unit>();
            UnitRelations = new ObservableCollection<UnitRelationListItemVIewModel>();
            Add = new DelegateCommand(ExecuteAddCommand, ExecuteCanAddCommand);
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

        private void ExecuteAddCommand()
        {
            var matUnit = new UnitRelationListItemVIewModel { MyUnit = SelectedUnit, Factor =  this.Factor};
            UnitRelations.Add(matUnit);
            Units.Remove(SelectedUnit);

        }

        private bool ExecuteCanAddCommand()
        {
            return true;// SelectedLanguage != null && !string.IsNullOrEmpty(MaterialName);
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
            }
        }
        private float _factor;

        public float Factor
        {
            get { return _factor; }
            set { _factor = value; }
        }

        public ICommand Add { get; set; }

        public ICommand Delete { get; set; }

        public ICommand SetAsDefaultUnit { get; set; }
    }
}
