namespace Mvvm.Validation
{
    public interface IValidationRule
    {
        bool IsValid();
        string ErrorMessage { get; }
    }
}