using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.Models.APIModels;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> ApplyFilterAndSort<T>(this IQueryable<T> query, List<QueryField>? queryFields)
        {
            if (queryFields == null || !queryFields.Any())
                return query;

            var properties = typeof(T).GetPropertiesAsDictionary();

            if (properties == null || !properties.Any())
                return query;

            // Step 1: Apply Filters
            #region Apply Filters
            foreach (var queryField in queryFields)
            {
                if (!properties.TryGetValue(queryField.FieldName, out var propertyInfo))
                    continue;

                var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T), "x");
                var member = System.Linq.Expressions.Expression.Property(parameter, propertyInfo);
                var constant = System.Linq.Expressions.Expression.Constant(Convert.ChangeType(queryField.QueryString, propertyInfo.PropertyType));
                var body = System.Linq.Expressions.Expression.Equal(member, constant);
                var lambda = System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(body, parameter);
                query = query.Where(lambda);
            }
            #endregion Apply Filters

            return query;
        }

        #region Private Helper Methods
        
        private static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, List<QueryField> queryFields, Dictionary<string, PropertyInfo> properties)
        {
            Expression? queryExpression = null;

            foreach (var queryField in queryFields)
            {
                if (!properties.TryGetValue(queryField.FieldName, out var propertyInfo))
                    continue;

                // TODO: Remove this test code and implement full filtering logic
                var test = propertyInfo.PropertyType;
               
                switch(propertyInfo.PropertyType)
                {
                    case Type t when t == typeof(string):
                        {
                            var fieldExpression = BuildComparisonExpressionForString(queryField);
                            queryExpression.BuildLogicalExpression(fieldExpression, queryField.LogicalOperator);
                        }
                        break;
                }
            }

            return query;
        }

        private static Expression BuildLogicalExpression(this Expression? expression, Expression fieldExpression, LogicalOperator? logicalOperator)
        {
            bool isFirstExpression = expression == null && logicalOperator == null;
            if(isFirstExpression)
                return fieldExpression;

            switch (logicalOperator)
            {
                case LogicalOperator.And:
                    return Expression.AndAlso(expression!, fieldExpression);
                case LogicalOperator.Or:
                    return Expression.OrElse(expression!, fieldExpression);
                default:
                    return expression!;
            }
        }

        private static Expression? BuildComparisonExpressionForString(QueryField queryField)
        {
            Expression? expression = null!;

            return expression;
        }

        private static Expression? BuildComparisonExpressionForNumberic(QueryField queryField)
        {
            Expression? expression = null!;
            return expression;
        }
        #endregion Private Helper Methods
    }
}
