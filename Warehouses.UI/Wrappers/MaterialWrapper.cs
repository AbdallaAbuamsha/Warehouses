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

        public long Id
        {
            get { return GetValue<long>(); }
            set { SetValue(value); }
        }
        //public string Name
        //{
        //    get { return GetValue<string>(); }
        //    set { SetValue(value); }
        //}
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string LatinName
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
        //public string Serial
        //{
        //    get { return GetValue<string>(); }
        //    set { SetValue(value); }
        //}
        public decimal? MaximumSaleAmount
        {
            get { return GetValue<decimal?>(); }
            set { SetValue(value); }
        }
        public decimal? MinimumSaleAmount
        {
            get { return GetValue<decimal?>(); }
            set { SetValue(value); }
        }
        //public decimal? DazonElementsCount
        //{
        //    get { return GetValue<decimal?>(); }
        //    set { SetValue(value); }
        //}
        public decimal? FreeReferencesAmount
        { 
            get { return GetValue<decimal?>(); }
            set { SetValue(value); }
        }

        public Organization SelectedOrganization
        {
            get { return GetValue<Organization>(); }
            set { SetValue(value); }
        }

        public Unit SelectedUnit
        {
            get { return GetValue<Unit>(); }
            set { SetValue(value); }
        }
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(SelectedOrganization):
                    if (SelectedOrganization == null)
                    {
                        yield return "Organization is required";
                    }
                    break;
                case nameof(SelectedUnit):
                    if (SelectedUnit == null)
                    {
                        yield return "Basic unit is required";
                    }
                    break;
            }
        }

    }
}
