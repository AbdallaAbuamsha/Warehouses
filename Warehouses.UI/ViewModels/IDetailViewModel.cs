using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.UI.ViewModels
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int id);
        int Id { get; }
    }
}