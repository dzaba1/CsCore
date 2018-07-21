using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dzaba.Utils
{
    public static class ReflectionExtensions
    {
        public static bool IsNullable(this Type type)
        {
            Require.NotNull(type, nameof(type));

            if (type.IsValueType)
            {
                return Nullable.GetUnderlyingType(type) != null;
            }

            return false;
        }

        public static bool HasCustomAttribute<T>(this MemberInfo member) where T : Attribute
        {
            Require.NotNull(member, nameof(member));

            return member.GetCustomAttribute<T>() != null;
        }

        public static bool ImplementsInterface<T>(this Type type)
        {
            Require.NotNull(type, nameof(type));

            return type.GetInterfaces().Any(t => t == typeof(T));
        }

        public static bool HasParameterlessConstructor(this Type type)
        {
            Require.NotNull(type, nameof(type));

            return type.GetConstructor(Type.EmptyTypes) != null;
        }

        public static Stream GetResourceStream(this Assembly assembly, string resourceName)
        {
            Require.NotNull(assembly, nameof(assembly));
            Require.NotWhiteSpace(resourceName, nameof(resourceName));

            var assemblyName = assembly.GetName().Name;
            var resourceNameFull = assemblyName + '.' + resourceName
                .Replace(Path.AltDirectorySeparatorChar, '.')
                .Replace(Path.DirectorySeparatorChar, '.');

            var stream = assembly.GetManifestResourceStream(resourceNameFull);
            if (stream == null)
            {
                throw new FileNotFoundException($"Couldn't find {resourceName} file in the assembly {assemblyName}", resourceNameFull);
            }

            return stream;
        }

        public static string GetResourceText(this Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
