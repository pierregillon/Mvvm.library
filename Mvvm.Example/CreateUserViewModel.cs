using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Mvvm.Commands;
using Mvvm.Routing;
using Mvvm.Time;
using Mvvm.Validation;
using ICommand = Mvvm.Commands.ICommand;

namespace Mvvm.Example
{
    public class CreateUserViewModel : ValidationViewModel
    {
        private readonly IClock _clock;
        private readonly IBus _bus;

        public ICommand CancelCommand { get; }
        public IAsyncCommand CreateCommand { get; }

        public bool IsVisible
        {
            get { return GetNotifiableProperty<bool>(); }
            set { SetNotifiableProperty(value); }
        }
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
        public DateTime? BirthDate
        {
            get { return GetNotifiableProperty<DateTime?>(); }
            set { SetNotifiableProperty(value); }
        }
        public int? Age
        {
            get
            {
                if (BirthDate.HasValue) {
                    return DateTime.Now.Subtract(BirthDate.Value).Days/365;
                }
                return null;
            }
        }

        // ----- Constructors
        public CreateUserViewModel(IClock clock, IBus bus)
        {
            _clock = clock;
            _bus = bus;

            CreateCommand = new AsyncRelayCommand(CreateAsync, CanCreate);
            CancelCommand = new RelayCommand(Cancel);

            _bus.Register<DisplayNewUserView>(this, OnDisplayNewUserView);
        }

        // ----- Command implementations
        private bool CanCreate()
        {
            return base.AreAllValidationRulesValid();
        }
        private async Task CreateAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsVisible = false;
        }
        private void Cancel()
        {
            IsVisible = false;
        }

        // ----- Callbacks
        private void OnDisplayNewUserView(DisplayNewUserView obj)
        {
            IsVisible = true;
        }

        // ----- Overrides
        protected override void ConfigureValidationRules(ValidationRuleBuilder builder)
        {
            builder.Add(() => string.IsNullOrWhiteSpace(FirstName), "You should defined a non empty first name.");
            builder.Add(() => string.IsNullOrWhiteSpace(LastName), "You should defined a non empty last name.");
            builder.Add(() => BirthDate == null, "You should defined a birth date.");
            builder.Add(() => BirthDate > _clock.Now(), "You should defined a birth date in the past.");
        }
    }
}