using System.Windows;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views.Popups
{
    /// <summary>
    /// Interaction logic for AddUnit.xaml
    /// </summary>
    public partial class AddUnit : Window
    {
        public AddUnit(IAddUnitViewModel addUnitViewModel)
        {
            InitializeComponent();
            AddUnitViewModel = addUnitViewModel;
            this.DataContext = addUnitViewModel;
            Loaded += AddBranch_Loaded;
        }

        private void AddBranch_Loaded(object sender, RoutedEventArgs e)
        {
            AddUnitViewModel.Load();
        }

        public IAddUnitViewModel AddUnitViewModel { get; set; }
    }
}
