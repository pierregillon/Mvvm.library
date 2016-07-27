using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mvvm
{
    public class NotifiableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private readonly object _locker = new object();
        private readonly IDictionary<string, object> _notifiableProperties = new Dictionary<string, object>();
        private readonly DependencyPropertyTracker _tracker;

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public NotifiableObject()
        {
            _tracker = new DependencyPropertyTracker(this);
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            foreach (var dependentPropertyName in _tracker.GetDependentProperties(propertyName)) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(dependentPropertyName));
            }
        }
        protected virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            foreach (var dependentPropertyName in _tracker.GetDependentProperties(propertyName)) {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(dependentPropertyName));
            }
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
                    _notifiableProperties.Add(propertyName, default(T));
                }
                var oldValue = (T) _notifiableProperties[propertyName];
                if (Equals(oldValue, newValue) == false) {
                    RaisePropertyChanging(propertyName);
                    _notifiableProperties[propertyName] = newValue;
                    RaisePropertyChanged(propertyName);
                }
            }
        }
    }
}