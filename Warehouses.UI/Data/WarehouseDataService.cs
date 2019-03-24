using System;
using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public class WarehouseDataService : IWarehouseDataService
    {
        public IEnumerable<Warehouse> GetAll()
        {
            yield return new Warehouse { Name = "Warehouse 1 ", ParentId = 1 };
            yield return new Warehouse { Name = "Warehouse 2 ", ParentId = 1 };
            yield return new Warehouse { Name = "Warehouse 3 ", ParentId = 3 };
            yield return new Warehouse { Name = "Warehouse 4 ", ParentId = 2 };
            yield return new Warehouse { Name = "Warehouse 5 ", ParentId = 2 };
        }

        public IEnumerable<Warehouse> GetByParentId(int id)
        {
            var warehouses = GetAll();
            foreach (Warehouse warehouse in warehouses)
            {
                if (warehouse.ParentId == id)
                {
                    yield return warehouse;
                }
            }
        }
    }
}
