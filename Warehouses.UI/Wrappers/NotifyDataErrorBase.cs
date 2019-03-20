using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections;
using Warehouses.UI.ViewModels;

namespace FriendsOrganizer.UI.Wrappers
{
    public class NotifyDataErrorBase : ViewModelBase, INotifyDataErrorInfo
    {

        private Dictionary<string, List<string>> _errorByPropertyName
          = new Dictionary<string, List<string>>();
        public bool HasErrors => _errorByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorByPropertyName.ContainsKey(propertyName)
                ? _errorByPropertyName[propertyName]
                : null;
        }
        // helper method th rais the error changed event
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            base.OnPropertyChanged(nameof(HasErrors));
        }
        protected void AddError(string propertyName, string error)
        {
            if (!_errorByPropertyName.ContainsKey(propertyName))
            {
                _errorByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorByPropertyName[propertyName].Contains(error))
            {
                _errorByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }
        protected void ClearErrors(string propertyName)
        {
            if (_errorByPropertyName.ContainsKey(propertyName))
            {
                _errorByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);

            }
        }
    }
}
