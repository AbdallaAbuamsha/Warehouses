using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IBranchDataService
    {
        IEnumerable<Branch> GetAll();
        IEnumerable<Branch> GetByParentId(long id);
        IEnumerable<Organization> GetAllOrganizations();
        Branch GetById(long id);
        bool HasSiblings();
        bool Delete(Branch model);
        bool Save(Branch model);
    }
}