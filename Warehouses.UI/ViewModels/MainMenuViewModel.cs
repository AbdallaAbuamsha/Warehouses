using Autofac;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouses.UI.Startup;
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
        }

        private void NewUnitExecute()
        {
            var addUnit = Bootstrapper.Builder.Resolve<AddUnit>();
            addUnit.ShowDialog();
        }

        private void NewMaterialExecute()
        {
            var addMaterial = Bootstrapper.Builder.Resolve<AddMaterial>();
            addMaterial.ShowDialog();
        }

        private void NewWarehouseExecute()
        {
            var addWarehouse = Bootstrapper.Builder.Resolve<AddWarehouse>();
            addWarehouse.ShowDialog();
        }

        private void NewBranchExecute()
        {
            var addBranch = Bootstrapper.Builder.Resolve<AddBranch>();
            addBranch.ShowDialog();
        }

        private void NewOrganizationExecute()
        {
            var addOrganization = Bootstrapper.Builder.Resolve<AddOrganization>();
            addOrganization.ShowDialog();
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
            MessageBox.Show("InputReceiptExecute");
        }

        public ICommand InputReceiptCommand { get; set; }
        public ICommand OutputReceiptCommand { get; set; }
        public ICommand TransactionReceiptCommand { get; set; }

        public ICommand NewOrganizationCommand { get; set; }
        public ICommand NewBranchCommand { get; set; }
        public ICommand NewWarehouseCommand { get; set; }
        public ICommand NewMaterialCommand { get ; set ; }
        public ICommand NewUnitCommand { get ; set ; }
    }
}
