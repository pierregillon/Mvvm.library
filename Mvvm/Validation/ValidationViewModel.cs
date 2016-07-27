using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Mvvm.Validation
{
    public abstract class ValidationViewModel : NotifiableObject, IDataErrorInfo
    {
        private readonly Dictionary<string, List<IValidationRule>> _validationRules;
        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public bool HasValidationErrors
        {
            get { return GetNotifiableProperty<bool>(); }
            private set { SetNotifiableProperty(value); }
        }
        public string NextValidationError
        {
            get { return GetNotifiableProperty<string>(); }
            private set { SetNotifiableProperty(value); }
        }
        public List<string> ValidationErrors
        {
            get { return GetNotifiableProperty<List<string>>(); }
            private set { SetNotifiableProperty(value); }
        }

        // ----- Constructors
        protected ValidationViewModel()
        {
            var builder = new ValidationRuleBuilder();
            // ReSharper disable once VirtualMemberCallInContructor
            ConfigureValidationRules(builder);
            _validationRules = builder.Build();

            ValidationErrors = new List<string>();
        }

        // ----- IDataErrorInfo
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error;
                if (_errors.TryGetValue(propertyName, out error)) {
                    return error;
                }
                return null;
            }
        }
        string IDataErrorInfo.Error => NextValidationError;

        // ----- Validation methods
        public void FeedAllValidationErrors()
        {
            foreach (var propertyName in _validationRules.Keys) {
                EvaluateRules(propertyName);
                base.RaisePropertyChanged(propertyName);
            }
            UpdateValidationProperties();
        }
        public void ClearValidationErrors()
        {
            _errors.Clear();
            foreach (var propertyName in _validationRules.Keys) {
                base.RaisePropertyChanged(propertyName);
            }
            UpdateValidationProperties();
        }
        protected bool AreAllValidationRulesValid()
        {
            return _validationRules.Values.SelectMany(x => x).All(x => x.IsValid());
        }

        // ----- Override / Abstract
        protected override void RaisePropertyChanged(string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (propertyName != nameof(ValidationErrors) && propertyName != nameof(NextValidationError) && propertyName != nameof(HasValidationErrors)) {
                EvaluateRules(propertyName);
            }

            base.RaisePropertyChanged(propertyName);

            if (propertyName != nameof(ValidationErrors) && propertyName != nameof(NextValidationError) && propertyName != nameof(HasValidationErrors)) {
                UpdateValidationProperties();
            }
        }
        protected abstract void ConfigureValidationRules(ValidationRuleBuilder builder);

        // ----- Internal logics
        private void EvaluateRules(string propertyName)
        {
            List<IValidationRule> rules;
            if (_validationRules.TryGetValue(propertyName, out rules)) {
                if (_errors.ContainsKey(propertyName) == false) {
                    _errors.Add(propertyName, null);
                }
                _errors[propertyName] = rules.FirstOrDefault(validationRule => !validationRule.IsValid())?.ErrorMessage;
            }
        }
        private void EvaluateAllRules()
        {
            foreach (var propertyName in _validationRules.Keys) {
                EvaluateRules(propertyName);
            }
        }
        private void UpdateValidationProperties()
        {
            if (_errors.Values.SequenceEqual(ValidationErrors) == false) {
                ValidationErrors = _errors.Values.ToList();
                NextValidationError = ValidationErrors.FirstOrDefault(error => string.IsNullOrEmpty(error) == false);
                HasValidationErrors = _errors.Values.Any(error => string.IsNullOrEmpty(error) == false);
            }
        }
    }
}