using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Warehouses.UI.Views;
using Warehouses.UI.Startup;
using Autofac;

namespace Warehouses.UI.ViewModels
{
    public class LoginViewModel
    {
        private String _username;
        private String _password;
        private bool _rememberMe;

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(OnLoginExecute, OnCanLoginExcecute);
        }

        private void OnLoginExecute()
        {
            MainWindow mainWindow = Bootstrapper.Builder.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private bool OnCanLoginExcecute()
        {
            return true;
        }

        public String Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public bool RememberMe
        {
            get { return _rememberMe; }
            set { _rememberMe = value; }
        }

        public ICommand LoginCommand { get; set; }
    }
}
