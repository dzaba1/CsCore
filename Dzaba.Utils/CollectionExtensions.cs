using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dzaba.Utils
{
    public static class CollectionExtensions
    {
        public static void Swap<T>(this T[] array, int i, int j)
        {
            Require.NotNull(array, nameof(array));
            Require.IndexInRange(i, array.Length, nameof(i));
            Require.IndexInRange(j, array.Length, nameof(j));

            if (i == j)
            {
                return;
            }

            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Require.NotNull(collection, nameof(collection));

            if (items != null)
            {
                foreach (var item in items)
                {
                    collection.Add(item);
                }
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            Require.NotNull(collection, nameof(collection));

            HashSet<T> hashSet = collection as HashSet<T>;

            if (hashSet == null)
            {
                hashSet = new HashSet<T>(collection);
            }

            return hashSet;
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection, IEqualityComparer<T> equalityComparer)
        {
            Require.NotNull(collection, nameof(collection));
            Require.NotNull(equalityComparer, nameof(equalityComparer));

            return new HashSet<T>(collection, equalityComparer);
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            Require.NotNull(dict, nameof(dict));

            TValue value;
            if (dict.TryGetValue(key, out value))
            {
                return value;
            }

            return default(TValue);
        }

        public static void AddOrIgnore<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            Require.NotNull(dict, nameof(dict));

            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            Require.NotNull(dict, nameof(dict));

            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
            else
            {
                dict[key] = value;
            }
        }

        public static void RemoveFromEnd(this StringBuilder builder, int count)
        {
            Require.NotNull(builder, nameof(builder));

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            builder.Remove(builder.Length - count, count);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool IsLast<T>(this IEnumerable<T> collection, T element)
        {
            Require.NotNull(collection, nameof(collection));

            var last = collection.Last();
            if (last == null && element == null)
            {
                return true;
            }

            if (last != null)
            {
                return last.Equals(element);
            }

            return element.Equals(last);
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> collection, Func<T, T, bool> comparer, Func<T, int> hashCodeFactory)
        {
            Require.NotNull(collection, nameof(collection));

            var comparerObj = new DelegateEqualityComparer<T>(comparer, hashCodeFactory);

            return collection.Distinct(comparerObj);
        }
    }
}
