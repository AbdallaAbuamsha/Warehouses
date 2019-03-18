using System.Windows;
using System.Windows.Input;
using Warehouses.UI.ViewModels;

namespace Warehouses.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel _viewModel;
        public LoginWindow(LoginViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viewModel;
            Keyboard.Focus(Username);
        }
    }
}
