using Dzaba.Utils;
using Ninject;
using System;
using System.ComponentModel;
using System.Windows;

namespace Dzaba.Mvvm.Windows
{
    public interface IViewProvider
    {
        FrameworkElement GetView(Type viewType);
        T GetView<T>() where T : FrameworkElement;
    }

    internal sealed class ViewProvider : IViewProvider
    {
        private readonly IKernel ioc;

        public ViewProvider(IKernel ioc)
        {
            Require.NotNull(ioc, nameof(ioc));
            this.ioc = ioc;
        }

        public FrameworkElement GetView(Type viewType)
        {
            WpfUtils.CheckForFrameworkElementType(viewType);

            return (FrameworkElement)ioc.Get(viewType);
        }

        public T GetView<T>() where T : FrameworkElement
        {
            return ioc.Get<T>();
        }
    }
}
