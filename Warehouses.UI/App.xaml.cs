using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Warehouses.UI.Startup;
using Warehouses.UI.Views;
using Warehouses.UI.Properties;
using Warehouses.UI.Data;

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
    }
}
