using System.Collections.Generic;

namespace Warehouses.UI.ViewModels
{
    public interface IAddUnitRelationViewModel
    {
        void Load();
        void SaveRelations(int newUnitId);
    }
}