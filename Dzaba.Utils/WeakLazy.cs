using System;

namespace Dzaba.Utils
{
    public class WeakLazy<T>
    {
        private WeakReference reference;
        private readonly Func<T> valueFactory;
        private readonly object syncLock = new object();

        public WeakLazy(Func<T> valueFactory)
        {
            Require.NotNull(valueFactory, nameof(valueFactory));
            this.valueFactory = valueFactory;
        }

        public T Value
        {
            get
            {
                lock (syncLock)
                {
                    if (reference == null || !reference.IsAlive)
                    {
                        var value = valueFactory();
                        reference = new WeakReference(value, false);
                        return value;
                    }

                    return (T)reference.Target;
                }
            }
        }
    }
}
