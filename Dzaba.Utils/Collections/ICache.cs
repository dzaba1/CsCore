using System;
using System.Collections.Generic;

namespace Dzaba.Utils.Collections
{
    public interface ICache<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
        where TKey : IEquatable<TKey>
    {
        TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);
    }
}
