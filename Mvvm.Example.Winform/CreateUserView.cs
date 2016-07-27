using System.Windows.Forms;
using Mvvm.Example.Winform.Utils;

namespace Mvvm.Example.Winform
{
    public partial class CreateUserView : Form
    {
        private readonly CreateUserViewModel _viewModel = Program.Locator.CreateUserViewModel;

        public CreateUserView()
        {
            InitializeComponent();

            firstNameTextBox.DataBindings.Add(nameof(firstNameTextBox.Text), _viewModel, nameof(_viewModel.FirstName), true, DataSourceUpdateMode.OnPropertyChanged);
            lastNameTextBox.DataBindings.Add(nameof(lastNameTextBox.Text), _viewModel, nameof(_viewModel.LastName), true, DataSourceUpdateMode.OnPropertyChanged);
            birthDatePicker.DataBindings.Add(nameof(birthDatePicker.Value), _viewModel, nameof(_viewModel.BirthDate), true, DataSourceUpdateMode.OnPropertyChanged);
            yearsOldLabel.DataBindings.Add(nameof(yearsOldLabel.Text), _viewModel, nameof(_viewModel.Age), true, DataSourceUpdateMode.OnPropertyChanged);
            nextValidationErrorLabel.DataBindings.Add(nameof(nextValidationErrorLabel.Text), _viewModel, nameof(_viewModel.NextValidationError), true, DataSourceUpdateMode.OnPropertyChanged);

            createButton.Command = _viewModel.CreateCommand;
            cancelButton.Command = _viewModel.CancelCommand;
            errorProvider.DataSource = _viewModel;

            this.BindPopupVisibility(_viewModel, x => x.IsVisible);
        }
    }
}
