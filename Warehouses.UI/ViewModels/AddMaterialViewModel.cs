using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouses.BusinessLayer;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class AddMaterialViewModel : ViewModelBase, IAddMaterialViewModel
    {
        IEventAggregator _eventAggregator;
        IMessageDialogService _messageDialogService;
        private bool _serializable;
        private MaterialWrapper _material;
        private Material _selectedParent;
        private string _parentCode;
        private Unit _selectedUnit;

        public AddMaterialViewModel(
            IEventAggregator eventAggregator,
            IAddMaterialNameDetailsViewModel addMaterialNameViewModel,
            // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
            //IAddMaterialUnitDetailsViewModel addRelatedMaterialUnitViewModel,
            //IAddMaterialUnitDetailsViewModel addUnRelatedMaterialUnitViewModel,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            // commented because the data access layer doesn't support mutli names yet .... please dont delete this
            //AddMaterialNameViewModel = addMaterialNameViewModel; 

            // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
            //this.AddRelatedMaterialUnitViewModel = addRelatedMaterialUnitViewModel;
            //this.AddUnRelatedMaterialUnitViewModel = addUnRelatedMaterialUnitViewModel;

            Materials = new ObservableCollection<Material>();
            Save = new DelegateCommand<Window>(ExecuteSaveCommand, ExecuteCanSaveCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);
            Organizations = new ObservableCollection<Organization>();
            Units = new ObservableCollection<Unit>();
            Material = new MaterialWrapper(new Material());
            Material.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(Material.HasErrors)))
                {
                    ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
            Material.Name = "";
            Material.LatinName = "";
            Material.Code = "";
            Material.Barcode = "";
            Material.MaximumSaleAmount = null;
            Material.MinimumSaleAmount = null;
            //Material.DazonElementsCount = null;
            Material.FreeReferencesAmount = null;
            Material.SelectedOrganization = null;
            Material.SelectedUnit = null;
        }

        public void Load()
        {
            //AddMaterialNameViewModel.Load();
            //Get all materials to select material parent
            LoadMaterials();
            LoadOrganizations();
            LoadUnits();
            
            // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
            //AddRelatedMaterialUnitViewModel.Load(true);
            //AddUnRelatedMaterialUnitViewModel.Load(false);

        }

        private void LoadUnits()
        {
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
        }

        private void LoadOrganizations()
        {
            ResultObject resultObject = BusinessLayer.Organization_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Organization> organizationResultList = (ResultList<Organization>)resultObject.Data;
            if (organizationResultList.TotalCount == 0)
            {
                _messageDialogService.ShowInfoDialog(Application.Current.FindResource("no_organizations_available").ToString());
                return;
            }
            var organizations = organizationResultList.List;
            FillLists(Organizations, organizations);
        }

        private void LoadMaterials()
        {
            ResultObject resultObject = BusinessLayer.Material_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                _messageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Material> materialResultList = (ResultList<Material>)resultObject.Data;
            if (materialResultList.TotalCount == 0)
            {
                _messageDialogService.ShowInfoDialog(Application.Current.FindResource("no_materials_available").ToString());
                return;
            }

            var materials = materialResultList.List;
            FillLists(Materials, materials);
        }

        private bool ExecuteCanSaveCommand(Window window)
        {
           return Material != null && !Material.HasErrors && /*AddMaterialNameViewModel.GetNames().Count > 0 &&*/ Material.SelectedOrganization != null;
        }

        private void ExecuteSaveCommand(Window window)
        {
            //ObservableCollection<MaterialName> listOfNames = AddMaterialNameViewModel.GetNames();
            //ObservableCollection<MaterialUnitListItemViewModel> listOfRelatedUnits = AddRelatedMaterialUnitViewModel.GetUnits();
            //ObservableCollection<MaterialUnitListItemViewModel> listOfUnRelatedUnits = AddUnRelatedMaterialUnitViewModel.GetUnits();
            StringBuilder materialNames = new StringBuilder();
            StringBuilder relatedUnitName = new StringBuilder();
            StringBuilder unRelatedUnitNames = new StringBuilder();
            long unitId = 0;
            long? parentId = null;
            // commented because the data access layer doesn't support mutli names yet .... please dont delete this
            //if (listOfNames.Count == 0)
            //{
            //    _messageDialogService.ShowInfoDialog("Please add one name at least");
            //    return;
            //}
            //if (listOfRelatedUnits.Count == 0)
            //{
            //    _messageDialogService.ShowInfoDialog("Please add one unit at least");
            //    return;
            //}
            // commented because the data access layer doesn't support mutli names yet .... please dont delete this
            //name = listOfNames[0].Name;
            //latinName = (listOfNames.Count > 1) ? listOfNames[1].Name : "";
            //foreach (var item in listOfNames)
            //{
            //    materialNames.Append(item.Name + "\n");
            //}

            // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
            //unitId = listOfRelatedUnits[0].Unit.Id;
            //foreach (var item in listOfRelatedUnits)
            //{
            //    relatedUnitName.Append(item.Unit.Name + "\n");
            //}
            //foreach (var item in listOfUnRelatedUnits)
            //{
            //    unRelatedUnitNames.Append(item.Unit.Name + "\n");
            //}
            unitId = SelectedUnit.Id;
            if (SelectedParent != null) parentId = SelectedParent.Id;
            ResultObject resultObject = Material_BL.Create(Material.Name, Material.LatinName, Material.Code, Material.Barcode, Material.Serializable, unitId, Material.MinimumSaleAmount, Material.MaximumSaleAmount, Material.FreeReferencesAmount, Material.SelectedOrganization.Id, parentId, AppConstants.ARABIC);
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
            new AfterDetailSavedEventArgs
            {
                Id = Material.Id,
                DisplayMember = Material.Name,
                ViewModelName = nameof(MaterialDetailViewModel)
            });
        }

        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }
        // commented because the data access layer doesn't support mutli names yet .... please dont delete this
        //public IAddMaterialNameDetailsViewModel AddMaterialNameViewModel { get; set; }

        // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
        //public IAddMaterialUnitDetailsViewModel AddRelatedMaterialUnitViewModel { get; set; }

        // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
        //public IAddMaterialUnitDetailsViewModel AddUnRelatedMaterialUnitViewModel { get; set; }

        public ObservableCollection<Unit> Units { get; set; }

        public ObservableCollection<Material> Materials { get; set; }

        public ObservableCollection<Organization> Organizations { get; set; }

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
        public Unit SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                OnPropertyChanged();
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

        public MaterialWrapper Material
        {
            get
            {
                return _material;
            }
            set
            {
                _material = value;
                OnPropertyChanged();
            }
        }

        public ICommand Save { get; set; }

        public ICommand Close { get; set; }
    }
}
