using System;
using Prism.Events;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Data;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Wrappers;
using Prism.Commands;
using Warehouses.UI.Helper;
using Warehouses.BusinessLayer;
using Warehouses.UI.Events;
using Warehouses.UI.Views.Popups;
using System.Windows;
using System.Linq;
using System.Text;

namespace Warehouses.UI.ViewModels
{
    public class MaterialDetailViewModel : DetailViewModelBase
    {
        private IMaterialDataService _materialService;
        private MaterialWrapper _material;
        private Material _selectedParent;
        private string _parentCode;
        private bool _serializable;

        public MaterialDetailViewModel(
            IMaterialDataService materialService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IAddMaterialUnitDetailsViewModel addRelatedMaterialUnitViewModel,
            IAddMaterialUnitDetailsViewModel addUnRelatedMaterialUnitViewModel)
            : base(eventAggregator, messageDialogService)
        {
            _materialService = materialService;
            // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
            //this.AddRelatedMaterialUnitViewModel = addRelatedMaterialUnitViewModel;
            //this.AddUnRelatedMaterialUnitViewModel = addUnRelatedMaterialUnitViewModel;
            EventAggregator.GetEvent<GetVoidReasonEvent>().Subscribe(OnGetVoidReason);
            //Names = new ObservableCollection<string>();
            //AvailableUnits = new ObservableCollection<Unit>();
            //AddedUnits = new ObservableCollection<Unit>();
            Materials = new ObservableCollection<Model.Material>();
            Organizations = new ObservableCollection<Organization>();
            Units = new ObservableCollection<Unit>();
        }
        public ObservableCollection<Material> Materials { get; set; }
        public ObservableCollection<Organization> Organizations { get; set; }

        // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
        //public IAddMaterialUnitDetailsViewModel AddRelatedMaterialUnitViewModel { get; set; }
        //public IAddMaterialUnitDetailsViewModel AddUnRelatedMaterialUnitViewModel { get; set; }

        //public ObservableCollection<String> Names { get; set; }

        //public ObservableCollection<Unit> AvailableUnits { get; set; }

