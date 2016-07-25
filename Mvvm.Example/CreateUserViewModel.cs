using System;
using System.Windows.Input;

namespace Mvvm.Example
{
    public class CreateUserViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }
        public ICommand CreateCommand { get; }

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
    }
}