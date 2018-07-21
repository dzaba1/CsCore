using System;

namespace Dzaba.Utils.Logging
{
    public interface ILoggerFactory
    {
        ILogger Create(Type ownerType);
    }
}
