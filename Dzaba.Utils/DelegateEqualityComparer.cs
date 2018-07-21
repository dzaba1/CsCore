using System;
using System.Collections.Generic;

namespace Dzaba.Utils
{
    public class DelegateEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> comparer;
        private readonly Func<T, int> hashCodeFactory;

        public DelegateEqualityComparer(Func<T, T, bool> comparer, Func<T, int> hashCodeFactory)
        {
            Require.NotNull(comparer, nameof(comparer));
            Require.NotNull(hashCodeFactory, nameof(hashCodeFactory));

            this.comparer = comparer;
            this.hashCodeFactory = hashCodeFactory;
        }

        public bool Equals(T x, T y)
        {
            return comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return hashCodeFactory(obj);
        }
    }
}
