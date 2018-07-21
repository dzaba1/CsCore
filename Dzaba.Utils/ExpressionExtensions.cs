using System;
using System.Linq.Expressions;

namespace Dzaba.Utils
{
    public static class ExpressionExtensions
    {
        public static string GetMemberName<T>(this Expression<Func<T>> property)
        {
            Require.NotNull(property, nameof(property));

            var expression = (MemberExpression)property.Body;
            var member = expression.Member;
            return member.Name;
        }
    }
}
