using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Warehouses.UI.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AddUnitRelation.xaml
    /// </summary>
    public partial class AddUnitRelation : UserControl
    {
        public AddUnitRelation()
        {
            InitializeComponent();
        }
        //TODO: Make it accept floating points
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
