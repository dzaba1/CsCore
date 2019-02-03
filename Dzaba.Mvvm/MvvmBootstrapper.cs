using Dzaba.Utils;
using Ninject;

namespace Dzaba.Mvvm
{
    public static class MvvmBootstrapper
    {
        public static void RegisterMvvm(this IKernel ioc)
        {
            RegisterMvvm(ioc, new BootstrapOptions());
        }

        public static void RegisterMvvm(this IKernel ioc, BootstrapOptions options)
        {
            Require.NotNull(ioc, nameof(ioc));
            Require.NotNull(options, nameof(options));

            if (options.LongOperationPopupSingleton)
            {
                ioc.Bind<ILongOperationPopup>()
                    .To<LongOperationPopupViewModel>()
                    .InSingletonScope();
            }
            else
            {
                ioc.Bind<ILongOperationPopup>()
                    .To<LongOperationPopupViewModel>()
                    .InTransientScope();
            }

            ioc.Bind<IViewModelProvider>()
                .To<ViewModelProvider>()
                .InTransientScope();
        }
    }
}
