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
        public IEnumerable<Branch> GetAll()
        {
            yield return new Branch{ Name = "Branch 1 " };
            yield return new Branch{ Name = "Branch 2 " };
            yield return new Branch{ Name = "Branch 3 " };
            yield return new Branch{ Name = "Branch 4 " };
            yield return new Branch{ Name = "Branch 5 " };
        }
    }
}
