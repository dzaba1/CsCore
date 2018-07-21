using System;
using System.Collections.Generic;

namespace Dzaba.Utils.Collections
{
    internal class TtlCacheEntry<TKey, TValue> : ICacheEntry<TValue>
    {
        private readonly Func<TKey, TValue> valueFactory;
        private TValue value;
        private readonly IDateTimeProvider dateTimeProvider;

        public TimeSpan TimeToLive { get; }
        public TKey Key { get; }
        public DateTime Created { get; private set; }

        public TtlCacheEntry(TimeSpan timeToLive,
            TKey key,
            Func<TKey, TValue> valueFactory,
            IDateTimeProvider dateTimeProvider)
        {
            Require.NotNull(valueFactory, nameof(valueFactory));
            Require.NotNull(dateTimeProvider, nameof(dateTimeProvider));

            TimeToLive = timeToLive;
            Key = key;
            this.valueFactory = valueFactory;
            this.dateTimeProvider = dateTimeProvider;

            SetValue();
        }

        private void SetValue()
        {
            value = valueFactory(Key);
            Created = dateTimeProvider.GetCurrentUtc();
        }

        public TValue GetValue()
        {
            var current = dateTimeProvider.GetCurrentUtc();
            if (current - Created > TimeToLive)
            {
                SetValue();
            }
            return value;
        }
    }

    public class TtlConcurrentCache<TKey, TValue> : ConcurrentCacheBase<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public TimeSpan TimeToLive { get; }

        public TtlConcurrentCache(TimeSpan timeToLive,
            IDateTimeProvider dateTimeProvider)
        {
            Require.NotNull(dateTimeProvider, nameof(dateTimeProvider));

            TimeToLive = timeToLive;
            this.dateTimeProvider = dateTimeProvider;
        }

        public TtlConcurrentCache(IEqualityComparer<TKey> comparer,
            TimeSpan timeToLive,
            IDateTimeProvider dateTimeProvider)
            : base(comparer)
        {
            Require.NotNull(dateTimeProvider, nameof(dateTimeProvider));

            TimeToLive = timeToLive;
            this.dateTimeProvider = dateTimeProvider;
        }

        protected override ICacheEntry<TValue> CreateNewEntry(TKey key, Func<TKey, TValue> valueFactory)
        {
            Require.NotNull(valueFactory, nameof(valueFactory));

            return new TtlCacheEntry<TKey, TValue>(TimeToLive, key, valueFactory, dateTimeProvider);
        }
    }
}
