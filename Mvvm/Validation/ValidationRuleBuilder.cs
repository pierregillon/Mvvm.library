using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mvvm.Validation
{
    public class ValidationRuleBuilder
    {
        private readonly Dictionary<string, List<IValidationRule>> _validationRules = new Dictionary<string, List<IValidationRule>>();

        public void Add(Expression<Func<bool>> predicate, string errorMessage)
        {
            var propertyNames = ExtractPropertyNamesFromExpression(predicate);
            var validationRule = new DelegateValidationRule(predicate.Compile(), errorMessage);
            foreach (var propertyName in propertyNames) {
                if (_validationRules.ContainsKey(propertyName) == false) {
                    _validationRules.Add(propertyName, new List<IValidationRule>());
                }
                _validationRules[propertyName].Add(validationRule);
            }
        }

        public Dictionary<string, List<IValidationRule>> Build()
        {
            return _validationRules;
        }

        private static IEnumerable<string> ExtractPropertyNamesFromExpression(Expression expression)
        {
            var propertyNames = new List<string>();

            if (expression is LambdaExpression) {
                propertyNames.AddRange(ExtractPropertyNamesFromExpression(((LambdaExpression) expression).Body));
            }
            else if (expression is MethodCallExpression) {
                var methodCallExpression = (MethodCallExpression) expression;
                foreach (var argumentExpression in methodCallExpression.Arguments) {
                    propertyNames.AddRange(ExtractPropertyNamesFromExpression(argumentExpression));
                }
                if (methodCallExpression.Object != null) {
                    propertyNames.AddRange(ExtractPropertyNamesFromExpression(methodCallExpression.Object));
                }
            }
            else if (expression is MemberExpression) {
                var memberExpression = ((MemberExpression) expression).Member;
                if (memberExpression is PropertyInfo) {
                    propertyNames.Add(memberExpression.Name);
                }
            }
            else if (expression is BinaryExpression) {
                propertyNames.AddRange(ExtractPropertyNamesFromExpression(((BinaryExpression) expression).Left));
                propertyNames.AddRange(ExtractPropertyNamesFromExpression(((BinaryExpression) expression).Right));
            }

            return propertyNames.Distinct();
        }
    }
}