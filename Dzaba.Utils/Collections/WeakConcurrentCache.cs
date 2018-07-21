using System;
using System.Collections.Generic;

namespace Dzaba.Utils.Collections
{
    internal class WeakReferenceEntry<TKey, TValue> : ICacheEntry<TValue>
    {
        private WeakReference reference;
        private readonly Func<TKey, TValue> valueFactory;

        public WeakReferenceEntry(TKey key, Func<TKey, TValue> valueFactory)
        {
            Require.NotNull(valueFactory, nameof(valueFactory));
            this.valueFactory = valueFactory;

            Key = key;
        }

        public TKey Key { get; }

        public TValue GetValue()
        {
            if (reference == null || !reference.IsAlive)
            {
                var value = valueFactory(Key);
                reference = new WeakReference(value, false);
                return value;
            }

            return (TValue)reference.Target;
        }
    }

    public class WeakConcurrentCache<TKey, TValue> : ConcurrentCacheBase<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        public WeakConcurrentCache()
        {

        }

        public WeakConcurrentCache(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {

        }

        public void Add(TKey key, Func<TKey, TValue> valueFactory)
        {
            AddEntrySynchronized(key, CreateNewEntry(key, valueFactory));
        }

        protected override ICacheEntry<TValue> CreateNewEntry(TKey key, Func<TKey, TValue> valueFactory)
        {
            return new WeakReferenceEntry<TKey, TValue>(key, valueFactory);
        }
    }
}
