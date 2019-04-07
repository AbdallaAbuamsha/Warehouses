using System.Collections.Generic;
using Warehouses.Model;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Data
{
    public interface IUnitDataService
    {
        IEnumerable<Unit> GetAll();
        int Save(Unit model);
        void SaveUnitRelations(List<UnitRelationListItemViewModel> relations);
    }
}