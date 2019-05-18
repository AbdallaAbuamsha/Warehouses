using System.Collections.Generic;
using System.Collections.ObjectModel;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public interface IAddMaterialUnitDetailsViewModel
    {
        ObservableCollection<MaterialUnitListItemViewModel> GetUnits();
        void Load(bool isRelated, List<Unit> alreadyAddedUnits = null, long defaultUnitId = -1);
    }
}