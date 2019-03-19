using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IOrganizationDataService
    {
        IEnumerable<Organization> GetAll();
    }
}