using Dzaba.Utils;
using Ninject;

namespace Dzaba.Mvvm.Windows
{
    internal static class ServiceLocator
    {
        private static IKernel container;

        public static void SetContainer(IKernel container)
        {
            Require.NotNull(container, nameof(container));
            ServiceLocator.container = container;
        }

        public static T Resolve<T>()
        {
            Require.NotNull(container, nameof(container));

            return container.Get<T>();
        }
    }
}
