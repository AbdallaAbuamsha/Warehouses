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

namespace Warehouses.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LoginWindow login = Bootstrapper.Builder.Resolve<LoginWindow>();
            login.Show();
        }
    }
}
