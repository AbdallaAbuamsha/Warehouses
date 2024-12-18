﻿using Autofac;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Warehouses.UI.Events;
using Warehouses.UI.Startup;

namespace Warehouses.UI.ViewModels
{
    public class ReceiptTableViewModel
    {
        private static int Increaser = 0;
        private IEventAggregator _eventAggregator;

        public ReceiptTableViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RowsItems = new ObservableCollection<ReceiptTableItemViewModel>();
            Delete = new DelegateCommand<ReceiptTableItemViewModel>(ExecuteDeleteCommand, ExecuteCanDeleteCommand);
            NewLine = new DelegateCommand(AddRow);
            _eventAggregator.GetEvent<AddReceiptRowEvent>().Subscribe(AddRow);
            RowsItems.Add(Bootstrapper.Builder.Resolve<ReceiptTableItemViewModel>(new NamedParameter("id", ++Increaser)));
        }

        //private void CreateNewLine()
        //{
        //    _eventAggregator.GetEvent<AddReceiptRowEvent>().Publish();
        //}

        private void AddRow()
        {
            RowsItems.Add(Bootstrapper.Builder.Resolve<ReceiptTableItemViewModel>(new NamedParameter("id", ++Increaser)));
        }

        private void ExecuteDeleteCommand(ReceiptTableItemViewModel item)
        {
            Increaser--;
            RowsItems.Remove(item);
            _eventAggregator.GetEvent<DeleteReceiptRowEvent>().Publish(item.Id);
        }

        private bool ExecuteCanDeleteCommand(ReceiptTableItemViewModel item)
        {
            return true;
        }

        public ObservableCollection<ReceiptTableItemViewModel> RowsItems { get; set; }
        public ICommand Delete { get; set; }
        public ICommand NewLine { get; set; }
    }
}
