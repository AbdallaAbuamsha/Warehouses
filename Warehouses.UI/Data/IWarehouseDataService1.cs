using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IWarehouseDataService1
    {
        IEnumerable<Warehouse> GetAll();
    }
}