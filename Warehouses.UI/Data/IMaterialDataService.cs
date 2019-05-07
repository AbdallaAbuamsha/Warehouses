using System.Collections.Generic;
using System.Collections.ObjectModel;
using Warehouses.Model;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Data
{
    public interface IMaterialDataService
    {
        IEnumerable<Material> GetAll();

        Material GetById(long id);

        IEnumerable<Unit> GetAddedUnits(int materialId);

        bool Save(Material model);

        bool Delete(Material model);
        
        IEnumerable<Unit> GetAllUnRelatedUnits(long materailId ,long unitId);

        IEnumerable<Unit> GetAllUnits(long materialId);
    }
}