using FriendsOrganizer.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Wrappers
{
    public class BranchWrapper : WrapperBase<Branch>
    {
        public BranchWrapper(Branch model) : base(model)
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

        public string Code
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Location
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
