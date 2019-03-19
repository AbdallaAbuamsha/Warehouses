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
    /// Interaction logic for AddMaterial.xaml
    /// </summary>
    public partial class AddMaterial : Window
    {
        public AddMaterial(IAddMaterialViewModel addMaterialViewModel)
        {
            InitializeComponent();
            AddMaterialViewModel = addMaterialViewModel;
            this.DataContext = addMaterialViewModel;
            Loaded += AddMaterial_Loaded;
        }

        private void AddMaterial_Loaded(object sender, RoutedEventArgs e)
        {
            AddMaterialViewModel.Load();
        }

        public IAddMaterialViewModel AddMaterialViewModel { get; set; }
    }
}
