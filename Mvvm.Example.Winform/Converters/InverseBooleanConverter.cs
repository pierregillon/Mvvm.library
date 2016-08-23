using Mvvm.Winform.BindingToolkit;

namespace Mvvm.Example.Winform.Converters
{
    public class InverseBooleanConverter : IValueConverter<bool, bool>
    {
        public object ConvertViewModelToControl(bool viewModelValue)
        {
            return !viewModelValue;
        }
        public object ConvertControlToViewModel(bool source)
        {
            return !source;
        }
    }
}