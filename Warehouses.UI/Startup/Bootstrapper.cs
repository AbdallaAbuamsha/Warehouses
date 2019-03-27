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
using Prism.Events;

namespace Warehouses.UI.Startup
{
    class Bootstrapper
    {
        public static readonly IContainer Builder = Bootstrap();

        private static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<Receipt>().AsSelf();
            builder.RegisterType<ReceiptViewModel>().As<IReceiptViewModel>();
            builder.RegisterType<ReceiptTableViewModel>().AsSelf();
            builder.RegisterType<ReceiptTableItemViewModel>().AsSelf();

            builder.RegisterType<OrganizationTreeViewItemViewModel>().AsSelf();
            builder.RegisterType<BranchTreeViewItemViewModel>().AsSelf();
            builder.RegisterType<WarehouseTreeViewItemViewModel>().AsSelf();

            builder.RegisterType<AddUnit>().AsSelf();
            builder.RegisterType<AddUnitViewModel>().As<IAddUnitViewModel>();
            builder.RegisterType<AddUnitRelationViewModel>().As<IAddUnitRelationViewModel>();

            builder.RegisterType<AddMaterial>().AsSelf();
            builder.RegisterType<AddMaterialViewModel>().As<IAddMaterialViewModel>();

            builder.RegisterType<AddMaterialNameDetailsViewModel>().As<IAddMaterialNameDetailsViewModel>();
            builder.RegisterType<MaterialDataService>().As<IMaterialDataService>();
            builder.RegisterType<LanguagesDataService>().As<ILanguagesDataService>();

            builder.RegisterType<AddMaterialUnitDetailsViewModel>().As<IAddMaterialUnitDetailsViewModel>();
            builder.RegisterType<UnitDataService>().As<IUnitDataService>();

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
            builder.RegisterType<MainWIndowWarehouseSelectionViewModel>().As<IMainWIndowWarehouseSelectionViewModel>();
            builder.RegisterType<MainMenuViewModel>().As<IMainMenuViewModel>();

            builder.RegisterType<LoginWindow>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();

            return builder.Build();
        }
    }
}