        //public ObservableCollection<Unit> AddedUnits { get; set; }
        public ObservableCollection<Unit> Units { get; set; }

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
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public override void Load(long id)
        {
            LoadOrganizations();
            LoadUnits();
            //Material material = _materialService.GetById(id);
            ResultObject resultObject = BusinessLayer.Material_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                MessageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Material> materialResultList = (ResultList<Material>)resultObject.Data;
            if (materialResultList.TotalCount == 0)
            {
                MessageDialogService.ShowInfoDialog(Application.Current.FindResource("no_materials_available").ToString());
                //return;
            }

            var materials = materialResultList.List;
            Material material;
            if (id > 0)
            {
                resultObject = Material_BL.GetById(id, AppConstants.ARABIC);
                if (resultObject.Code == AppConstants.ERROR_CODE)
                {
                    MessageDialogService.ShowInfoDialog(resultObject.Message);
                    return;
                }
                material = (Material)resultObject.Data;
            }
            else
                material = new Material();

            InitializeMaterial(material);

            foreach (var item in materials.ToList())
            {
                if (material.Id == item.Id)
                    materials.Remove(item);
                if (material.ParentId == item.Id)
                    SelectedParent = item;
            }
            FillLists(Materials, materials);

            Material.SelectedOrganization = Organizations.FirstOrDefault(f => f.Id == material.OrganizationId);

            resultObject = MaterialUnit_BL.GetBasicUnitByMaterialId(id, AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                MessageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            Unit basicUnit = (Unit)resultObject.Data;
            //Material.SelectedUnit = Units.FirstOrDefault(f => f.ParentUnitId == basicUnit.Id);
            foreach (var unit in Units)
            {
                if (unit.Id == basicUnit.Id)
                    Material.SelectedUnit = unit;
            }
            // commented because unrelated materials has been bended and not GetRelatedUnitsByUnitId exist in data access layer
            //AddRelatedMaterialUnitViewModel.Load(true);
            //AddUnRelatedMaterialUnitViewModel.Load(false);
        }
        private void LoadUnits()
        {
            ResultObject resultObject = BusinessLayer.Unit_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                MessageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Unit> unitResultList = (ResultList<Unit>)resultObject.Data;
            if (unitResultList.TotalCount == 0)
            {
                MessageDialogService.ShowInfoDialog(Application.Current.FindResource("no_units_available").ToString());
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
                MessageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Organization> organizationResultList = (ResultList<Organization>)resultObject.Data;
            if (organizationResultList.TotalCount == 0)
            {
                MessageDialogService.ShowInfoDialog(Application.Current.FindResource("no_organizations_available").ToString());
                return;
            }
            var organizations = organizationResultList.List;
            //var organizations = _organizationDataService.GetAll();
            FillLists(Organizations, organizations);
        }

        private void LoadMaterials()
        {
            ResultObject resultObject = BusinessLayer.Material_BL.GetAll(AppConstants.ARABIC);
            if (resultObject.Code == AppConstants.ERROR_CODE)
            {
                MessageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            ResultList<Material> materialResultList = (ResultList<Material>)resultObject.Data;
            if (materialResultList.TotalCount == 0)
            {
                MessageDialogService.ShowInfoDialog(Application.Current.FindResource("no_materials_available").ToString());
                return;
            }

            var materials = materialResultList.List;
            FillLists(Materials, materials);
        }

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

                if (ParentCode.Equals(SelectedParent.Code)) return;

                foreach (var material in Materials)
                {
                    if (material.Code.Equals(ParentCode))
                    {
                        SelectedParent = material;
                    }
                }
            }
        }
        
        public bool Serializable
        {
            get { return _serializable; }
            set { _serializable = value;
                OnPropertyChanged();
            }
        }

        private void InitializeMaterial(Material material)
        {
            Material = new MaterialWrapper(material);
            Serializable = material.Serializable;
            Material.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Material.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Material.Id == 0)
            {
                // Little trick to trigger the validation
                Material.Name = "";
            }
        }

        protected override void OnDeleteExecute()
        {
            var result = MessageDialogService.ShowOkCancelDialog($"Do you really want to delete the Material {Material.Name}?", "Question");

            if (result == MessageDialogResult.Cancel)
                return;
            new GetReasonWindow(EventAggregator).ShowDialog();
        }

        protected override bool OnSaveCanExecute()
        {
            return Material != null && !Material.HasErrors;
        }

        protected override void OnSaveExecute()
        {
            long? parentId = null;
            if (SelectedParent != null) parentId = SelectedParent.Id;
            ResultObject resultObject = Material_BL.Edit(
                Material.Id,
                Material.Name,
                Material.LatinName,
                Material.Code,
                Material.Barcode,
                Serializable,
                Material.MinimumSaleAmount,
                Material.MaximumSaleAmount,
                Material.FreeReferencesAmount,
                parentId,
                AppConstants.ARABIC);
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
            new AfterDetailSavedEventArgs
            {
                Id = Material.Id,
                DisplayMember = Material.Name,
                ViewModelName = nameof(MaterialDetailViewModel)
            });
        }

        private Material CreateNewOrganization()
        {
            var material = new Material();
            return material;
        }

        private void OnGetVoidReason(string voidReason)
        {
            ResultObject resultObject = Material_BL.Delete(Material.Id, voidReason, AppConstants.ARABIC);
            if (resultObject.Code < AppConstants.ERROR_CODE)
            {
                MessageDialogService.ShowInfoDialog(resultObject.Message);
                return;
            }
            bool res = (bool)resultObject.Data;
            if (res == true)
            {
                MessageDialogService.ShowInfoDialog("Deleted Seccessfully");
                RaiseDetailDeletedEvent(Material.Id);
            }
            else
            {
                MessageDialogService.ShowInfoDialog("Delete Failed");
            }
        }
    }
}
