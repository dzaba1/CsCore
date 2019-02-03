using Dzaba.Utils;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Dzaba.Mvvm
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            RaisePropertyChanged(ReflectionUtils.GetPropertyName(property));
        }

        protected void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(property))
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
