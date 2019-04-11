using System;
using System.Collections.ObjectModel;
using Prism.Events;
using Warehouses.UI.Events;
using Warehouses.UI.Data;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public class MaterialNavigationViewModel : NavigationViewModel
    {
        private IMaterialDataService _materialService;
        private ObservableCollection<NavigationItemViewModel> _materials;

        public MaterialNavigationViewModel(
            IEventAggregator eventAggregator,
            IMaterialDataService materialService)
            : base(eventAggregator)
        {
            _materialService = materialService;
            Materials = new ObservableCollection<NavigationItemViewModel>();
        }

        public override void Load()
        {
            var materials = _materialService.GetAll();
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
