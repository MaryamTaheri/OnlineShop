using System.Linq.Expressions;
using System.Reflection;
using ONLINE_SHOP.Domain.Framework.Contracts.Request.DynamicSearchFilter;
using ONLINE_SHOP.Domain.Framework.Exceptions;

namespace ONLINE_SHOP.Domain.Framework.Extensions.DynamicSearchFilter;

public static class FilterExtensions
{
    public static Expression<Func<TEntity, bool>> GetExpression<TEntity, TQuery>(TQuery request)
        => Predicate<TEntity, TQuery>(request);

    public static Expression<Func<TEntity, bool>> Predicate<TEntity, TQuery>(TQuery request)
    {
        var filteringModel = request as BaseSearchFilterPayload;
        if (filteringModel?.Restrictions is null)
            return null;

        Expression<Func<TEntity, bool>> mainExpression = null;
        Expression<Func<TEntity, bool>> finalExpression = null;

        List<PropertyInfo> entityProperties = typeof(TEntity)
            .GetPublicPropertiesFromCache()?
            .ToList();

        List<KeyValuePair<string, string[]>> subPropertyNames = filteringModel
            .Restrictions
            .Where(e => e.Field.Contains('.'))
            .Select(e => new KeyValuePair<string, string[]>(e.Field, e.Field.Split('.')))
            .ToList();

        foreach (var item in filteringModel.Restrictions)
        {
            Expression<Func<TEntity, bool>> expression = null;
            var properties = ReflectionExtensions.FindDomainProperties(entityProperties, subPropertyNames, item.Field);

            if (properties is null || properties.Count == 0)
                continue;

            switch (item.Type)
            {
                case "collection":
                    expression = item.Values.HasItem()
                        ? CreateCondition<TEntity>(item, properties.ToArray())
                        : r => true;
                    break;
                case "range":
                    expression = CreateCondition<TEntity>(item, properties.ToArray());
                    break;
                case "simple":
                    expression = CreateCondition<TEntity>(item, properties.ToArray());
                    break;
                default:
                    throw new Dexception(Situation.Make(SitKeys.BadRequest),
                        new List<KeyValuePair<string, string>>
                        {
                            new(":پیام:", "نوع فیلد «:نوع:» در فیلترهای ارسال شده نامعتبر است."
                                .Replace(":نوع:", item.Type))
                        });
            }

            mainExpression = mainExpression is null ? expression : mainExpression.AndAlso(expression);
        }

        if (mainExpression is not null)
            finalExpression = mainExpression;

        return finalExpression;
    }

    public static Expression<Func<TEntity, bool>> CreateCondition<TEntity>(RestrictionDto value, params PropertyInfo[] properties)
    {
        Expression<Func<TEntity, bool>> expression = null;

        if (string.Equals(value.Type, "range", StringComparison.OrdinalIgnoreCase))
        {
            if (value.MinValue.HasValue() && value.MaxValue.HasValue())
            {
                Expression<Func<TEntity, bool>> from = ExpressionExtensions.AddPredicate<TEntity>(
                    "greaterthanorequal",
                    ReflectionExtensions.ChangeType(value.MinValue, properties[properties.Length - 1].PropertyType),
                    properties.ToArray());

                Expression<Func<TEntity, bool>> to = ExpressionExtensions.AddPredicate<TEntity>(
                    "lessthanorequal",
                    ReflectionExtensions.ChangeType(value.MaxValue, properties[properties.Length - 1].PropertyType),
                    properties.ToArray());

                expression = from.AndAlso(to);
            }

            else
                expression = e => true;
        }
        else if (string.Equals(value.Type, "collection", StringComparison.OrdinalIgnoreCase))
        {
            if (value.Values?.Count is not > 0)
                throw new Dexception(Situation.Make(SitKeys.BadRequest),
                    new List<KeyValuePair<string, string>>
                    {
                        new(":پیام:", "نوع فیلد «:نوع:» باید دارای گزینه‌های قابل انتخاب باشد."
                            .Replace(":نوع:", value.Type))
                    });

            foreach (var item in value.Values)
            {
                Expression<Func<TEntity, bool>> condition = ExpressionExtensions.AddPredicate<TEntity>(
                    "equals",
                    ReflectionExtensions.ChangeType(item, properties[properties.Length - 1].PropertyType),
                    properties.ToArray());

                expression = expression is null ? condition : expression.OrElse(condition);
            }
        }
        else if (string.Equals(value.Type, "simple", StringComparison.OrdinalIgnoreCase) && value.Operation.HasValue())
        {
            switch (value.Operation)
            {
                case "eq":
                    Expression<Func<TEntity, bool>> eqExpression = ExpressionExtensions.AddPredicate<TEntity>(
                        "equals",
                        ReflectionExtensions.ChangeType(value.Value, properties[properties.Length - 1].PropertyType),
                        properties.ToArray());

                    expression = eqExpression;
                    break;
                case "gt":
                    Expression<Func<TEntity, bool>> gtExpression = ExpressionExtensions.AddPredicate<TEntity>(
                        "greaterthan",
                        ReflectionExtensions.ChangeType(value.Value, properties[properties.Length - 1].PropertyType),
                        properties.ToArray());

                    expression = gtExpression;
                    break;
                case "ge":
                    Expression<Func<TEntity, bool>> geExpression = ExpressionExtensions.AddPredicate<TEntity>(
                        "greaterthanorequal",
                        ReflectionExtensions.ChangeType(value.Value, properties[properties.Length - 1].PropertyType),
                        properties.ToArray());

                    expression = geExpression;
                    break;
                case "lt":
                    Expression<Func<TEntity, bool>> ltExpression = ExpressionExtensions.AddPredicate<TEntity>(
                        "lessthan",
                        ReflectionExtensions.ChangeType(value.Value, properties[properties.Length - 1].PropertyType),
                        properties.ToArray());

                    expression = ltExpression;
                    break;
                case "le":
                    Expression<Func<TEntity, bool>> leExpression = ExpressionExtensions.AddPredicate<TEntity>(
                        "lessthanorequal",
                        ReflectionExtensions.ChangeType(value.Value, properties[properties.Length - 1].PropertyType),
                        properties.ToArray());

                    expression = leExpression;
                    break;
                default:
                    throw new Dexception(Situation.Make(SitKeys.BadRequest),
                        new List<KeyValuePair<string, string>>
                        {
                            new(":پیام:", "عملگر «:عملگر:» در فیلترهای ارسال شده نامعتبر است."
                                .Replace(":عملگر:", value.Operation))
                        });
            }
        }

        return expression;
    }
}