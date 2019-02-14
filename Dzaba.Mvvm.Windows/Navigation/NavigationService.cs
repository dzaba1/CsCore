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
                SetView(argument, view);
            }
        }

        private void SetView(object argument, FrameworkElement view)
        {
            if (ActiveView != view)
            {
                var navigatable = view as INavigatable;
                if (navigatable == null && view.DataContext != null)
                {
                    navigatable = view.DataContext as INavigatable;
                }

                navigatable?.OnNavigate(argument);
                ShowView(view);
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

        private DelegateCommand _startViewCommand;
        public DelegateCommand StartViewCommand
        {
            get
            {
                if (_startViewCommand == null)
                    _startViewCommand = new DelegateCommand(OnStartView);
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

        public void ShowView(Type viewType)
        {
            Require.NotNull(viewType, nameof(viewType));

            var view = viewProvider.GetView(viewType);

            lock (syncLock)
            {
                ShowView(view);
            }
        }

        public void ShowView(Type viewType, object argument)
        {
            Require.NotNull(viewType, nameof(viewType));

            var view = viewProvider.GetView(viewType);

            lock (syncLock)
            {
                SetView(argument, view);
            }
        }
    }
}
