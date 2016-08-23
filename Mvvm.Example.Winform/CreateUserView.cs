using System.Windows.Forms;
using Mvvm.Example.Winform.Converters;
using Mvvm.Winform.BindingToolkit;

namespace Mvvm.Example.Winform
{
    public partial class CreateUserView : Form
    {
        private readonly CreateUserViewModel _viewModel = Program.Locator.CreateUserViewModel;

        public CreateUserView()
        {
            InitializeComponent();

            firstNameTextBox
                .Bind(x => x.Text)
                .To(_viewModel)
                .On(x => x.FirstName)
                .TwoWay();

            lastNameTextBox
                .Bind(x => x.Text)
                .To(_viewModel)
                .On(x => x.LastName)
                .TwoWay();

            birthDatePicker
                .Bind(x => x.Value)
                .To(_viewModel)
                .On(x => x.BirthDate)
                .TwoWay();

            yearsOldLabel
                .Bind(x => x.Text)
                .To(_viewModel)
                .On(x => x.Age)
                .OneWay();

            nextValidationErrorLabel
                .Bind(x => x.Text)
                .To(_viewModel)
                .On(x => x.NextValidationError)
                .OneWay();

            this
                .Bind(x => x.Enabled)
                .To(_viewModel)
                .On(x => x.IsLoading)
                .OneWay()
                .UseConverter(new InverseBooleanConverter());

            busyProgressBar.BindVisibility(_viewModel, x => x.IsLoading);

            createButton.Command = _viewModel.CreateCommand;
            cancelButton.Command = _viewModel.CancelCommand;
            showAllValidationErrorsButton.Command = _viewModel.ShowAllValidationErrorsCommand;
            hideValidationErrorsButton.Command = _viewModel.HideValidationErrorsCommand;
            errorProvider.DataSource = _viewModel;

            this.BindPopupVisibility(_viewModel, x => x.IsVisible);
        }
    }
}