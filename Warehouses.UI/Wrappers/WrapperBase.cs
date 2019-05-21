using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using System.ComponentModel;

namespace FriendsOrganizer.UI.Wrappers
{
    public class WrapperBase<T> : NotifyDataErrorBase
    {
        public T Model;
        public WrapperBase(T model)
        {
            this.Model = model;
        }
        protected virtual TValue GetValue<TValue>([CallerMemberName]string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }
        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName]string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            OnPropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName, value);
        }

        private void ValidatePropertyInternal(string propertyName, object value)
        {
            ClearErrors(propertyName);

            ValidateDataAnnotations(propertyName, value);

            NewMethod(propertyName);
        }

        private void NewMethod(string propertyName)
        {
            var errors = ValidateProperty(propertyName);
            if (errors == null)
                return;

            foreach (var error in errors)
            {
                AddError(propertyName, error);
            }
        }

        private void ValidateDataAnnotations(string propertyName, object value)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var context = new ValidationContext(Model) { MemberName = propertyName };
            Validator.TryValidateProperty(value, context, results);
            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }
        protected virtual IEnumerable<string> ValidateProperty(string propertyName)
        {
            return null;
        }        
    }
}
