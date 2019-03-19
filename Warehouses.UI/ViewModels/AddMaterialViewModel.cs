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

namespace Warehouses.UI.ViewModels
{
    public class AddMaterialViewModel : ViewModelBase, IAddMaterialViewModel
    {
        private string _materialCode;
        private string _parentName;
        private string _parentCode;
        private string _barcode;
        private string _serial;
        private float _maximumSaleAmount;
        private float _minimumSaleAmount;
        private float _dazonElementsCount;
        private float _freeReferencesAmount;


        public AddMaterialViewModel()
        {
            Materials = new ObservableCollection<Material>();
            Save = new DelegateCommand(ExecuteSaveCommand);
        }

        private void ExecuteSaveCommand()
        {
            MessageBox.Show(
                MaterialCode + "\n" +
                ParentName + "\n" +
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

        }
        public ObservableCollection<Material> Materials { get; set; }

        public string MaterialCode
        {
            get { return _materialCode; }
            set { _materialCode = value; }
        }
  
        public string ParentName
        {
            get { return _parentName; }
            set { _parentName = value; }
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
