using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Wrappers;

namespace Warehouses.UI.ViewModels
{
    class AddUnitViewModel : ViewModelBase, IAddUnitViewModel
    {         

        public AddUnitViewModel(IAddUnitRelationViewModel addUnitRelationViewModel)
        {
            MyAddUnitRelationViewModel = addUnitRelationViewModel;
            MyUnit = new UnitWrapper(new Unit());
            Save = new DelegateCommand(ExecuteSaveCommand, ExecuteCanSaveCommand);
            MyUnit.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals(nameof(OrganizationWrapper.HasErrors)))
                {
                    ((DelegateCommand)Save).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)Save).RaiseCanExecuteChanged();
        }

        public void Load()
        {
            MyAddUnitRelationViewModel.Load();
        }
        private void ExecuteSaveCommand()
        {


        }

        private bool ExecuteCanSaveCommand()
        {
            return true;// SelectedLanguage != null && !string.IsNullOrEmpty(MaterialName);
        }

        public IAddUnitRelationViewModel MyAddUnitRelationViewModel { get; set; }
        public UnitWrapper MyUnit { get; set; }
        public ICommand Save { get; set; }
        public ICommand Close { get; set; }

    }
}
