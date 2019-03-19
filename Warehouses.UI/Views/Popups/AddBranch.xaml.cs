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
    /// Interaction logic for AddBranch.xaml
    /// </summary>
    public partial class AddBranch : Window
    {
        public AddBranch(IAddBranchViewModel addBranchViewModel)
        {
            InitializeComponent();
            AddBranchViewModel = addBranchViewModel;
            this.DataContext = addBranchViewModel;
            Loaded += AddBranch_Loaded;
        }

        private void AddBranch_Loaded(object sender, RoutedEventArgs e)
        {
            AddBranchViewModel.Load();
        }

        public IAddBranchViewModel AddBranchViewModel { get; set; }
    }
}
