using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Warehouses.UI.MyControls
{
    public class DoubleTextBox : TextBox
    {
        static DoubleTextBox()
        {
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            e.Handled = !DoubleCharChecker(e.Text);
            base.OnTextInput(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            e.Handled = (e.Key == Key.Space);
            base.OnPreviewKeyDown(e);
        }

        private bool DoubleCharChecker(string str)
        {
            return IsTextAllowed(str);
            //foreach (char c in str)
            //{
            //    if (c.Equals(','))
            //        return true;

            //    else if (c.Equals('.'))
            //        return true;

            //    else if (Char.IsNumber(c))
            //        return true;
            //}
            //return false;
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}