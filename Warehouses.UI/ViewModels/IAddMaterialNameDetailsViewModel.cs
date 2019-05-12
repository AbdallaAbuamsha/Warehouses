using System.Collections.Generic;
using System.Collections.ObjectModel;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public interface IAddMaterialNameDetailsViewModel
    {
        ObservableCollection<MaterialName> GetNames();
        void Load();
    }
}