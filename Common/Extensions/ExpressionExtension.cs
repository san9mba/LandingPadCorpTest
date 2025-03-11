using System;
using System.Linq.Expressions;

namespace Common.Extensions
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var parameter = left.Parameters[0];
            var body = Expression.AndAlso(left.Body, Expression.Invoke(right, left.Parameters));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
