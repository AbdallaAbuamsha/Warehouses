using System;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Warehouses.UI.Views;
using Warehouses.UI.Startup;
using Autofac;
using Warehouses.UI.Data;
using Warehouses.UI.Views.Services;
using Warehouses.UI.Views.Popups;
using Warehouses.UI.Properties;
using Warehouses.Model;
using Warehouses.UI.Helper;

namespace Warehouses.UI.ViewModels
{
    public class LoginViewModel
    {
        private String _username;
        private String _password;
        private bool _rememberMe;
        private IUserDataService _userService;
        private IMessageDialogService _messageService;

        public LoginViewModel(IUserDataService userService, IMessageDialogService messageService)
        {            
            _userService = userService;
            _messageService = messageService;
            LoginCommand = new DelegateCommand<Window>(OnLoginExecute);
            OpenSettingsCommand = new DelegateCommand(OnOpenSettingsCommandExecute);
        }

        private void OnLoginExecute(Window window)
        {
            //var user = _userService.Login(Username, Password);
            ResultObject resultObject = BusinessLayer.User_BL.Login(Username, Password, AppConstants.ARABIC);
            if (resultObject.Code <= AppConstants.ERROR_CODE)
            {
                _messageService.ShowInfoDialog(resultObject.Message);
                return;
            }
            var user = (User)resultObject.Data;
            UserSingleton.GetUser().User = user;
            if (RememberMe)
            {
                Settings.Default.RememberMe = true;
                Settings.Default.Username = Username;
                Settings.Default.Password = Password;
                Settings.Default.Save();
            }
            MainWindow mainWindow = Bootstrapper.Builder.Resolve<MainWindow>();
            mainWindow.Show();
            window.Close();
        }
        public String Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public String Password
        {
            get {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public bool RememberMe
        {
            get { return _rememberMe; }
            set { _rememberMe = value; }
        }    

        public ICommand LoginCommand { get; set; }
        public ICommand OpenSettingsCommand { get; set; }

        private void OnOpenSettingsCommandExecute()
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
        }
    }
}
