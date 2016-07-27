using Mvvm.Routing;
using Mvvm.Time;

namespace Mvvm.Example
{
    public class ViewModelLocator
    {
        private static readonly Bus Bus = new Bus();

        public CreateUserViewModel CreateUserViewModel { get; } = new CreateUserViewModel(new Clock(), Bus);
        public MainViewModel MainViewModel { get; } = new MainViewModel(Bus);
    }
}
