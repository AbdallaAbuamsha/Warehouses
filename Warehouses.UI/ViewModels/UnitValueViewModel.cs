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
        private float _value;

        public UnitValueViewModel(int id, string name, float value)
        {
            this.Id = id;
            this.Name = name;
            this.Value = value;
        }

        public int Id { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

    }
}
