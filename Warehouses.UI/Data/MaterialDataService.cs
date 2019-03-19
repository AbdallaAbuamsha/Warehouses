using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public class MaterialDataService : IMaterialDataService
    {
        public IEnumerable<Material> GetAll()
        {
            yield return new Material { Name = "Material 1 " };
            yield return new Material { Name = "Material 2 " };
            yield return new Material { Name = "Material 3 " };
            yield return new Material { Name = "Material 4 " };
            yield return new Material { Name = "Material 5 " };
        }
    }
}
