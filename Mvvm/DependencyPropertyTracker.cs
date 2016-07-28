using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Reflection;

namespace Mvvm
{
    public class DependencyPropertyTracker
    {
        private readonly IDictionary<string, List<string>> _dependentProperties = new Dictionary<string, List<string>>();

        public DependencyPropertyTracker(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            var allProperties = target.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToArray();

            var dependencyInformations = allProperties
                .Where(x => x.GetSetMethod(true) != null || x.DeclaringType?.GetProperty(x.Name).GetSetMethod(true) != null)
                .Select(x => new DependencyInformation {Name = x.Name, GetMethod = x.GetMethod})
                .ToList();

            var calculatedProperties = allProperties
                .Where(x => x.GetSetMethod(true) == null && x.DeclaringType?.GetProperty(x.Name).GetSetMethod(true) == null)
                .ToArray();

            foreach (var propertyInfo in calculatedProperties) {
                foreach (var instruction in propertyInfo.GetMethod.GetInstructions()) {
                    var callToMethod = instruction.Operand as MethodInfo;
                    if (callToMethod != null) {
                        foreach (var dependencyInformation in dependencyInformations) {
                            if (dependencyInformation.GetMethod.ToString() == callToMethod.ToString()) {
                                AddDependentProperty(dependencyInformation, propertyInfo);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public IReadOnlyCollection<string> GetDependentProperties(string propertyName)
        {
            List<string> dependentPropertyNames;
            if (_dependentProperties.TryGetValue(propertyName, out dependentPropertyNames)) {
                return dependentPropertyNames;
            }
            return new List<string>();
        }

        private void AddDependentProperty(DependencyInformation keyValuePair, PropertyInfo propertyInfo)
        {
            if (_dependentProperties.ContainsKey(keyValuePair.Name) == false) {
                _dependentProperties.Add(keyValuePair.Name, new List<string>());
            }
            _dependentProperties[keyValuePair.Name].Add(propertyInfo.Name);
        }

        private class DependencyInformation
        {
            public string Name { get; set; }
            public MethodInfo GetMethod { get; set; }
        }
    }
}