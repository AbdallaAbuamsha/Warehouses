using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace Warehouses.UI.ViewModels
{
    public class TreeViewItemViewModel : ViewModelBase
    {
        public long Id { get; }
        private string _displayMember;

        public TreeViewItemViewModel(long id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

    }
}
