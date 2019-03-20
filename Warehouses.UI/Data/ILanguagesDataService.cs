using System.Collections.Generic;
using Warehouse.Model;

namespace Warehouses.UI.Data
{
    public interface ILanguagesDataService
    {
        IEnumerable<Language> GetAll();
    }
}