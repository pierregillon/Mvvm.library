using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Reflection;

namespace Mvvm
{
    public class NotifiableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private readonly object _locker = new object();
        private readonly IDictionary<string, object> _notifiableProperties = new Dictionary<string, object>();
        private readonly IDictionary<string, List<string>> _dependentProperties = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public NotifiableObject()
        {
            var allProperties = GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .ToArray();

            var properties = allProperties
                .Where(x => x.SetMethod != null)
                .Select(x => new MyClass { Name=x.Name, GetMethod=x.GetMethod})
                .ToDictionary(x => x.GetMethod);

            var calculatedProperties = allProperties.Where(x => x.SetMethod == null).ToArray();


            foreach (var propertyInfo in calculatedProperties) {
                foreach (var instruction in propertyInfo.GetMethod.GetInstructions()) {
                    if (instruction.Operand is MethodInfo) {
                        MyClass dependentPropertyName = null;
                        if (properties.TryGetValue((MethodInfo) instruction.Operand, out dependentPropertyName)) {
                            if (_dependentProperties.ContainsKey(propertyInfo.Name) == false) {
                                _dependentProperties.Add(propertyInfo.Name, new List<string>());
                            }
                            _dependentProperties[propertyInfo.Name].Add(dependentPropertyName.Name);
                        }
                    }
                }
            }
        }

        private class MyClass
        {
            public string Name { get; set; }
            public MethodInfo GetMethod { get; set; }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected T GetNotifiableProperty<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            object value;
            if (_notifiableProperties.TryGetValue(propertyName, out value)) {
                return (T) value;
            }
            return default(T);
        }
        protected void SetNotifiableProperty<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            lock (_locker) {
                if (_notifiableProperties.ContainsKey(propertyName) == false) {
                    _notifiableProperties.Add(propertyName, null);
                }
                var oldValue = (T) _notifiableProperties[propertyName];
                if (Equals(oldValue, newValue) == false) {
                    RaisePropertyChanging(propertyName);
                    _notifiableProperties[propertyName] = newValue;
                    RaisePropertyChanged(propertyName);

                    foreach (var dependentProperty in _dependentProperties) {
                        if (dependentProperty.Value.Contains(propertyName)) {
                            RaisePropertyChanged(dependentProperty.Key);
                        }
                    }
                }
            }
        }
    }
}