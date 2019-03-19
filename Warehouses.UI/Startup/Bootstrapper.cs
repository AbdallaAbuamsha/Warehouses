using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Warehouses.UI.ViewModels;
using Warehouses.UI.Views;
using Warehouses.UI.Views.Popups;
using Warehouses.UI.Data;

namespace Warehouses.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AddMaterial>().AsSelf();            

            builder.RegisterType<AddBranch>().AsSelf();
            builder.RegisterType<AddBranchViewModel>().As<IAddBranchViewModel>();

            builder.RegisterType<AddWarehouse>().AsSelf();
            builder.RegisterType<AddWarehouseViewModels>().As<IAddWarehouseViewModels>();

            builder.RegisterType<OrganizationDataService>().As<IOrganizationDataService>();
            builder.RegisterType<BranchDataService>().As<IBranchDataService>();
            builder.RegisterType<WarehouseDataService>().As<IWarehouseDataService>();

            builder.RegisterType<AddOrganization>().AsSelf();
            builder.RegisterType<AddOrganizationViewModels>().As<IAddOrganizationViewModels>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            builder.RegisterType<MainMenuViewModel>().As<IMainMenuViewModel>();

            builder.RegisterType<LoginWindow>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();

            return builder.Build();
        }
    }
}
