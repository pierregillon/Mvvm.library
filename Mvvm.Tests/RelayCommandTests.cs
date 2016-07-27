using Mvvm.Commands;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class RelayCommand_should
    {
        [Fact]
        public void call_delegate_on_execute()
        {
            var result = 0;
            var command = new RelayCommand(() => { result ++; });

            command.Execute();

            Check.That(result).IsEqualTo(1);
        }

        [Fact]
        public void not_call_delegate_on_execute_when_cannot_execute()
        {
            var result = 0;
            var command = new RelayCommand(() => { result++; }, () => false);

            command.Execute();

            Check.That(result).IsEqualTo(0);
        }

        [Fact]
        public void raise_can_execute_changed_when_command_manager_invalidate_requery_suggested()
        {
            var result = false;
            var command = new RelayCommand(() => {});
            command.CanExecuteChanged += (sender, args) => {
                result = true;
            };

            CommandManager.InvalidateRequerySuggested();

            Check.That(result).IsTrue();
        }
    }
}