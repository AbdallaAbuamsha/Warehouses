using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouses.UI.ViewModels
{
    public class UnitValueViewModel : ViewModelBase
    {
        private string _name;
        private float _quantity;

        public UnitValueViewModel(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public long Id { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public float Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

    }
}
