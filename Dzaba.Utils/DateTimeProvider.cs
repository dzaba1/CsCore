using System;

namespace Dzaba.Utils
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrent();
        DateTime GetCurrentUtc();
        DateTime GetCurrentDate();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Today;
        }

        public DateTime GetCurrent()
        {
            return DateTime.Now;
        }

        public DateTime GetCurrentUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
