using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IBranchDataService
    {
        IEnumerable<Branch> GetAll();
        IEnumerable<Branch> GetByParentId(int id);
        IEnumerable<Organization> GetAllOrganizations();
        Branch GetById(int id);
        bool HasSiblings();
        bool Delete(Branch model);
        bool Save(Branch model);
    }
}