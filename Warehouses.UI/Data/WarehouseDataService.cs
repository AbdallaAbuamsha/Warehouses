using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public class WarehouseDataService : IWarehouseDataService
    {
        public IEnumerable<Warehouse> GetAll()
        {
            yield return new Warehouse { Name = "Warehouse 1 " };
            yield return new Warehouse { Name = "Warehouse 2 " };
            yield return new Warehouse { Name = "Warehouse 3 " };
            yield return new Warehouse { Name = "Warehouse 4 " };
            yield return new Warehouse { Name = "Warehouse 5 " };
        }
    }
}
