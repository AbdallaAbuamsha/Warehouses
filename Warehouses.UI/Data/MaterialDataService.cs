using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Warehouses.Model;
using Warehouses.UI.ViewModels;

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

        public Material GetById(int id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }
        public IEnumerable<Unit> GetAddedUnits(int materialId)
        {
            yield return new Unit { Name = "Unit 1 " };
            yield return new Unit { Name = "Unit 2 " };
            yield return new Unit { Name = "Unit 3 " };
        }
        public bool Save(Material model)
        {
            return true;
        }
        public bool Delete(Material model)
        {
            return true;
        }

        public IEnumerable<Unit> GetAllUnRelatedUnits(int materailId, int unitId)
        {
            return new UnitDataService().GetAll();
        }

        public IEnumerable<Unit> GetAllUnits(int materialId)
        {
            return new UnitDataService().GetAll();
        }
    }
}
