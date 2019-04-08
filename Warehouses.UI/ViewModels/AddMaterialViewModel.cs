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
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class AddMaterialViewModel : ViewModelBase, IAddMaterialViewModel
    {
        private string _materialCode;
        private Material _selectedParent;
        private string _barcode;
        private string _serial;
        private float? _maximumSaleAmount;
        private float? _minimumSaleAmount;
        private float? _dazonElementsCount;
        private float? _freeReferencesAmount;
        private IMaterialDataService _materialDataService;
        private string _parentCode;

        public AddMaterialViewModel(IMaterialDataService materialDataService, 
            IAddMaterialNameDetailsViewModel addMaterialNameViewModel,
            IAddMaterialUnitDetailsViewModel addMaterialUnitViewModel)
        {
            Materials = new ObservableCollection<Material>();
            Save = new DelegateCommand(ExecuteSaveCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);
            _materialDataService = materialDataService;

            AddMaterialNameViewModel = addMaterialNameViewModel;
            AddMaterialUnitViewModel = addMaterialUnitViewModel;
            Material = new MaterialWrapper(new Material());
            Material.Name = "";
            Material.Code = "";
            Material.Barcode = "";
            Material.Serial = "";
            Material.MaximumSaleAmount = null;
            Material.MinimumSaleAmount = null;
            Material.DazonElementsCount = null;
            Material.FreeReferencesAmount = null;
        }
        
        private void ExecuteSaveCommand()
        {
            MessageBox.Show(
                //MaterialCode + "\n" +
                SelectedParent.Name + "\n" +
                SelectedParent.Code+ "\n" 
                //Barcode + "\n" +
                //Serial + "\n" +
                //MaximumSaleAmount.ToString() + "\n" +
                //MinimumSaleAmount.ToString() + "\n" +
                //DazonElementsCount.ToString() + "\n" +
                //FreeReferencesAmount.ToString() + "\n" 
                );
        }
        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }
        public void Load()
        {
            var materials = _materialDataService.GetAll();
            AddMaterialNameViewModel.Load();
            AddMaterialUnitViewModel.Load();
            FillLists<Material>(Materials, materials);
        }
        public IAddMaterialNameDetailsViewModel AddMaterialNameViewModel { get; set; }

        public IAddMaterialUnitDetailsViewModel AddMaterialUnitViewModel { get; set; }

        public ObservableCollection<Material> Materials { get; set; }

        //public string MaterialCode
        //{
        //    get { return _materialCode; }
        //    set { _materialCode = value; }
        //}
  
        public Material SelectedParent
        {
            get { return _selectedParent; }
            set
            {
                _selectedParent = value;
                OnPropertyChanged();
                ParentCode = SelectedParent.Code;
            }
        }

        public string ParentCode
        {
            get { return _parentCode; }
            set
            {
                _parentCode = value;
                OnPropertyChanged();

                if (    ParentCode.Equals(SelectedParent.Code)) return;

                foreach (var material in Materials)
                {
                    if(material.Code.Equals(ParentCode))
                    {
                        SelectedParent = material;
                    }
                }
            }
        }
        public MaterialWrapper Material { get; set; }
        //public string Barcode
        //{
        //    get { return _barcode; }
        //    set { _barcode = value; }
        //}

        //public string Serial
        //{
        //    get { return _serial; }
        //    set { _serial = value; }
        //}

        //public float? MaximumSaleAmount
        //{
        //    get { return _maximumSaleAmount; }
        //    set { _maximumSaleAmount = value; }
        //}

        //public float? MinimumSaleAmount
        //{
        //    get { return _minimumSaleAmount; }
        //    set { _minimumSaleAmount = value; }
        //}

        //public float? DazonElementsCount
        //{
        //    get { return _dazonElementsCount; }
        //    set { _dazonElementsCount = value; }
        //}

        //public float? FreeReferencesAmount
        //{
        //    get { return _freeReferencesAmount; }
        //    set { _freeReferencesAmount = value; }
        //}

        public ICommand Save { get; set; }

        public ICommand Close { get; set; }
    }
}
