namespace Mvvm.Commands
{
    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }
}