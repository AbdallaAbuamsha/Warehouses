using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class GetVoidReasonViewModel : ViewModelBase
    {
        IEventAggregator _eventAggregator;
        public GetVoidReasonViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DoneCommand = new DelegateCommand<Window>(OnDone);
            CancelCommand = new DelegateCommand<Window>(OnCancel);
        }

        private void OnCancel(Window window)
        {
            window.Close();
        }

        private void OnDone(Window window)
        {
            _eventAggregator.GetEvent<GetVoidReasonEvent>().Publish(VoidReason);
            window.Close();
        }

        private string _voidReason;

        public string VoidReason
        {
            get
            {
                return _voidReason;
            }
            set
            {
                _voidReason = value;
                OnPropertyChanged();
            }
        }
        public ICommand DoneCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
