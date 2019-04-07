using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Data
{
    public class UnitDataService : IUnitDataService
    {
        public IEnumerable<Unit> GetAll()
        {
            yield return new Unit { Name = "Unit 1 " };
            yield return new Unit { Name = "Unit 2 " };
            yield return new Unit { Name = "Unit 3 " };
            yield return new Unit { Name = "Unit 4 " };
            yield return new Unit { Name = "Unit 5 " };
        }

        public int Save(Unit unit)
        {
            int newUnitId = 1;
            return newUnitId;
        }

        public void SaveUnitRelations(List<UnitRelationListItemViewModel> relations)
        {
            
        }
    }
}
