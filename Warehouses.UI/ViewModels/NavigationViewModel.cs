using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Warehouses.Model;
using Warehouses.UI.Data;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public abstract class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        public IEventAggregator eventAggregator;

        public NavigationViewModel(

            IEventAggregator eventAggregator)
        {

            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public abstract void Load();

        protected abstract void AfterDetailDeleted(AfterDetailDeletedEventArgs args);

        protected abstract void AfterDetailDeleted(ObservableCollection<TreeViewItemViewModel> items,
          AfterDetailDeletedEventArgs args);

        protected abstract void AfterDetailSaved(AfterDetailSavedEventArgs args);

        protected abstract void AfterDetailSaved(ObservableCollection<TreeViewItemViewModel> items,
          AfterDetailSavedEventArgs args);
    }
}
