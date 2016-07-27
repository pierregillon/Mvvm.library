using System;

namespace Mvvm.Routing
{
    public interface IBus
    {
        void Send<T>(T message);
        void Register<T>(object subscriber, Action<T> callbacks);
        void Unregister(object subscriber);
    }
}