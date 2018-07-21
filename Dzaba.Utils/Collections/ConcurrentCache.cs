using System;
using System.Collections.Generic;

namespace Dzaba.Utils.Collections
{
    internal class StaticEntry<TValue> : ICacheEntry<TValue>
    {
        public StaticEntry(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; }

        public TValue GetValue()
        {
            return Value;
        }
    }

    public class ConcurrentCache<TKey, TValue> : ConcurrentCacheBase<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        public ConcurrentCache()
        {

        }

        public ConcurrentCache(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {

        }

        public void Add(TKey key, TValue value)
        {
            AddEntrySynchronized(key, new StaticEntry<TValue>(value));
        }

        protected override ICacheEntry<TValue> CreateNewEntry(TKey key, Func<TKey, TValue> valueFactory)
        {
            Require.NotNull(valueFactory, nameof(valueFactory));

            return new StaticEntry<TValue>(valueFactory(key));
        }
    }
}
