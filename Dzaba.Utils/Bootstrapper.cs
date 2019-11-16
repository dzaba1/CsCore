using Microsoft.Extensions.DependencyInjection;

namespace Dzaba.Utils
{
    public static class Bootstrapper
    {
        public static void RegisterUtils(this IServiceCollection container)
        {
            Require.NotNull(container, nameof(container));

            container.AddTransient<IDateTimeProvider, DateTimeProvider>();
            container.AddTransient<IGuidProvider, GuidProvider>();
            container.AddSingleton<IRandom, Random>();
        }
    }
}
