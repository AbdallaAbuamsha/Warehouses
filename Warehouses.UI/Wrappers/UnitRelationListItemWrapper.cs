using FriendsOrganizer.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Wrappers
{
    class UnitRelationListItemWrapper : WrapperBase<int>
    {
        public UnitRelationListItemWrapper(int model) : base(model)
        {
        }

        public float Factor { get; set; }
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Factor):
                    //if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    //{
                        yield return "Robots are not valid friends";
                    //}
                    break;
            }
        }
    }
}
