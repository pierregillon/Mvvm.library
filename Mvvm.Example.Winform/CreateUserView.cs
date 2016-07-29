using System.Windows.Forms;
using Mvvm.Winform.BindingToolkit;

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
            busyProgressBar.BindVisibility(_viewModel, x => x.IsLoading);

            var binding = DataBindings.Add(nameof(Enabled), _viewModel, nameof(_viewModel.IsLoading), true, DataSourceUpdateMode.OnPropertyChanged);
            binding.Parse += ReverseBoolean;
            binding.Format += ReverseBoolean;

            createButton.Command = _viewModel.CreateCommand;
            cancelButton.Command = _viewModel.CancelCommand;
            showAllValidationErrorsButton.Command = _viewModel.ShowAllValidationErrorsCommand;
            hideValidationErrorsButton.Command = _viewModel.HideValidationErrorsCommand;
            errorProvider.DataSource = _viewModel;

            this.BindPopupVisibility(_viewModel, x => x.IsVisible);
        }

        private static void ReverseBoolean(object sender, ConvertEventArgs args)
        {
            args.Value = (bool)args.Value == false;
        }
    }
}
