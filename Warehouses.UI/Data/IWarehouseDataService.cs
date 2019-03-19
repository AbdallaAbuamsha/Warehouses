using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IWarehouseDataService
    {
        IEnumerable<Warehouse> GetAll();
    }
}