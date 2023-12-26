using System;
using System.Linq;
using System.Linq.Expressions;

public static class OrderByExtensions
{
    public static IOrderedQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName)
    {
        return ApplyOrder(source, propertyName, "OrderBy");
    }

    public static IOrderedQueryable<T> OrderByDescendingProperty<T>(this IQueryable<T> source, string propertyName)
    {
        return ApplyOrder(source, propertyName, "OrderByDescending");
    }

    private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string propertyName, string methodName)
    {
        var entityType = typeof(T);
        var property = entityType.GetProperty(propertyName);
        if (property == null)
        {
            throw new ArgumentException($"Property {propertyName} not found on type {entityType.Name}");
        }

        var parameter = Expression.Parameter(entityType, "x");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        var resultExp = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { entityType, property.PropertyType },
            source.Expression,
            Expression.Quote(orderByExp));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(resultExp);
    }
}
