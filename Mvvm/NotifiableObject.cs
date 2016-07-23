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

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

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
                return (T)value;
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
                }
            }
        }
    }
}