using System.Collections.Generic;
using Warehouses.Model;

namespace Warehouses.UI.Data
{
    public interface ILanguagesDataService
    {
        IEnumerable<Language> GetAll();
    }
}