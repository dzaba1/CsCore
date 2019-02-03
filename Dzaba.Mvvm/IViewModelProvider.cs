using Dzaba.Utils;
using Ninject;
using System;
using System.ComponentModel;

namespace Dzaba.Mvvm
{
    public interface IViewModelProvider
    {
        INotifyPropertyChanged GetViewModel(Type viewModelType);
        T GetViewModel<T>() where T : INotifyPropertyChanged;
    }

    internal sealed class ViewModelProvider : IViewModelProvider
    {
        private readonly IKernel ioc;

        public ViewModelProvider(IKernel ioc)
        {
            Require.NotNull(ioc, nameof(ioc));
            this.ioc = ioc;
        }

        public INotifyPropertyChanged GetViewModel(Type viewModelType)
        {
            Require.NotNull(viewModelType, nameof(viewModelType));

            if (!viewModelType.ImplementsInterface<INotifyPropertyChanged>())
            {
                throw new ArgumentException($"The provided type {viewModelType.FullName} does not implement System.ComponentModel.INotifyPropertyChanged.");
            }

            return (INotifyPropertyChanged)ioc.Get(viewModelType);
        }

        public T GetViewModel<T>() where T : INotifyPropertyChanged
        {
            return ioc.Get<T>();
        }
    }
}
