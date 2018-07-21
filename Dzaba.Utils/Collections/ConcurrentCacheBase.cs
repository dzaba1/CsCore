using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dzaba.Utils.Collections
{
    public abstract class ConcurrentCacheBase<TKey, TValue> : ICache<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private readonly Dictionary<TKey, ICacheEntry<TValue>> dict;

        protected ConcurrentCacheBase()
        {
            dict = new Dictionary<TKey, ICacheEntry<TValue>>();
        }

        protected ConcurrentCacheBase(IEqualityComparer<TKey> comparer)
        {
            Require.NotNull(comparer, nameof(comparer));

            dict = new Dictionary<TKey, ICacheEntry<TValue>>(comparer);
        }

        protected object SyncLock { get; } = new object();

        public IEnumerable<TKey> Keys
        {
            get
            {
                lock (SyncLock)
                {
                    return dict.Keys.ToArray();
                }
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                lock (SyncLock)
                {
                    return dict.Values.Select(x => x.GetValue()).ToArray();
                }
            }
        }

        public int Count
        {
            get
            {
                lock (SyncLock)
                {
                    return dict.Keys.Count;
                }
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (SyncLock)
                {
                    return dict[key].GetValue();
                }
            }
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            Require.NotNull(valueFactory, nameof(valueFactory));

            lock (SyncLock)
            {
                ICacheEntry<TValue> entry;
                if (!dict.TryGetValue(key, out entry))
                {
                    entry = CreateNewEntry(key, valueFactory);
                    dict.Add(key, entry);
                }

                return entry.GetValue();
            }
        }

        protected void AddEntrySynchronized(TKey key, ICacheEntry<TValue> entry)
        {
            Require.NotNull(entry, nameof(entry));

            lock (SyncLock)
            {
                dict.Add(key, entry);
            }
        }

        protected abstract ICacheEntry<TValue> CreateNewEntry(TKey key, Func<TKey, TValue> valueFactory);

        public bool ContainsKey(TKey key)
        {
            lock (SyncLock)
            {
                return dict.ContainsKey(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (SyncLock)
            {
                ICacheEntry<TValue> entry;
                var result = dict.TryGetValue(key, out entry);

                if (result)
                {
                    value = entry.GetValue();
                }
                else
                {
                    value = default(TValue);
                }

                return result;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            lock (SyncLock)
            {
                return dict
                    .Select(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value.GetValue()))
                    .ToList()
                    .GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
