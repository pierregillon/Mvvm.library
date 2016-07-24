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
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .ToArray();

            var properties = allProperties
                .Where(x => x.SetMethod != null)
                .Select(x => new DependencyInformation {Name = x.Name, GetMethod = x.GetMethod})
                .ToDictionary(x => x.GetMethod);

            var calculatedProperties = allProperties.Where(x => x.SetMethod == null).ToArray();

            foreach (var propertyInfo in calculatedProperties) {
                foreach (var instruction in propertyInfo.GetMethod.GetInstructions()) {
                    var callToMethod = instruction.Operand as MethodInfo;
                    if (callToMethod != null) {
                        DependencyInformation dependencyInformation;
                        if (properties.TryGetValue(callToMethod, out dependencyInformation)) {
                            if (_dependentProperties.ContainsKey(dependencyInformation.Name) == false) {
                                _dependentProperties.Add(dependencyInformation.Name, new List<string>());
                            }
                            _dependentProperties[dependencyInformation.Name].Add(propertyInfo.Name);
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

        private class DependencyInformation
        {
            public string Name { get; set; }
            public MethodInfo GetMethod { get; set; }
        }
    }
}