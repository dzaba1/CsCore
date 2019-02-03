using Dzaba.Utils;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Dzaba.Mvvm.Windows
{
    public static class WpfUtils
    {
        public static void RunOnMainThread(Action action)
        {
            RunOnMainThread(action, DispatcherPriority.Normal);
        }

        public static void RunOnMainThread(Action action, DispatcherPriority priority)
        {
            Require.NotNull(action, nameof(action));

            Application.Current.Dispatcher.Invoke(action, priority);
        }

        public static void SetCulture(CultureInfo culture)
        {
            Require.NotNull(culture, nameof(culture));

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}
