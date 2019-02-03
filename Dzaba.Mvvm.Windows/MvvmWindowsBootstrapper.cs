using Dzaba.Utils;
using Ninject;

namespace Dzaba.Mvvm.Windows
{
    public static class MvvmBootstrapper
    {
        public static void RegisterMvvm(this IKernel ioc)
        {
            Require.NotNull(ioc, nameof(ioc));

            ioc.Bind<IInteractionService>()
                .To<MessageBoxInteractionService>()
                .InTransientScope();
            ioc.Bind<IWindowPresenter>()
                .To<WindowPresenter>()
                .InTransientScope();
            ioc.Bind<IViewProvider>()
                .To<ViewProvider>()
                .InTransientScope();
        }
    }
}
