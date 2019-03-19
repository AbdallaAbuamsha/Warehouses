using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views.Popups
{
    /// <summary>
    /// Interaction logic for AddOrganization.xaml
    /// </summary>
    public partial class AddWarehouse : Window
    {
        public AddWarehouse(IAddWarehouseViewModels addWarehouseViewModels)
        {
            InitializeComponent();
            AddWarehouseViewModels = addWarehouseViewModels;
            this.DataContext = addWarehouseViewModels;
            Loaded += AddWarehouse_Loaded;
        }

        private void AddWarehouse_Loaded(object sender, RoutedEventArgs e)
        {
            AddWarehouseViewModels.Load();
        }

        public IAddWarehouseViewModels AddWarehouseViewModels { get; set; }
    }
}
