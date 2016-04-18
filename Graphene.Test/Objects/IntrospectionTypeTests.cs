using System;
using System.Collections.Generic;
using Graphene.Core;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Test.Data;
using NUnit.Framework;

namespace Graphene.Test.Objects
{
    public class IntrospectionTypeTests
    {
        public object TestType(string fieldName, Func<ResolveObjectContext, IGraphQLType> resolve, string selection = null)
        {
            var type = new __Type();
            var query = "__Type{" + fieldName + selection + "}";
            var dictionary = TestHelpers.QueryAType(type, "__Type", query, resolve);
            return dictionary[fieldName];
        }

        [Test]
        public void GraphQLString()
        {
            var expected = "String";
            var actual = TestType("name", _ => new GraphQLString());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLBoolean()
        {
            var expected = "Boolean";
            var actual = TestType("name", _ => new GraphQLBoolean());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLObject()
        {
            var expected = "Boss";
            var actual = TestType("name", _ => TestSchemas.CreateBossType());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLScalarKind()
        {
            var expected = "SCALAR";
            var actual = TestType("kind", _ => new GraphQLString());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLObjectKind()
        {
            var expected = "OBJECT";
            var actual = TestType("kind", _ => TestSchemas.CreateBossType());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLDescription()
        {
            var actual = TestType("description", _ => new GraphQLString());
            Assert.IsTrue(actual.ToString().Length > 100);
        }

        [Test]
        public void GraphQLOfType()
        {
            var actual = TestType("ofType", _ => new GraphQLString(), "{name}");
            Assert.IsNull(actual);
        }

        [Test]
        public void GraphQLFields()
        {
            var actual = TestType("fields", _ => TestSchemas.CreateBossType(), "{name}");
            var list = (List<object>)actual;
            var field1 = (IDictionary<string, object>)list[0];
            var field2 = (IDictionary<string, object>) list[1];

            Assert.AreEqual("id", field1["name"]);
            Assert.AreEqual("name", field2["name"]);
        }

        [Test]
        public void GraphQLFieldsOfType()
        {
            var actual = TestType("fields", _ => TestSchemas.CreateBossType(), "{isDeprecated,deprecationReason}");
            var list = (List<object>)actual;
            var field1 = (IDictionary<string, object>)list[0];
            var field2 = (IDictionary<string, object>)list[1];

            Assert.AreEqual(false, field1["isDeprecated"]);
            Assert.AreEqual(null, field2["deprecationReason"]);
        }
    }
}