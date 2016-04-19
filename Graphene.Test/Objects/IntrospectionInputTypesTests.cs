using System;
using System.Collections.Generic;
using Graphene.Core;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Scalar;
using Graphene.Test.Data;
using NUnit.Framework;

namespace Graphene.Test.Objects
{
    public class IntrospectionInputTypesTests
    {
        [Test]
        public void GraphQLArgumentName()
        {
            var argument = GetGraphQLArgument();
            var expected = "id";
            var actual = TestArgument("name", _ => argument);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLArgumentDescription()
        {
            var argument = GetGraphQLArgument();
            var expected = "This is a test argument";
            var actual = TestArgument("description", _ => argument);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLArgumentDefaultValue()
        {
            var argument = GetGraphQLArgument();
            var expected = "foo";
            var actual = TestArgument("defaultValue", _ => argument);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLArgumentType()
        {
            var argument = GetGraphQLArgument();
            var actual = (IDictionary<string, object>)TestArgument("type", _ => argument, "{name,kind,description}");
            Assert.AreEqual("String", actual["name"]);
            Assert.AreEqual("SCALAR", actual["kind"]);
            Assert.IsTrue(actual["description"].ToString().Length > 100);
        }

        public object TestArgument(string fieldName, Func<ResolveObjectContext, IGraphQLArgument> resolve, string selection = null)
        {
            var type = new __InputValue(TestSchemas.GetTypeList());
            var query = "__InputValue{" + fieldName + selection + "}";
            var dictionary = TestHelpers.QueryAType(type, "__InputValue", query, resolve);
            return dictionary[fieldName];
        }

        private static GraphQLArgument GetGraphQLArgument()
        {
            var argument = new GraphQLArgument
            {
                Name = "id",
                Description = "This is a test argument",
                DefaultValue = "foo",
                Type = new GraphQLString()
            };
            return argument;
        }
    }
}