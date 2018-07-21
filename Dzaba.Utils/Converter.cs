using System;
using System.Globalization;

namespace Dzaba.Utils
{
    public static class Converter
    {
        public static T FromString<T>(string str)
            where T : struct
        {
            Require.NotWhiteSpace(str, nameof(str));

            TypeCode code = Type.GetTypeCode(typeof(T));
            if (code == TypeCode.Object)
            {
                throw new InvalidOperationException($"Can't convert custom type {typeof(T).AssemblyQualifiedName}.");
            }

            return (T)Convert.ChangeType(str, code, CultureInfo.InvariantCulture);
        }
    }
}
