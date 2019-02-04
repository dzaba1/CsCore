using Dzaba.Mvvm.Windows.Navigation;
using Dzaba.Utils;
using Ninject;
using System;
using System.Windows;

namespace Dzaba.Mvvm.Windows
{
    public static class AppHandler
    {
        public static void OnStartup<T>(Application current, Func<IKernel> iocFactory)
            where T : Window
        {
            try
            {
                Require.NotNull(current, nameof(current));
                Require.NotNull(iocFactory, nameof(iocFactory));

                var container = iocFactory();
                ServiceLocator.SetContainer(container);

                var mainWindow = container.Get<T>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                current?.Shutdown();
            }
        }

        public static void OnStartup<TWindow, TView>(Application current, Func<IKernel> iocFactory)
            where TWindow : Window
            where TView : FrameworkElement
        {
            try
            {
                Require.NotNull(current, nameof(current));
                Require.NotNull(iocFactory, nameof(iocFactory));

                var container = iocFactory();
                ServiceLocator.SetContainer(container);

                var navigation = container.Get<INavigationService>();
                navigation.SetStartView<TView>();
                navigation.ShowStartView();

                var mainWindow = container.Get<TWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                current?.Shutdown();
            }
        }
    }
}
