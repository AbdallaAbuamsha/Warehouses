using FriendsOrganizer.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Wrappers
{
    public class OrganizationWrapper : WrapperBase<Organization>
    {
        public OrganizationWrapper(Organization model) : base(model)
        {

        }

        public string Name
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
