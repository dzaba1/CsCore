using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dzaba.Utils
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitToLines(this string str)
        {
            Require.NotNull(str, nameof(str));

            using (StringReader sr = new StringReader(str))
            {
                return sr.ToEnumerable().ToArray();
            }
        }

        public static IEnumerable<string> SplitToLines(this string str, bool removeEmptyLines)
        {
            Require.NotNull(str, nameof(str));

            using (StringReader sr = new StringReader(str))
            {
                var enumerable = sr.ToEnumerable();
                if (removeEmptyLines)
                {
                    enumerable = enumerable.Where(x => !string.IsNullOrWhiteSpace(x));
                }
                return enumerable.ToArray();
            }
        }

        public static string Swap(this string str, int index1, int index2)
        {
            Require.NotNull(str, nameof(str));
            Require.IndexInRange(index1, str.Length, nameof(index1));
            Require.IndexInRange(index2, str.Length, nameof(index2));

            if (index1 == index2)
            {
                return str;
            }

            var array = str.ToCharArray();
            array.Swap(index1, index2);

            return new string(array);
        }
    }
}
