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
            yield return new Organization { Id = 1, Name = "Organization 1 " };
            yield return new Organization { Id = 2, Name = "Organization 2 ", Location = "Rukn al-deen, Ibn-al3ameed" };
            yield return new Organization { Id = 3, Name = "Organization 3 ", Location = "Al-Mezzah, Autostrad" };
            yield return new Organization { Id = 4, Name = "Organization 4 " };
            yield return new Organization { Id = 5, Name = "Organization 5 " };
        }
        public Organization GetById(int id)
        {
            var organizations = GetAll();
            return organizations.First(o => o.Id == id);
        }
        public bool Save(Organization organization)
        {
            return true;
        }
        public bool Delete(Organization organization)
        {
            return true;
        }

        public bool HasSiblings()
        {
            return false;
        }
    }
}
