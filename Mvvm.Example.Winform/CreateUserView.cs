using System.Windows.Forms;

namespace Mvvm.Example.Winform
{
    public partial class CreateUserView : Form
    {
        private readonly CreateUserViewModel _viewModel = new CreateUserViewModel();

        public CreateUserView()
        {
            InitializeComponent();
        }
    }
}
