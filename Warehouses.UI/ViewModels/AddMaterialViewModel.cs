using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public class AddMaterialViewModel : ViewModelBase
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


    }
}
