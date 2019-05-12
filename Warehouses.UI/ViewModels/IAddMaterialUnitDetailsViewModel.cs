using System.Collections.ObjectModel;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public interface IAddMaterialUnitDetailsViewModel
    {
        ObservableCollection<MaterialUnitListItemViewModel> GetUnits();
        void Load(bool isRelated);
    }
}