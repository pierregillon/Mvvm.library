using System.Windows.Forms;

namespace Mvvm.Example.Winform
{
    public partial class MainWindow : Form
    {
        private readonly CreateUserView _createUserView = new CreateUserView();
        private readonly MainViewModel _viewModel = Program.Locator.MainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            displayCreateNewUserButton.DataBindings.Add(nameof(displayCreateNewUserButton.Command), _viewModel, nameof(_viewModel.DisplayNewUserViewCommand));
        }
    }
}
