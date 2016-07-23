using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class NotifiableObject_should
    {
        [Fact]
        public void return_affected_value()
        {
            var person = new Person();

            person.FirstName = "Mark";

            Check.That(person.FirstName).IsEqualTo("Mark");
        }

        [Fact]
        public void raise_property_changed_when_setting_notifiable_property()
        {
            var person = new Person();
            PropertyChangedEventArgs propertyChangedEventArgs = null;
            person.PropertyChanged += (sender, args) => {
                propertyChangedEventArgs = args;
            };

            person.FirstName = "Dupond";

            Check.That(propertyChangedEventArgs).IsNotNull();
            Check.That(propertyChangedEventArgs.PropertyName).IsEqualTo("FirstName");
        }

        [Fact]
        public void not_raise_property_changed_when_setting_notifiable_property_with_same_values()
        {
            var person = new Person {FirstName = "Dupond"};
            PropertyChangedEventArgs propertyChangedEventArgs = null;
            person.PropertyChanged += (sender, args) => {
                propertyChangedEventArgs = args;
            };

            person.FirstName = "Dupond";

            Check.That(propertyChangedEventArgs).IsNull();
        }

        [Fact]
        public void be_thread_safe()
        {
            var person = new Person();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++) {
                tasks.Add(Task.Factory.StartNew(() => person.FirstName = i.ToString()));
            }
            Task.WaitAll(tasks.ToArray());
            Check.That(person.FirstName).IsNotNull();
        }

        private class Person : NotifiableObject
        {
            public string FirstName
            {
                get { return GetNotifiableProperty<string>(); }
                set { SetNotifiableProperty(value); }
            }
        }
    }
}
