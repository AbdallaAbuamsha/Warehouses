using FriendsOrganizer.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Wrappers
{
    public class UnitWrapper : WrapperBase<Unit>
    {
        public UnitWrapper(Unit model) : base(model)
        {

        }
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Symbol
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
