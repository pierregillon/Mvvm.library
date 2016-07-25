using System.Windows.Input;

namespace Mvvm.Example
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand DisplayNewUserViewCommand { get; }

        public MainViewModel()
        {
        }
    }
}
