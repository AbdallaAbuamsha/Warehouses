using System.Collections.Generic;
using Warehouses.Model;
namespace Warehouses.UI.Data
{
    public interface IWarehouseDataService
    {
        IEnumerable<Model.Warehouse> GetAll();
        IEnumerable<Warehouse> GetByParentId(int id);
    }
}