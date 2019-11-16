using Dzaba.Utils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.Utils
{
    public interface IApplicationComponentsManager
    {
        void OnApplicationStart();
        void OnApplicationEnd();
    }

    public class ApplicationComponentsManager : IApplicationComponentsManager
    {
        private readonly IApplicationComponent[] components;
        private readonly ILogger logger;

        public ApplicationComponentsManager(IEnumerable<IApplicationComponent> components,
            ILoggerFactory loggerFactory)
        {
            Require.NotNull(components, nameof(components));
            Require.NotNull(loggerFactory, nameof(loggerFactory));

            this.components = components.ToArray();
            logger = loggerFactory.Create(GetType());
        }

        public void OnApplicationEnd()
        {
            foreach (var component in components)
            {
                try
                {
                    component.OnApplicationEnd();
                }
                catch (Exception ex)
                {
                    logger.Error($"Failed to finish the {component.GetType()} component.", ex);
                }
            }
        }

        public void OnApplicationStart()
        {
            foreach (var component in components)
            {
                try
                {
                    component.OnApplicationStart();
                }
                catch (Exception ex)
                {
                    logger.Error($"Failed to start the {component.GetType()} component.", ex);
                }
            }
        }
    }
}
