using System;

namespace Dzaba.Utils.Logging
{
    public interface ILogger
    {
        void Trace(string message);
        void Trace(string message, Exception ex);

        void Debug(string message);
        void Debug(string message, Exception ex);

        void Info(string message);
        void Info(string message, Exception ex);

        void Warn(string message);
        void Warn(string message, Exception ex);

        void Error(string message);
        void Error(string message, Exception ex);

        void Critical(string message);
        void Critical(string message, Exception ex);
    }
}
