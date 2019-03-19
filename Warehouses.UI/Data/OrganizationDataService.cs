using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public class OrganizationDataService : IOrganizationDataService
    {
        public IEnumerable<Organization> GetAll()
        {
            yield return new Organization { Name = "Organization 1 " };
            yield return new Organization { Name = "Organization 2 " };
            yield return new Organization { Name = "Organization 3 " };
            yield return new Organization { Name = "Organization 4 " };
            yield return new Organization { Name = "Organization 5 " };
        }
    }
}
