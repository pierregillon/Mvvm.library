using Mvvm.Validation;
using NFluent;
using Xunit;

namespace Mvvm.Tests
{
    public class ValidationRuleBuilder_should
    {
        private string Name { get; set; }
        private int Age { get; set; }

        [Fact]
        public void create_rule_from_method_call_expression()
        {
            var builder = new ValidationRuleBuilder();

            builder.Add(() => string.IsNullOrEmpty(Name), "Invalid name");

            var rules = builder.Build();
            Check.That(rules).HasSize(1);
            Check.That(rules).ContainsKey("Name");
            Check.That(rules["Name"]).HasSize(1);
            Check.That(rules["Name"][0].ErrorMessage).IsEqualTo("Invalid name");
        }

        [Fact]
        public void create_rule_from_binary_expression()
        {
            var builder = new ValidationRuleBuilder();

            builder.Add(() => Name != null, "Invalid name");

            var rules = builder.Build();
            Check.That(rules).HasSize(1);
            Check.That(rules).ContainsKey("Name");
            Check.That(rules["Name"]).HasSize(1);
            Check.That(rules["Name"][0].ErrorMessage).IsEqualTo("Invalid name");
        }

        [Fact]
        public void create_rule_from_combined_method_call_expressions()
        {
            var builder = new ValidationRuleBuilder();

            builder.Add(() => string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name), "Invalid name");

            var rules = builder.Build();
            Check.That(rules).HasSize(1);
            Check.That(rules).ContainsKey("Name");
            Check.That(rules["Name"]).HasSize(1);
            Check.That(rules["Name"][0].ErrorMessage).IsEqualTo("Invalid name");
        }

        [Fact]
        public void create_rule_from_aggregated_method_call_expression()
        {
            var builder = new ValidationRuleBuilder();

            builder.Add(() => bool.Parse(string.IsNullOrEmpty(Name).ToString()), "Invalid name");

            var rules = builder.Build();
            Check.That(rules).HasSize(1);
            Check.That(rules).ContainsKey("Name");
            Check.That(rules["Name"]).HasSize(1);
            Check.That(rules["Name"][0].ErrorMessage).IsEqualTo("Invalid name");
        }

        [Fact]
        public void create_rule_from_multiple_property_calls()
        {
            var builder = new ValidationRuleBuilder();

            builder.Add(() => string.IsNullOrEmpty(Name) || Age > 1, "Invalid name or age");

            var rules = builder.Build();
            Check.That(rules).HasSize(2);
            Check.That(rules).ContainsKey("Name");
            Check.That(rules).ContainsKey("Age");
            Check.That(rules["Name"]).ContainsExactly(rules["Age"]);
            Check.That(rules["Name"][0].ErrorMessage).IsEqualTo("Invalid name or age");
        }
    }
}
