using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public class BranchDataService : IBranchDataService
    {
        public bool Delete(Branch model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Branch> GetAll()
        {
            yield return new Branch{ Id = 1, Name = "Branch 1 ", ParentId = 1 };
            yield return new Branch{ Id = 2, Name = "Branch 2 ", ParentId = 1 };
            yield return new Branch{ Id = 3, Name = "Branch 3 ", ParentId = 3 };
            yield return new Branch{ Id = 4, Name = "Branch 4 ", ParentId = 2 };
            yield return new Branch{ Id = 5, Name = "Branch 5 ", ParentId = 2 };
        }

        public Branch GetById(int id)
        {
            return GetAll().First(f => f.Id == id);
        }

        public IEnumerable<Branch> GetByParentId(int id)
        {
            var branches = GetAll();
            foreach (Branch branch in branches)
            {
                if(branch.ParentId == id)
                {
                    yield return branch;
                }
            }
        }

        public IEnumerable<Organization> GetAllOrganizations()
        {
            return new OrganizationDataService().GetAll();
        }

        public bool HasSiblings()
        {
            return false;
        }

        public bool Save(Branch model)
        {
            return true;
        }
    }
}
