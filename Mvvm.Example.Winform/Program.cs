using System;
using System.Windows.Forms;
using Mvvm.Commands;

namespace Mvvm.Example.Winform
{
    static class Program
    {
        public static readonly ViewModelLocator Locator = new ViewModelLocator();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CommandManager.Configure(new WinformApplicationStateNotifier());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
