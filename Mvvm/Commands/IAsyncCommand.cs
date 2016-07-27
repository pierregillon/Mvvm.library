using System.Threading.Tasks;

namespace Mvvm.Commands
{
    public interface IAsyncCommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}