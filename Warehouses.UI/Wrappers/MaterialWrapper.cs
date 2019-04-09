using FriendsOrganizer.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouses.Model;

namespace Warehouses.UI.Wrappers
{
    public class MaterialWrapper : WrapperBase<Material>
    {
        public MaterialWrapper(Material model) : base(model)
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
        public string Barcode
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Serial
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public float MaximumSaleAmount
        {
            get { return GetValue<float>(); }
            set { SetValue(value); }
        }
        public float MinimumSaleAmount
        {
            get { return GetValue<float>(); }
            set { SetValue(value); }
        }
        public float DazonElementsCount
        {
            get { return GetValue<float>(); }
            set { SetValue(value); }
        }
        public float FreeReferencesAmount
        {
            get { return GetValue<float>(); }
            set { SetValue(value); }
        }
    }
}
