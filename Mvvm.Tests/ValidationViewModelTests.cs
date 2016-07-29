using System.Collections.Generic;
using System.ComponentModel;
using Mvvm.Validation;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class ValidationViewModel_should
    {
        private readonly PersonViewModel _personViewModel;
        private readonly List<string> _propertyNames = new List<string>();
        private readonly List<string> _errorPropertyNames = new List<string>();

        public ValidationViewModel_should()
        {
            _personViewModel = new PersonViewModel();
            _personViewModel.PropertyChanged += (sender, args) => { _propertyNames.Add(args.PropertyName); };
            _personViewModel.ErrorsChanged += (sender, args) => { _errorPropertyNames.Add(args.PropertyName); };
        }

        [Fact]
        public void have_no_validation_errors_by_default()
        {
            Check.That(_personViewModel.NextValidationError).IsNull();
            Check.That(_personViewModel.ValidationErrors).IsEmpty();
        }

        [Fact]
        public void display_next_validation_error_when_setting_invalid_value()
        {
            _personViewModel.FirstName = " ";

            Check.That(_personViewModel.NextValidationError).IsEqualTo("First name should be defined.");
        }

        [Fact]
        public void notify_property_changed_for_validation_properties_when_invalid_value()
        {
            _personViewModel.FirstName = "  ";

            Check.That(_propertyNames).ContainsExactly(
                nameof(_personViewModel.FirstName),
                nameof(_personViewModel.ValidationErrors),
                nameof(_personViewModel.NextValidationError),
                nameof(_personViewModel.HasValidationErrors));
        }

        [Fact]
        public void display_validation_errors_when_setting_invalid_value()
        {
            _personViewModel.FirstName = " ";

            Check.That(_personViewModel.ValidationErrors).ContainsExactly("First name should be defined.");
        }

        [Fact]
        public void notify_error_changed_when_setting_invalid_value()
        {
            _personViewModel.FirstName = "  ";

            Check.That(_errorPropertyNames).ContainsExactly(nameof(_personViewModel.FirstName));
        }

        [Fact]
        public void not_notify_error_changed_when_setting_invalid_value_with_same_validation_rule()
        {
            _personViewModel.FirstName = "  ";
            _errorPropertyNames.Clear();
            _personViewModel.FirstName = "    ";

            Check.That(_errorPropertyNames).IsEmpty();
        }

        [Fact]
        public void not_notify_property_changed_for_validation_properties_when_value_always_invalid()
        {
            _personViewModel.FirstName = "  ";
            _propertyNames.Clear();
            _personViewModel.FirstName = "   ";

            Check.That(_propertyNames).ContainsExactly(nameof(_personViewModel.FirstName));
        }

        [Fact]
        public void display_first_validation_error_when_setting_multiple_invalid_values()
        {
            _personViewModel.LastName = " ";
            _personViewModel.FirstName = " ";

            Check.That(_personViewModel.NextValidationError).IsEqualTo("Last name should be defined.");
        }

        [Fact]
        public void display_next_validation_error_when_feeding_all()
        {
            _personViewModel.FeedAllValidationErrors();

            Check.That(_personViewModel.NextValidationError).IsEqualTo("First name should be defined.");
        }

        [Fact]
        public void display_all_validation_errors_when_feeding_all()
        {
            _personViewModel.FeedAllValidationErrors();

            Check.That(_personViewModel.ValidationErrors).ContainsExactly("First name should be defined.", "Last name should be defined.");
        }

        [Fact]
        public void notify_property_changed_for_all_validation_properties_when_feeding_all()
        {
            _personViewModel.FeedAllValidationErrors();

            Check.That(_propertyNames).ContainsExactly(
                nameof(_personViewModel.ValidationErrors),
                nameof(_personViewModel.NextValidationError),
                nameof(_personViewModel.HasValidationErrors));
        }

        [Fact]
        public void notify_property_changed_for_all_validation_properties_when_clearing_all()
        {
            _personViewModel.FeedAllValidationErrors();
            _propertyNames.Clear();
            _personViewModel.ClearValidationErrors();

            Check.That(_propertyNames).ContainsExactly(
                nameof(_personViewModel.ValidationErrors),
                nameof(_personViewModel.NextValidationError),
                nameof(_personViewModel.HasValidationErrors));
        }

        [Fact]
        public void notify_error_changed_for_properties_with_validation_rules_when_feeding_all()
        {
            _personViewModel.FeedAllValidationErrors();

            Check.That(_errorPropertyNames).ContainsExactly(
                nameof(_personViewModel.FirstName),
                nameof(_personViewModel.LastName));
        }

        [Fact]
        public void notify_error_changed_for_properties_with_validation_rules_when_clearing_all()
        {
            _personViewModel.FeedAllValidationErrors();
            _errorPropertyNames.Clear();
            _personViewModel.ClearValidationErrors();

            Check.That(_errorPropertyNames).ContainsExactly(
                nameof(_personViewModel.FirstName),
                nameof(_personViewModel.LastName));
        }

        // ----- Internal logics

        private class PersonViewModel : ValidationViewModel
        {
            public string FirstName
            {
                get { return GetNotifiableProperty<string>(); }
                set { SetNotifiableProperty(value); }
            }
            public string LastName
            {
                get { return GetNotifiableProperty<string>(); }
                set { SetNotifiableProperty(value); }
            }

            protected override void ConfigureValidationRules(ValidationRuleBuilder builder)
            {
                builder.Add(() => string.IsNullOrWhiteSpace(FirstName), "First name should be defined.");
                builder.Add(() => string.IsNullOrWhiteSpace(LastName), "Last name should be defined.");
            }
        }
    }
}