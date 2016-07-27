using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mvvm.Routing
{
    public class Bus : IBus
    {
        private readonly Dictionary<Type, List<Delegate>> _callbacks = new Dictionary<Type, List<Delegate>>(); 
        private readonly Dictionary<object, List<Delegate>> _ownedCallbacks = new Dictionary<object, List<Delegate>>();

        public void Send<T>(T message)
        {
            try {
                if (_callbacks.ContainsKey(typeof (T))) {
                    foreach (var callback in _callbacks[typeof(T)].ToArray()) {
                        callback.DynamicInvoke(message);
                    }
                }
            }
            catch (TargetInvocationException ex) {
                throw ex.InnerException;
            }
        }

        public void Register<T>(object subscriber, Action<T> callback)
        {
            if (_callbacks.ContainsKey(typeof (T)) == false) {
                _callbacks[typeof(T)] = new List<Delegate>();
            }
            _callbacks[typeof (T)].Add(callback);

            if (_ownedCallbacks.ContainsKey(subscriber) == false) {
                _ownedCallbacks[subscriber] = new List<Delegate>();
            }
            _ownedCallbacks[subscriber].Add(callback);
        }

        public void Unregister(object subscriber)
        {
            if (_ownedCallbacks.ContainsKey(subscriber)) {
                foreach (var @delegate in _ownedCallbacks[subscriber]) {
                    var messageType = @delegate.GetType().GetGenericArguments()[0];
                    if (_callbacks.ContainsKey(messageType)) {
                        _callbacks[messageType].Remove(@delegate);
                    }
                }
                _ownedCallbacks.Remove(subscriber);
            }
        }
    }
}