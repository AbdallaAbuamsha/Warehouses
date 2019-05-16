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

        private Material _selectedParent;
        private IMaterialDataService _materialDataService;
        private string _parentCode;

        public AddMaterialViewModel(
            IEventAggregator eventAggregator,
            IMaterialDataService materialDataService, 
            IAddMaterialNameDetailsViewModel addMaterialNameViewModel,
            IAddMaterialUnitDetailsViewModel addRelatedMaterialUnitViewModel,
            IAddMaterialUnitDetailsViewModel addUnRelatedMaterialUnitViewModel,
            IMessageDialogService messageDialogService)
        {
            _materialDataService = materialDataService;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            // commented because the data access layer doesn't support mutli names yet .... please dont delete this
            //AddMaterialNameViewModel = addMaterialNameViewModel; 
            this.AddRelatedMaterialUnitViewModel = addRelatedMaterialUnitViewModel;
            this.AddUnRelatedMaterialUnitViewModel = addUnRelatedMaterialUnitViewModel;

            Materials = new ObservableCollection<Material>();
            Save = new DelegateCommand<Window>(ExecuteSaveCommand, ExecuteCanSaveCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseOrganizationCommand);
            Organizations = new ObservableCollection<Organization>();
            Material = new MaterialWrapper(new Material());
            Material.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(Material.HasErrors)))
                {
                    ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand<Window>)Save).RaiseCanExecuteChanged();
            Material.Code = "";
            Material.Barcode = "";
            //Material.Serial = "";
            Material.MaximumSaleAmount = null;
            Material.MinimumSaleAmount = null;
            //Material.DazonElementsCount = null;
            Material.FreeReferencesAmount = null;
            Material.SelectedOrganization = null;
        }
        public void Load()
        {
            var materials = _materialDataService.GetAll();
            //AddMaterialNameViewModel.Load();
            AddRelatedMaterialUnitViewModel.Load(true);
            AddUnRelatedMaterialUnitViewModel.Load(false);
            FillLists<Material>(Materials, materials);
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
            //var organizations = _organizationDataService.GetAll();
            FillLists(Organizations, organizations);
        }

        private bool ExecuteCanSaveCommand(Window window)
        {
           return Material != null && !Material.HasErrors && /*AddMaterialNameViewModel.GetNames().Count > 0 &&*/ Material.SelectedOrganization != null;
        }

        private void ExecuteSaveCommand(Window window)
        {
            //ObservableCollection<MaterialName> listOfNames = AddMaterialNameViewModel.GetNames();
            ObservableCollection<MaterialUnitListItemViewModel> listOfRelatedUnits = AddRelatedMaterialUnitViewModel.GetUnits();
            ObservableCollection<MaterialUnitListItemViewModel> listOfUnRelatedUnits = AddUnRelatedMaterialUnitViewModel.GetUnits();
            StringBuilder materialNames = new StringBuilder();
            StringBuilder relatedUnitName = new StringBuilder();
            StringBuilder unRelatedUnitNames = new StringBuilder();
            string name, latinName;
            long unitId = 0;
            long? parentId = null;
            // commented because the data access layer doesn't support mutli names yet .... please dont delete this
            //if (listOfNames.Count == 0)
            //{
            //    _messageDialogService.ShowInfoDialog("Please add one name at least");
            //    return;
            //}
            if (listOfRelatedUnits.Count == 0)
            {
                _messageDialogService.ShowInfoDialog("Please add one unit at least");
                return;
            }
            // commented because the data access layer doesn't support mutli names yet .... please dont delete this
            //name = listOfNames[0].Name;
            //latinName = (listOfNames.Count > 1) ? listOfNames[1].Name : "";
            //foreach (var item in listOfNames)
            //{
            //    materialNames.Append(item.Name + "\n");
            //}
            unitId = listOfRelatedUnits[0].Unit.Id;
            foreach (var item in listOfRelatedUnits)
            {
                relatedUnitName.Append(item.Unit.Name + "\n");
            }
            foreach (var item in listOfUnRelatedUnits)
            {
                unRelatedUnitNames.Append(item.Unit.Name + "\n");
            }
            if (SelectedParent != null) parentId = SelectedParent.Id;
            ResultObject resultObject = Material_BL.Create(Material.Name, Material.LatinName, Material.Code, Material.Barcode, Serializable, unitId, Material.MinimumSaleAmount, Material.MaximumSaleAmount, Material.FreeReferencesAmount, Material.SelectedOrganization.Id, parentId, AppConstants.ARABIC);
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Publish(
            new AfterDetailSavedEventArgs
            {
                Id = Material.Id,
                DisplayMember = Material.Name,
                ViewModelName = nameof(MaterialDetailViewModel)
            });
            //MessageBox.Show(
            //    ""+materialNames.ToString()+
            //    "code "+Material.Code + "\n" +
            //    "barcode "+Material.Barcode + "\n" +
            //    "max sale"+Material.MaximumSaleAmount + "\n" +
            //    "min sale"+Material.MinimumSaleAmount + "\n" +
            //    //"dazon "+Material.DazonElementsCount + "\n" +
            //    "free "+Material.FreeReferencesAmount + "\n" +
            //    "serializable "+Serializable.ToString() + "\n" +
            //    "relateds "+relatedUnitName.ToString() +
            //    "unrelateds"+unRelatedUnitNames
            //    );
        }
        private void ExecuteCloseOrganizationCommand(Window window)
        {
            window.Close();
        }
        // commented because the data access layer doesn't support mutli names yet .... please dont delete this
        //public IAddMaterialNameDetailsViewModel AddMaterialNameViewModel { get; set; }

        public IAddMaterialUnitDetailsViewModel AddRelatedMaterialUnitViewModel { get; set; }
        public IAddMaterialUnitDetailsViewModel AddUnRelatedMaterialUnitViewModel { get; set; }

        public ObservableCollection<Material> Materials { get; set; }

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
        private MaterialWrapper _material;
        private bool _serializable;

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
        public bool Serializable
        {
            get
            {
                return _serializable;
            }
            set
            {
                _serializable = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Organization> Organizations { get; set; }

        public ICommand Save { get; set; }

        public ICommand Close { get; set; }
    }
}
