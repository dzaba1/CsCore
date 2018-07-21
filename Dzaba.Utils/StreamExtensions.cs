using System.Collections.Generic;
using System.IO;

namespace Dzaba.Utils
{
    public static class StreamExtensions
    {
        public static IEnumerable<string> ToEnumerable(this TextReader reader)
        {
            Require.NotNull(reader, nameof(reader));

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
