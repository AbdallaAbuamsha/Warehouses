using Autofac;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;
using Warehouses.UI.Startup;

namespace Warehouses.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel(IMainWIndowWarehouseSelectionViewModel mainWIndowWarehouseSelectionDropDownsViewModel)
        {
            MainWIndowWarehouseSelectionDropDownsViewModel = mainWIndowWarehouseSelectionDropDownsViewModel;
        }


        public void Load()
        {
            MainWIndowWarehouseSelectionDropDownsViewModel.Load();
        }
        public IMainWIndowWarehouseSelectionViewModel MainWIndowWarehouseSelectionDropDownsViewModel { get; set; }
    }
}
