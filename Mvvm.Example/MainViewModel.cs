using Mvvm.Commands;
using Mvvm.Routing;

namespace Mvvm.Example
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IBus _bus;

        public ICommand DisplayNewUserViewCommand { get; }

        public MainViewModel(IBus bus)
        {
            _bus = bus;
            DisplayNewUserViewCommand = new RelayCommand(DisplayNewUserView);
        }

        private void DisplayNewUserView()
        {
            _bus.Send(new DisplayNewUserView());
        }
    }
}
