using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Data;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.Wrappers;
using Prism.Commands;

namespace Warehouses.UI.ViewModels
{
    public class MaterialDetailViewModel : DetailViewModelBase
    {
        private IMaterialDataService _materialService;
        private MaterialWrapper _material;

        public MaterialDetailViewModel(
            IMaterialDataService materialService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
            _materialService = materialService;
            Names = new ObservableCollection<string>();
            AvailableUnits = new ObservableCollection<Unit>();
            AddedUnits = new ObservableCollection<Unit>();
        }

        public ObservableCollection<String> Names { get; set; }

        public ObservableCollection<Unit> AvailableUnits { get; set; }

        public ObservableCollection<Unit> AddedUnits { get; set; }

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

        public override void Load(int id)
        {
            Material material = _materialService.GetById(id);
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            throw new NotImplementedException();
        }

        protected override void OnSaveExecute()
        {
            throw new NotImplementedException();
        }
    }
}
