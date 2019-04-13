using Prism.Events;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class ReceiptTableItemViewModel : ViewModelBase
    {


        private IMaterialDataService _materialService;
        private IWarehouseDataService _warehouseService;

        private int _id;
        private Material _selectedMaterial;
        private Unit _selectedMainUnit;
        private UnitValueViewModel _selecteUnRelatedUnit;
        private float? _quantity;
        private string _serial;
        private string _barcode;
        private string _expireDate;
        private string _note;
        private string _code;
        private Warehouse _selectedWarehouse;
        private IEventAggregator _eventAggregator;

        public ReceiptTableItemViewModel(
            int id,
            IMaterialDataService materialService,
            IWarehouseDataService warehouseService,
            IUnitDataService unitService,
            IEventAggregator eventAggregator)
        {
            _materialService = materialService;
            _warehouseService = warehouseService;
            _eventAggregator = eventAggregator;
            Materials = new ObservableCollection<Material>();
            FillLists(Materials, _materialService.GetAll());
            MainUnits = new ObservableCollection<Unit>();
            UnRelatedUnits = new ObservableCollection<UnitValueViewModel>();
            Warehouses = new ObservableCollection<Warehouse>();
            FillLists(Warehouses, _warehouseService.GetAll());

            Id = id;
            _eventAggregator.GetEvent<DeleteReceiptRowEvent>().Subscribe(DeleteReceiptRow);
        }

        private void DeleteReceiptRow(int id)
        {
            if (Id > id)
                Id--;
        }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Material> Materials { get; set; }

        public Material SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged();
                FillLists(MainUnits, _materialService.GetAllUnits(SelectedMaterial.Id));
            }
        }

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Unit> MainUnits { get; set; }

        public Unit SelectedMainUnit
        {
            get { return _selectedMainUnit; }
            set
            {
                _selectedMainUnit = value;
                OnPropertyChanged();
                LoadUnRelatedUnits();
            }
        }

        public float? Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public ObservableCollection<UnitValueViewModel> UnRelatedUnits { get; set; }

        public UnitValueViewModel SelecteUnRelatedUnit
        {
            get { return _selecteUnRelatedUnit; }
            set
            {
                _selecteUnRelatedUnit = value;
                OnPropertyChanged();
            }
        }

        public string Serial
        {
            get { return _serial; }
            set
            {
                _serial = value;
                OnPropertyChanged();
            }
        }

        public string Barcode
        {
            get { return _barcode; }
            set
            {
                _barcode = value;
                OnPropertyChanged();
            }
        }

        public string ExpireDate
        {
            get { return _expireDate; }
            set
            {
                _expireDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Warehouse> Warehouses { get; set; }

        public Warehouse SelectedWarehouse
        {
            get { return _selectedWarehouse; }
            set { _selectedWarehouse = value; }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        private void LoadUnRelatedUnits()
        {
            //FillLists(RelatedUnits, _unitService.GetAll());
            var unRelatedUnits = _materialService.GetAllUnRelatedUnits(SelectedMaterial.Id, SelectedMainUnit.Id);
            UnRelatedUnits.Clear();
            foreach (Unit unit in unRelatedUnits)
            {
                UnRelatedUnits.Add(new UnitValueViewModel(unit.Id, unit.Name));
            }
        }
    }
}
