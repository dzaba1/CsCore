using System;
using System.Linq.Expressions;

namespace Dzaba.Utils
{
    public static class ReflectionUtils
    {
        public static string GetPropertyName<T>(Expression<Func<T>> property)
        {
            Require.NotNull(property, nameof(property));

            var expression = property.Body as MemberExpression;
            if (expression == null)
                throw new ArgumentException("Invalid property expression.", nameof(property));

            var member = expression.Member;
            return member.Name;
        }
    }
}