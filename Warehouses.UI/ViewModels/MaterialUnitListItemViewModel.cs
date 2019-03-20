using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public class MaterialUnitListItemViewModel : ViewModelBase
    {
        private Unit _unit;
        private bool _isDefault;
        public MaterialUnitListItemViewModel(Unit unit, bool isDefault)
        {
            this.Unit = unit;
            this.IsDefault = IsDefault;
        }
        public Unit Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }
        public bool IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                _isDefault = value;
                OnPropertyChanged();
            }
        }
    }
}
