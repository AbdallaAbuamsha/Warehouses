using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Model;

namespace Warehouses.UI.Data
{
    public class LanguagesDataService : ILanguagesDataService
    {
        public IEnumerable<Language> GetAll()
        {
            yield return new Language { Name = "Language 1" };
            yield return new Language { Name = "Language 2" };
            yield return new Language { Name = "Language 3" };
            yield return new Language { Name = "Language 4" };
            yield return new Language { Name = "Language 5" };
        }
    }
}
