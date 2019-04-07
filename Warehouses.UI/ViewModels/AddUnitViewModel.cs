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
    class AddUnitViewModel : ViewModelBase, IAddUnitViewModel
    {
        private UnitWrapper _myUnit;
        private IUnitDataService _unitService;

        public AddUnitViewModel(
            IUnitDataService unitService,
            IAddUnitRelationViewModel addUnitRelationViewModel)
        {
            _unitService = unitService;
            MyAddUnitRelationViewModel = addUnitRelationViewModel;
            MyUnit = new UnitWrapper(new Unit());
            Save = new DelegateCommand(ExecuteSaveCommand, ExecuteCanSaveCommand);
            Close = new DelegateCommand<Window>(ExecuteCloseCommand);
            MyUnit.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(OrganizationWrapper.HasErrors)))
                {
                    ((DelegateCommand)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)Save).RaiseCanExecuteChanged();
            MyUnit.Name = "";
            MyUnit.Symbol = "";
        }

        public void Load()
        {
            MyAddUnitRelationViewModel.Load();

        }

        public IAddUnitRelationViewModel MyAddUnitRelationViewModel { get; set; }

        public UnitWrapper MyUnit
        {
            get
            { return _myUnit; }
            set
            {
                _myUnit = value;
                OnPropertyChanged();
                if(Save != null)
                    ((DelegateCommand)Save).RaiseCanExecuteChanged();
            }
        }

        public ICommand Save { get; set; }

        public ICommand Close { get; set; }

        private void ExecuteSaveCommand()
        {
            int newUnitId = _unitService.Save(MyUnit.Model);
            MyAddUnitRelationViewModel.SaveRelations(newUnitId);
            Application.Current.MainWindow.Close();
        }

        private void ExecuteCloseCommand(Window window)
        {
            window.Close();
        }

        private bool ExecuteCanSaveCommand()
        {
            return MyUnit != null && !MyUnit.HasErrors;
        }

    }
}
