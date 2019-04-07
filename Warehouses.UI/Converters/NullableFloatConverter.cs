using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Warehouses.UI.Converters
{
    public sealed class NullableFloatConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0F;
            else
                return value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;

            float result;
            if (!string.IsNullOrWhiteSpace(s) && float.TryParse(s, out result))
            {
                return result;
            }

            return null;
        }
    }
}