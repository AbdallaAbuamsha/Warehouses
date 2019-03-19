using Prism.Commands;
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

namespace Warehouses.UI.ViewModels
{
    public class AddMaterialViewModel : ViewModelBase, IAddMaterialViewModel
    {
        private string _materialCode;
        private string _selectedParent;
        private string _parentCode;
        private string _barcode;
        private string _serial;
        private float _maximumSaleAmount;
        private float _minimumSaleAmount;
        private float _dazonElementsCount;
        private float _freeReferencesAmount;
        private IMaterialDataService _materialDataService;

        public AddMaterialViewModel(IMaterialDataService materialDataService)
        {
            Materials = new ObservableCollection<Material>();
            Save = new DelegateCommand(ExecuteSaveCommand);
            _materialDataService = materialDataService;            
        }
        
        private void ExecuteSaveCommand()
        {
            MessageBox.Show(
                MaterialCode + "\n" +
                SelectedParent + "\n" +
                ParentCode + "\n" +
                Barcode + "\n" +
                Serial + "\n" +
                MaximumSaleAmount.ToString() + "\n" +
                MinimumSaleAmount.ToString() + "\n" +
                DazonElementsCount.ToString() + "\n" +
                FreeReferencesAmount.ToString() + "\n" 
                );
        }
        public void Load()
        {
            var materials = _materialDataService.GetAll();
            FillLists<Material>(Materials, materials);
        }
        private void FillLists<T>(ObservableCollection<T> empty, IEnumerable<T> filled)
        {
            foreach (var item in filled)
            {
                empty.Add(item);
            }
        }
        public ObservableCollection<Material> Materials { get; set; }

        public string MaterialCode
        {
            get { return _materialCode; }
            set { _materialCode = value; }
        }
  
        public string SelectedParent
        {
            get { return _selectedParent; }
            set { _selectedParent = value; }
        }

        public string ParentCode
        {
            get { return _parentCode; }
            set { _parentCode = value; }
        }

        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }

        public string Serial
        {
            get { return _serial; }
            set { _serial = value; }
        }

        public float MaximumSaleAmount
        {
            get { return _maximumSaleAmount; }
            set { _maximumSaleAmount = value; }
        }

        public float MinimumSaleAmount
        {
            get { return _minimumSaleAmount; }
            set { _minimumSaleAmount = value; }
        }

        public float DazonElementsCount
        {
            get { return _dazonElementsCount; }
            set { _dazonElementsCount = value; }
        }

        public float FreeReferencesAmount
        {
            get { return _freeReferencesAmount; }
            set { _freeReferencesAmount = value; }
        }

        public ICommand Save { get; set; }
    }
}
