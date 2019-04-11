using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IMaterialDataService
    {
        IEnumerable<Material> GetAll();

        Material GetById(int id);

        IEnumerable<Unit> GetAddedUnits(int materialId);

        bool Save(Material model);

        bool Delete(Material model);
    }
}