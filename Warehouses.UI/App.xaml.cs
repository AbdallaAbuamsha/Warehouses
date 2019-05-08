using System.Windows;
using Autofac;
using Warehouses.UI.Startup;
using Warehouses.UI.Views;
using Warehouses.UI.Properties;
using Warehouses.UI.Data;
using System;

namespace Warehouses.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Settings.Default.RememberMe == true)
            {
                string username = Settings.Default.Username;
                string password = Settings.Default.Password;
                var user = new UserDataService().Login(username, password);
                if (user != null)
                {
                    MainWindow mainWindow = Bootstrapper.Builder.Resolve<MainWindow>();
                    mainWindow.Show();
                }
            }
            else
            {
                LoginWindow login = Bootstrapper.Builder.Resolve<LoginWindow>();
                login.Show();
            }
        }
        private void Application_DispatcherUnhandledException(object sender,
          System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(Application.Current.FindResource("server_error").ToString()
              + Environment.NewLine + e.Exception.Message, "Unexpected error");
            e.Handled = true;
        }
    }
}
