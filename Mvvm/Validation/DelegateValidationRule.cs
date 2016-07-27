using System;

namespace Mvvm.Validation
{
    public class DelegateValidationRule : IValidationRule
    {
        private readonly Func<bool> _predicate;

        public string ErrorMessage { get; set; }

        public DelegateValidationRule(Func<bool> predicate, string errorMessage)
        {
            _predicate = predicate;
            ErrorMessage = errorMessage;
        }

        public bool IsValid()
        {
            return !_predicate();
        }
    }
}