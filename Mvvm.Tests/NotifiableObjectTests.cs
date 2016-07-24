using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class NotifiableObject_should
    {
        private readonly Person _person;
        private readonly List<PropertyChangedEventArgs> _propertyChangedEvents;

        public NotifiableObject_should()
        {
            _person = new Person {FirstName = "Georges", LastName = "Washington"};
            _propertyChangedEvents = new List<PropertyChangedEventArgs>();
            _person.PropertyChanged += (sender, args) => { _propertyChangedEvents.Add(args); };
        }

        [Fact]
        public void return_affected_value()
        {
            _person.Nickname = "Mark";

            Check.That(_person.Nickname).IsEqualTo("Mark");
        }

        [Fact]
        public void raise_property_changed_when_setting_notifiable_property()
        {
            _person.Nickname = "Dupond";

            Check.That(_propertyChangedEvents).HasSize(1);
            Check.That(_propertyChangedEvents[0].PropertyName).IsEqualTo("Nickname");
        }

        [Fact]
        public void not_raise_property_changed_when_setting_notifiable_property_with_same_values()
        {
            _person.Nickname = "Dupond";
            _person.Nickname = "Dupond";
            _person.Nickname = "Dupond";

            Check.That(_propertyChangedEvents).HasSize(1);
            Check.That(_propertyChangedEvents[0].PropertyName).IsEqualTo("Nickname");
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

        [Fact]
        public void raise_property_changed_when_setting_dependent_property_of_calculated()
        {
            _person.LastName = "Dupond";

            Check.That(_propertyChangedEvents).HasSize(2);
            Check.That(_propertyChangedEvents[0].PropertyName).IsEqualTo("LastName");
            Check.That(_propertyChangedEvents[1].PropertyName).IsEqualTo("FullName");
        }

        [Fact]
        public void return_calculated_property_value()
        {
            _person.LastName = "Dupond";

            Check.That(_person.FullName).IsEqualTo("Georges Dupond");
        }

        // ----- Private classes

        private class Person : NotifiableObject
        {
            public string Nickname
            {
                get { return GetNotifiableProperty<string>(); }
                set { SetNotifiableProperty(value); }
            }

            public string FirstName
            {
                get { return GetNotifiableProperty<string>(); }
                set { SetNotifiableProperty(value); }
            }

            public string LastName
            {
                get { return GetNotifiableProperty<string>(); }
                set { SetNotifiableProperty(value); }
            }

            public string FullName => FirstName + " " + LastName;
        }
    }
}