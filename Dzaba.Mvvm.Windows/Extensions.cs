using Dzaba.Utils;
using Ninject;
using Ninject.Syntax;
using System.ComponentModel;
using System.Windows;

namespace Dzaba.Mvvm.Windows
{
    public static class Extensions
    {
        public static bool IsInDesignMode(this UIElement element)
        {
            Require.NotNull(element, nameof(element));

            return DesignerProperties.GetIsInDesignMode(element);
        }

        public static void RegisterView<T>(this IKernel ioc, bool singleton = false)
            where T : FrameworkElement
        {
            Require.NotNull(ioc, nameof(ioc));

            if (singleton)
            {
                ioc.Bind<T>().ToSelf().InSingletonScope();
            }
            else
            {
                ioc.Bind<T>().ToSelf().InTransientScope();
            }
        }

        public static void RegisterViewModel<T>(this IKernel ioc, bool singleton = false)
            where T : INotifyPropertyChanged
        {
            Require.NotNull(ioc, nameof(ioc));

            if (singleton)
            {
                ioc.Bind<T>().ToSelf().InSingletonScope();
            }
            else
            {
                ioc.Bind<T>().ToSelf().InTransientScope();
            }
        }

        public static void RegisterView<TView, TViewModel>(this IKernel ioc, bool registerViewModel = true, bool singleton = false)
            where TView : FrameworkElement
            where TViewModel : INotifyPropertyChanged
        {
            Require.NotNull(ioc, nameof(ioc));

            if (registerViewModel)
            {
                ioc.RegisterViewModel<TViewModel>();
            }

            IBindingNamedWithOrOnSyntax<TView> registration;
            if (singleton)
            {
                registration = ioc.Bind<TView>().ToSelf().InSingletonScope();
            }
            else
            {
                registration = ioc.Bind<TView>().ToSelf().InTransientScope();
            }

            registration.WithPropertyValue(nameof(FrameworkElement.DataContext), i => i.Kernel.Get<TViewModel>());
        }
    }
}
