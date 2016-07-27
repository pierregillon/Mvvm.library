using System.Threading.Tasks;
using Mvvm.Commands;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class AsyncRelayCommand_should
    {
        [Fact]
        public async Task call_delegate_on_execute()
        {
            var result = 0;
            var command = new AsyncRelayCommand(() => { return Task.Run(() => result++); });

            await command.ExecuteAsync();

            Check.That(result).IsEqualTo(1);
        }

        [Fact]
        public async Task not_call_delegate_on_execute_when_cannot_execute()
        {
            var result = 0;
            var command = new AsyncRelayCommand(() => { return Task.Run(() => result++); }, () => false);

            await command.ExecuteAsync();

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void raise_can_execute_changed_when_command_manager_invalidate_requery_suggested()
        {
            var result = false;
            var command = new AsyncRelayCommand(() => Task.Delay(0));
            command.CanExecuteChanged += (sender, args) => {
                result = true;
            };

            CommandManager.InvalidateRequerySuggested();

            Check.That(result).IsTrue();
        }
    }
}