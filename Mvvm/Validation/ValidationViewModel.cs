using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Mvvm.Validation
{
    public abstract class ValidationViewModel : NotifiableObject, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<IValidationRule>> _validationRules;
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public List<string> ValidationErrors
        {
            get { return GetNotifiableProperty<List<string>>(); }
            private set { SetNotifiableProperty(value); }
        }
        public string NextValidationError => ValidationErrors.FirstOrDefault(error => string.IsNullOrEmpty(error) == false);
        public bool HasValidationErrors => ValidationErrors.Any();

        // ----- Constructors
        protected ValidationViewModel()
        {
            var builder = new ValidationRuleBuilder();
            // ReSharper disable once VirtualMemberCallInContructor
            ConfigureValidationRules(builder);
            _validationRules = builder.Build();

            ValidationErrors = new List<string>();
        }

        // ----- INotifyDataErrorInfo
        bool INotifyDataErrorInfo.HasErrors => HasValidationErrors;
        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            List<string> validationErrors;
            if (_errors.TryGetValue(propertyName, out validationErrors)) {
                return validationErrors;
            }
            return Enumerable.Empty<string>();
        }

        // ----- Validation methods
        public void FeedAllValidationErrors()
        {
            foreach (var propertyName in _validationRules.Keys) {
                EvaluateRules(propertyName);
                base.RaisePropertyChanged(propertyName);
            }
            RaiseValidationPropertiesChanged();
        }
        public void ClearValidationErrors()
        {
            _errors.Clear();
            foreach (var propertyName in _validationRules.Keys) {
                base.RaisePropertyChanged(propertyName);
            }
            RaiseValidationPropertiesChanged();
        }
        protected bool AreAllValidationRulesValid()
        {
            return _validationRules.Values.SelectMany(x => x).All(x => x.IsValid());
        }
        protected virtual void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        // ----- Override / Abstract
        protected override void RaisePropertyChanged(string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (propertyName != nameof(ValidationErrors) && propertyName != nameof(NextValidationError) &&
                propertyName != nameof(HasValidationErrors)) {
                EvaluateRules(propertyName);
            }

            base.RaisePropertyChanged(propertyName);

            if (propertyName != nameof(ValidationErrors) && propertyName != nameof(NextValidationError) &&
                propertyName != nameof(HasValidationErrors)) {
                RaiseValidationPropertiesChanged();
            }
        }
        protected abstract void ConfigureValidationRules(ValidationRuleBuilder builder);

        // ----- Internal logics
        private void EvaluateRules(string propertyName)
        {
            List<IValidationRule> rules;
            if (_validationRules.TryGetValue(propertyName, out rules)) {
                if (_errors.ContainsKey(propertyName) == false) {
                    _errors.Add(propertyName, new List<string>());
                }

                var currentErrors = _errors[propertyName];
                var newErrors = rules
                    .Where(validationRule => !validationRule.IsValid())
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                if (currentErrors.SequenceEqual(newErrors) == false) {
                    currentErrors.Clear();
                    currentErrors.AddRange(newErrors);
                    RaiseErrorsChanged(propertyName);
                }
            }
        }
        private void RaiseValidationPropertiesChanged()
        {
            if (_errors.Values.SelectMany(x => x).SequenceEqual(ValidationErrors) == false) {
                ValidationErrors = _errors.Values.SelectMany(x => x).ToList();
            }
        }
    }
}