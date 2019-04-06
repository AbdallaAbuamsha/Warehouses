using Autofac;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouses.UI.Properties;
using Warehouses.UI.Startup;
using Warehouses.UI.Views;
using Warehouses.UI.Views.Popups;

namespace Warehouses.UI.ViewModels
{
    public class MainMenuViewModel : IMainMenuViewModel
    {
        public MainMenuViewModel()
        {
            InputReceiptCommand = new DelegateCommand(InputReceiptExecute);
            OutputReceiptCommand = new DelegateCommand(OutputReceiptExecute);
            TransactionReceiptCommand = new DelegateCommand(TransactionReceiptExecute);
            NewOrganizationCommand = new DelegateCommand(NewOrganizationExecute);
            NewBranchCommand = new DelegateCommand(NewBranchExecute);
            NewWarehouseCommand = new DelegateCommand(NewWarehouseExecute);
            NewMaterialCommand = new DelegateCommand(NewMaterialExecute);
            NewUnitCommand = new DelegateCommand(NewUnitExecute);
            SettingsCommand = new DelegateCommand(SettingsCommandExecute);
            LogoutCommand = new DelegateCommand<Window>(OnLogoutCommandExecute);
        }

        private void OnLogoutCommandExecute(Window window)
        {
            Settings.Default.RememberMe = false;
            Settings.Default.Username = "";
            Settings.Default.Password = "";
            Settings.Default.Save();
            Bootstrapper.Builder.Resolve<LoginWindow>().Show();
            window.Close();
        }

        private void SettingsCommandExecute()
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
        }

        private void NewUnitExecute()
        {
            Bootstrapper.Builder.Resolve<AddUnit>().ShowDialog();
        }

        private void NewMaterialExecute()
        {
            Bootstrapper.Builder.Resolve<AddMaterial>().ShowDialog();
        }

        private void NewWarehouseExecute()
        {
            Bootstrapper.Builder.Resolve<AddWarehouse>().ShowDialog();
        }

        private void NewBranchExecute()
        {
            Bootstrapper.Builder.Resolve<AddBranch>().ShowDialog();
        }

        private void NewOrganizationExecute()
        {
            Bootstrapper.Builder.Resolve<AddOrganization>().ShowDialog();
        }

        private void TransactionReceiptExecute()
        {
            MessageBox.Show("TransactionReceiptExecute");
        }

        private void OutputReceiptExecute()
        {
            MessageBox.Show("OutputReceiptExecute");
        }

        private void InputReceiptExecute()
        {
            Bootstrapper.Builder.Resolve<Receipt>().ShowDialog(); ;
        }

        public ICommand InputReceiptCommand { get; set; }
        public ICommand OutputReceiptCommand { get; set; }
        public ICommand TransactionReceiptCommand { get; set; }
        public ICommand NewOrganizationCommand { get; set; }
        public ICommand NewBranchCommand { get; set; }
        public ICommand NewWarehouseCommand { get; set; }
        public ICommand NewMaterialCommand { get ; set ; }
        public ICommand NewUnitCommand { get ; set ; }
        public ICommand SettingsCommand { get ; set ; }
        public ICommand LogoutCommand { get; private set; }
    }
}
