using System.Windows;

namespace Mvvm.Example.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CreateUserView _view = new CreateUserView();

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
