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
            yield return new Material { Name = "Material 1 ", Code="111" };
            yield return new Material { Name = "Material 2 ", Code="222" };
            yield return new Material { Name = "Material 3 ", Code="333" };
            yield return new Material { Name = "Material 4 ", Code="444" };
            yield return new Material { Name = "Material 5 ", Code="555" };
        }
    }
}
