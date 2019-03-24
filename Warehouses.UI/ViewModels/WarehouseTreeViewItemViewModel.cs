using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.ViewModels
{
    public class WarehouseTreeViewItemViewModel
    {
        public WarehouseTreeViewItemViewModel()
        {

        }
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
