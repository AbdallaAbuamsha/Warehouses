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
using Warehouses.UI.Views.Services;

namespace Warehouses.UI.Startup
{
    class Bootstrapper
    {
        public static readonly IContainer Builder = Bootstrap();

        private static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>().SingleInstance();

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

            builder.RegisterType<OrganizationDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(OrganizationDetailViewModel));
            builder.RegisterType<BranchDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(BranchDetailViewModel));
            builder.RegisterType<WarehouseDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(WarehouseDetailViewModel));
            builder.RegisterType<MaterialDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(MaterialDetailViewModel));
            builder.RegisterType<UnitDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(UnitDetailViewModel));
            builder.RegisterType<WarehousesNavigationViewModel>()
                .Keyed<INavigationViewModel>(nameof(WarehousesNavigationViewModel));
            builder.RegisterType<MaterialNavigationViewModel>()
                .Keyed<INavigationViewModel>(nameof(MaterialNavigationViewModel));
            builder.RegisterType<UnitNavigationViewModel>()
                .Keyed<INavigationViewModel>(nameof(UnitNavigationViewModel));
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<MainMenuViewModel>().As<IMainMenuViewModel>();
            builder.RegisterType<MaterialDetailViewModel>().AsSelf();
            builder.RegisterType<UnitDetailViewModel>().AsSelf();

            builder.RegisterType<LoginWindow>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<UserDataService>().As<IUserDataService>();

            return builder.Build();
        }
    }
}
