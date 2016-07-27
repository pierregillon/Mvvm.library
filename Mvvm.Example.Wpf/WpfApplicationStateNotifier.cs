using System;
using System.Windows.Interop;
using Mvvm.Commands;

namespace Mvvm.Example.Wpf
{
    public class WpfApplicationStateNotifier : INotifyApplicationStateChanged
    {
        public event EventHandler Idle
        {
            add { ComponentDispatcher.ThreadIdle += value; }
            remove { ComponentDispatcher.ThreadIdle -= value; }
        }
    }
}