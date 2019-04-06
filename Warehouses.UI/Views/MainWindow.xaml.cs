using System.Windows;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMainViewModel _mainWindowViewModel;
        public MainWindow(IMainViewModel mainWindowViewModel)
        {

            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            this.DataContext = mainWindowViewModel;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindowViewModel.Load();
        }
    }
}
