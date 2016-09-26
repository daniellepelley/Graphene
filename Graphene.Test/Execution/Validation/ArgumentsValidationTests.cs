using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;
using Graphene.Core.Types.Scalar;
using Graphene.Execution;
using NUnit.Framework;

namespace Graphene.Test.Execution.Validation
{
    public class ArgumentsValidationTests
    {
        [Test]
        public void WhenArgumentIsMatches()
        {
            var arguments = Arguments("id", 1);
            var graphQLArguments = GraphQLArguments("id", new [] { "Int" });

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, graphQLArguments, "bar", "baz");

            Assert.IsFalse(result.Any());
        }

        [Test]
        public void WhenArgumentValueIdWrongType()
        {
            var arguments = Arguments("id", "42");
            var graphQLArguments = GraphQLArguments("id", new[] { "Int" });

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, graphQLArguments, "bar", "baz");

            Assert.AreEqual("Argument 'id' has invalid value 42. Expected type 'Int'", result.First());
        }

        [Test]
        public void WhenArgumentIsNotKnown1()
        {
            var arguments = Arguments("foo", 42);
            var graphQLArguments = GraphQLArguments("bar", new[] { "Int" });

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, graphQLArguments, "bar", "baz");

            Assert.AreEqual(@"Unknown argument ""foo"" on field ""bar"" on type ""baz"".", result.First());
        }

        [Test]
        public void WhenArgumentIsNotKnown2()
        {
            var arguments = Arguments("foo", 42);
            var graphQLArguments = GraphQLArguments("bar", new[] { "Int" });

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, graphQLArguments, null, "baz");

            Assert.AreEqual(@"Unknown argument ""foo"" on type ""baz"".", result.First());
        }

        [Test]
        public void WhenArgumentIsNotKnown3()
        {
            var arguments = Arguments("foo", 42);
            var graphQLArguments = GraphQLArguments("bar", new[] { "Int" });

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, graphQLArguments, "bar", null);

            Assert.AreEqual(@"Unknown argument ""foo"" on field ""bar"".", result.First());
        }

        [Test]
        public void WhenArgumentIsNotKnown4()
        {
            var arguments = Arguments("foo", 42);

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, null, "bar", "baz");

            Assert.AreEqual(@"Unknown argument ""foo"" on field ""bar"" on type ""baz"".", result.First());
        }

        [Test]
        public void WhenArgumentIsNotKnown5()
        {
            var arguments = Arguments("foo", 42);
            var graphQLArguments = GraphQLArguments("bar", new[] { "Int" });

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, graphQLArguments, null, null);

            Assert.AreEqual(@"Unknown argument ""foo"".", result.First());
        }

        [Test]
        public void PassInNulls()
        {
            var result = new OperationValidator(TypeList.Create()).Validate(null, null, null, null);

            Assert.IsFalse(result.Any());
        }

        [Test]
        public void WhenArgumentIsRepeated()
        {
            var arguments = Arguments("id", 42).Concat(Arguments("id", 42)).ToArray();
            var graphQLArguments = GraphQLArguments("id", new[] { "Int" });

            var result = new OperationValidator(TypeList.Create()).Validate(arguments, graphQLArguments, "bar", "baz");

            Assert.AreEqual(@"There can be only one argument named ""id""", result.First());
            Assert.AreEqual(1, result.Length);
        }

        private static Argument[] Arguments(string name, object value)
        {
            var arguments = new[]
            {
                new Argument
                {
                    Name = name,
                    Value = value
                }
            };
            return arguments;
        }

        private static IGraphQLArgument[] GraphQLArguments(string name, string[] type)
        {
            var arguments = new IGraphQLArgument[]
            {
                new GraphQLArgument
                {
                    Name = name,
                    Type = type
                }
            };
            return arguments;
        }
    }
}