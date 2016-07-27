using System.Windows;
using Mvvm.Commands;

namespace Mvvm.Example.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CommandManager.Configure(new WpfApplicationStateNotifier());
        }
    }
}
