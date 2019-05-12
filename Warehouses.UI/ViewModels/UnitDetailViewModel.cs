using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    public class UnitDetailViewModel : DetailViewModelBase
    {
        private IUnitDataService _unitService;
        private UnitWrapper _unit;

        public UnitDetailViewModel(
            IUnitDataService unitService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
            _unitService = unitService;
            Names = new ObservableCollection<string>();
        }

        public ObservableCollection<String> Names { get; set; }



        public UnitWrapper Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit= value;
                OnPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public override void Load(long id)
        {
            //Unit unit = _unitService.GetById(id);
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
