using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace FriendsOrganizer.UI.Wrappers
{
    public class FriendWrapper : WrapperBase<Material>
    {
        public FriendWrapper(Material model) : base(model)
        {

        }
        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Email {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (FirstName.Equals("Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robors are not valid frieds";
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
