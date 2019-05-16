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
        public long Id
        {
            get { return GetValue<long>(); }
            set { SetValue(value); }
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public decimal? Factor
        {
            get { return GetValue<decimal?>(); }
            set { SetValue(value); }
        }
    }
}
