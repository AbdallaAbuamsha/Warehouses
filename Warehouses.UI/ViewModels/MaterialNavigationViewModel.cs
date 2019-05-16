using System;
using System.Collections.ObjectModel;
using Prism.Events;
using Warehouses.UI.Events;
using Warehouses.UI.Data;
using Warehouses.Model;
using Warehouses.UI.Helper;
using Warehouses.UI.Views.Services;
using System.Windows;

namespace Warehouses.UI.ViewModels
{
    public class MaterialNavigationViewModel : NavigationViewModel
    {
        private IMessageDialogService _messageDialogService;
        private ObservableCollection<NavigationItemViewModel> _materials;

        public MaterialNavigationViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            : base(eventAggregator)
        {
            _messageDialogService = messageDialogService;
            Materials = new ObservableCollection<NavigationItemViewModel>();
        }

        public override void Load()
        {
            //var materials = _materialService.GetAll();
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
            Materials.Clear();
            foreach (var mat in materials)
            {
                Materials.Add(new NavigationItemViewModel(mat.Id, mat.Name, nameof(MaterialDetailViewModel), eventAggregator));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Materials { get
            {
                return _materials;
            }
            set
            {
                _materials = value;
                OnPropertyChanged();
            }
        }
        protected override void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(
            new OpenDetailViewEventArgs
            {
                ViewModelName = nameof(MaterialDetailViewModel)
            });
        }

        protected override void AfterDetailDeleted(ObservableCollection<TreeViewItemViewModel> items, AfterDetailDeletedEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void AfterDetailSaved(ObservableCollection<TreeViewItemViewModel> items, AfterDetailSavedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
