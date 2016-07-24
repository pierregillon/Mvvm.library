using System;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class DependencyPropertyTracker_should
    {
        private readonly Person _person;
        private readonly DependencyPropertyTracker _tracker;

        public DependencyPropertyTracker_should()
        {
            _person = new Person();
            _tracker = new DependencyPropertyTracker(_person);
        }

        [Fact]
        public void track_concatened_dependent_property()
        {
            var dependentProperties = _tracker.GetDependentProperties(nameof(_person.FirstName));

            Check.That(dependentProperties).ContainsExactly(nameof(_person.FullName));
        }

        [Fact]
        public void track_dependent_property_with_method_call()
        {
            var dependentProperties = _tracker.GetDependentProperties(nameof(_person.BirthDate));

            Check.That(dependentProperties).ContainsExactly(nameof(_person.Age));
        }


        private class Person : NotifiableObject
        {
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

            public DateTime BirthDate
            {
                get { return GetNotifiableProperty<DateTime>(); }
                set { SetNotifiableProperty(value); }
            }

            public string FullName => FirstName + " " + LastName;
            public int Age => (int)DateTime.Now.Subtract(BirthDate).TotalDays / 365;
        }
    }
}
