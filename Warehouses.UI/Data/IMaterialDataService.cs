using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IMaterialDataService
    {
        IEnumerable<Material> GetAll();
    }
}