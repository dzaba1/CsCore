using Dzaba.Utils;
using Microsoft.Win32;
using System.Windows;

namespace Dzaba.Mvvm.Windows
{
    public interface IWindowPresenter
    {
        bool? ShowDialog(Window window);
        bool? ShowDialog(Window window, Window owner);
        void Show(Window window);
        void Show(Window window, Window owner);
        bool? ShowDialog(CommonDialog dialog);
        bool? ShowDialog(CommonDialog dialog, Window owner);
    }

    internal sealed class WindowPresenter : IWindowPresenter
    {
        public void Show(Window window)
        {
            Show(window, null);
        }

        public void Show(Window window, Window owner)
        {
            Require.NotNull(window, nameof(window));

            window.Owner = owner;
            window.Show();
        }

        public bool? ShowDialog(CommonDialog dialog)
        {
            return ShowDialog(dialog, null);
        }

        public bool? ShowDialog(Window window)
        {
            return ShowDialog(window, null);
        }

        public bool? ShowDialog(Window window, Window owner)
        {
            Require.NotNull(window, nameof(window));

            window.Owner = owner;
            return window.ShowDialog();
        }

        public bool? ShowDialog(CommonDialog dialog, Window owner)
        {
            Require.NotNull(dialog, nameof(dialog));

            return dialog.ShowDialog(owner);
        }
    }
}
