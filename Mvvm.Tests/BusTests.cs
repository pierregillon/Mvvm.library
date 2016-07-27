using System;
using Mvvm.Routing;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class Bus_should
    {
        private readonly Bus _bus;

        public Bus_should()
        {
            _bus = new Bus();
        }

        [Fact(DisplayName = "notifies registered delegates")]
        public void notify_registered_delegates()
        {
            // Arrange
            var subscriber1 = new object();
            var valueReceived1 = default(int);

            var subscriber2 = new object();
            var valueReceived2 = default(int);

            _bus.Register<int>(subscriber1, message => valueReceived1 = message);
            _bus.Register<int>(subscriber2, message => valueReceived2 = message);

            // Act
            _bus.Send(88);

            // Asset
            Check.That(valueReceived1).IsEqualTo(88);
            Check.That(valueReceived2).IsEqualTo(88);
        }

        [Fact(DisplayName = "does not notify unregistered delegates")]
        public void not_notify_unregistered_delegates()
        {
            // Arrange
            var subscriber = new object();
            var valueReceived1 = default(int);
            var valueReceived2 = default(string);

            // Act
            _bus.Register<int>(subscriber, message => valueReceived1 = message);
            _bus.Register<string>(subscriber, message => valueReceived2 = message);
            _bus.Unregister(subscriber);

            _bus.Send(88);
            _bus.Send("hello");

            // Asset
            Check.That(valueReceived1).IsEqualTo(default(int));
            Check.That(valueReceived2).IsEqualTo(default(string));
        }

        [Fact(DisplayName = "raises original exception when registerer throws error")]
        public void raise_original_exception_when_registerer_throws_error()
        {
            // Arrange
            var subscriber1 = new object();
            _bus.Register<int>(subscriber1, message =>
            {
                throw new Exception("Boom");
            });

            // Act
            Action code = ()=> _bus.Send(88);

            // Asset
            Check.ThatCode(code).Throws<Exception>().WithMessage("Boom");
        }
    }
}
