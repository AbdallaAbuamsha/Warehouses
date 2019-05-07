using FriendsOrganizer.UI.Wrappers;
using Warehouses.Model;

namespace Warehouses.UI.Wrappers
{
    public class OrganizationWrapper : WrapperBase<Organization>
    {
        public OrganizationWrapper(Organization model) : base(model)
        {

        }
        public long Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
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
