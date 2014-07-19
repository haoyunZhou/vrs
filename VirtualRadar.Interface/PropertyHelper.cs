﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace VirtualRadar.Interface
{
    /// <summary>
    /// Utility methods for working with properties.
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// Retrieves the name of a property from the lambda expression passed across. Returns null
        /// if the expression refers to anything other than a simple property.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ExtractName(Expression expression)
        {
            String result;

            var lambdaExpression = (LambdaExpression)expression;
            var parameter = lambdaExpression.Parameters.FirstOrDefault();

            MemberExpression memberExpression = lambdaExpression.Body.NodeType == ExpressionType.Convert ?
                                                ((UnaryExpression)lambdaExpression.Body).Operand as MemberExpression :
                                                lambdaExpression.Body as MemberExpression;
            if(parameter != null) {
                result = memberExpression.ToString().Substring(parameter.Name.Length + 1);
            } else {
                var propertyInfo = memberExpression == null ? null : memberExpression.Member as PropertyInfo;
                result = propertyInfo == null ? null : propertyInfo.Name;
            }

            return result;
        }

        /// <summary>
        /// Retrieves the name of a property from the lambda expression passed across. Returns null
        /// if the expression refers to anything other than a simple property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectorExpression"></param>
        /// <returns></returns>
        public static string ExtractName<T>(Expression<Func<T, object>> selectorExpression)
        {
            return ExtractName((Expression)selectorExpression);
        }

        /// <summary>
        /// Retrieves the name of a property from the lambda expression passed across. Returns null
        /// if the expression refers to anything other than a simple property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unused"></param>
        /// <param name="selectorExpression"></param>
        /// <returns></returns>
        public static string ExtractName<T>(T unused, Expression<Func<T, object>> selectorExpression)
        {
            return ExtractName((Expression)selectorExpression);
        }
    }
}