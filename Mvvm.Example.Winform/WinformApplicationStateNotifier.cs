using System;
using System.Windows.Forms;
using Mvvm.Commands;

namespace Mvvm.Example.Winform
{
    public class WinformApplicationStateNotifier : INotifyApplicationStateChanged
    {
        public event EventHandler Idle
        {
            add { Application.Idle += value; }
            remove { Application.Idle -= value; }
        }
    }
}
