using System.Collections.Generic;
using Warehouses.Model;
namespace Warehouses.UI.Data
{
    public interface IWarehouseDataService
    {
        IEnumerable<Model.Warehouse> GetAll();
        IEnumerable<Warehouse> GetByParentId(int id);
        Warehouse GetById(int id);
        IEnumerable<Organization> GetAllOrganizations();
        IEnumerable<Branch> GetAllBranches();
        bool HasSiblings();
        bool Delete(Warehouse model);
        bool Save(Warehouse model);
    }
}