using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ctacke.Xamarin.MVVM
{
    public abstract class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T storageVariable, T value, bool alwaysRaiseEvent = false, [CallerMemberName] string propertyName = null)
        {
            // has the value changed (and we're not always raising the event)
            if (!alwaysRaiseEvent && EqualityComparer<T>.Default.Equals(storageVariable, value))
            {
                return;
            }

            // store the new value
            storageVariable = value;
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }

        protected void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            var me = (MemberExpression)property.Body;
            RaisePropertyChanged(me.Member.Name);
        }
    }
}
