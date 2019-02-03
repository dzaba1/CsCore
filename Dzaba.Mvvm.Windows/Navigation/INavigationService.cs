using System.Windows;

namespace Dzaba.Mvvm.Windows.Navigation
{
    public interface INavigationService
    {
        void ShowView<T>(object argument)
            where T : FrameworkElement;
        void ShowView<T>()
            where T : FrameworkElement;
        void SetStartView<T>()
            where T : FrameworkElement;
        void ShowStartView();
    }
}
