using Dzaba.Mvvm.Navigation;
using Dzaba.Utils;
using System;
using System.Windows;

namespace Dzaba.Mvvm.Windows.Navigation
{
    internal sealed class NavigationService : BaseViewModel, INavigationService
    {
        private Type startViewType;
        private readonly IViewProvider viewProvider;
        private readonly object syncLock = new object();
        private readonly IInteractionService interactionService;

        public NavigationService(IViewProvider viewProvider,
            IInteractionService interactionService)
        {
            Require.NotNull(viewProvider, nameof(viewProvider));
            Require.NotNull(interactionService, nameof(interactionService));

            this.interactionService = interactionService;
            this.viewProvider = viewProvider;
        }

        public void ShowView<T>() where T : FrameworkElement
        {
            var view = viewProvider.GetView<T>();

            lock (syncLock)
            {
                ShowView(view);
            }
        }

        public void ShowView<T>(object argument) where T : FrameworkElement
        {
            var view = viewProvider.GetView<T>();

            lock (syncLock)
            {
                if (ActiveView != view)
                {
                    if (view is INavigatable)
                    {
                        ((INavigatable)view).OnNavigate(argument);
                    }

                    if (view.DataContext != null && view.DataContext is INavigatable)
                    {
                        ((INavigatable)view.DataContext).OnNavigate(argument);
                    }

                    ShowView(view);
                }
            }
        }

        private void ShowView(FrameworkElement view)
        {
            if (ActiveView != view)
            {
                ActiveView = view;
            }
        }

        public void SetStartView<T>()
            where T : FrameworkElement
        {
            lock (syncLock)
            {
                startViewType = typeof(T);
            }
        }

        public void ShowStartView()
        {
            lock (syncLock)
            {
                var view = viewProvider.GetView(startViewType);
                ShowView(view);
            }
        }

        private FrameworkElement _activeView;
        public FrameworkElement ActiveView
        {
            get { return _activeView; }
            private set
            {
                _activeView = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _startViewCommand;
        public RelayCommand StartViewCommand
        {
            get
            {
                if (_startViewCommand == null)
                    _startViewCommand = new RelayCommand(OnStartView);
                return _startViewCommand;
            }
        }

        private void OnStartView()
        {
            try
            {
                ShowStartView();
            }
            catch (Exception ex)
            {
                interactionService.ShowError(ex);
            }
        }
    }
}
