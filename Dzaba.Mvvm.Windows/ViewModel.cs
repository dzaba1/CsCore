using System;
using System.Windows;

namespace Dzaba.Mvvm.Windows
{
    public static class ViewModel
    {
        public static Type GetType(DependencyObject obj)
        {
            return (Type)obj.GetValue(TypeProperty);
        }

        public static void SetType(DependencyObject obj, Type value)
        {
            obj.SetValue(TypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.RegisterAttached("Type", typeof(Type), typeof(ViewModel), new FrameworkPropertyMetadata(null, OnTypeChanged));

        private static void OnTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;

            if (fe != null && !fe.IsInDesignMode() && e.NewValue != null)
            {
                Type viewModelType = (Type)e.NewValue;
                var viewModelProvider = ServiceLocator.Resolve<IViewModelProvider>();
                var vm = viewModelProvider.GetViewModel(viewModelType);
                fe.DataContext = vm;
            }
        }
    }
}
