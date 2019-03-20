using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface IUnitDataService
    {
        IEnumerable<Unit> GetAll();
    }
}