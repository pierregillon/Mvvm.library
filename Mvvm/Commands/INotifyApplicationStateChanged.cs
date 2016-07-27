using System;

namespace Mvvm.Commands
{
    public interface INotifyApplicationStateChanged
    {
        event EventHandler Idle;
    }
}