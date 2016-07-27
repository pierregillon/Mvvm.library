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
        private readonly List<PropertyChangedEventArgs> _propertyChangedArgs = new List<PropertyChangedEventArgs>();

        public ValidationViewModel_should()
        {
            _personViewModel = new PersonViewModel();
            _personViewModel.PropertyChanged += (sender, args) => {
                _propertyChangedArgs.Add(args);
            };
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
        public void raise_property_changed_for_validation_properties_when_invalid_value()
        {
            _personViewModel.FirstName = "  ";

            Check.That(_propertyChangedArgs).HasSize(4);
            Check.That(_propertyChangedArgs[0].PropertyName).IsEqualTo(nameof(_personViewModel.FirstName));
            Check.That(_propertyChangedArgs[1].PropertyName).IsEqualTo(nameof(_personViewModel.ValidationErrors));
            Check.That(_propertyChangedArgs[2].PropertyName).IsEqualTo(nameof(_personViewModel.NextValidationError));
            Check.That(_propertyChangedArgs[3].PropertyName).IsEqualTo(nameof(_personViewModel.HasValidationErrors));
        }

        [Fact]
        public void not_raise_property_changed_for_validation_properties_when_value_always_invalid()
        {
            _personViewModel.FirstName = "  ";
            _propertyChangedArgs.Clear();
            _personViewModel.FirstName = "   ";

            Check.That(_propertyChangedArgs).HasSize(1);
            Check.That(_propertyChangedArgs[0].PropertyName).IsEqualTo(nameof(_personViewModel.FirstName));
        }

        [Fact]
        public void display_first_validation_error_when_setting_multiple_invalid_values()
        {
            _personViewModel.LastName = " ";
            _personViewModel.FirstName = " ";

            Check.That(_personViewModel.NextValidationError).IsEqualTo("Last name should be defined.");
        }

        [Fact]
        public void display_validation_errors_when_setting_invalid_value()
        {
            _personViewModel.FirstName = " ";

            Check.That(_personViewModel.ValidationErrors).ContainsExactly("First name should be defined.");
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
        public void raise_property_changed_for_all_validation_properties_when_feeding_all()
        {
            _personViewModel.FeedAllValidationErrors();

            Check.That(_propertyChangedArgs).HasSize(5);
            Check.That(_propertyChangedArgs[0].PropertyName).IsEqualTo(nameof(_personViewModel.FirstName));
            Check.That(_propertyChangedArgs[1].PropertyName).IsEqualTo(nameof(_personViewModel.LastName));
            Check.That(_propertyChangedArgs[2].PropertyName).IsEqualTo(nameof(_personViewModel.ValidationErrors));
            Check.That(_propertyChangedArgs[3].PropertyName).IsEqualTo(nameof(_personViewModel.NextValidationError));
            Check.That(_propertyChangedArgs[4].PropertyName).IsEqualTo(nameof(_personViewModel.HasValidationErrors));
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