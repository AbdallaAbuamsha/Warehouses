using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warehouses.UI.Events;

namespace Warehouses.UI.ViewModels
{
    public class NavigationItemViewModel : ViewModelBase
    { 
        private string _name;
        private IEventAggregator _eventAggregator;
        private string _detailViewModelName;

    public NavigationItemViewModel(long id, string name,
      string detailViewModelName,
      IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
        Id = id;
        Name = name;
        _detailViewModelName = detailViewModelName;
        OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
    }

    public long Id { get; }

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public ICommand OpenDetailViewCommand { get; }

    private void OnOpenDetailViewExecute()
    {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                  .Publish(
              new OpenDetailViewEventArgs
              {
                  ViewModelName = _detailViewModelName
          });
    }
}
}