using System;
using System.Collections.Generic;
using Graphene.Core;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Scalar;
using Graphene.Test.Data;
using NUnit.Framework;

namespace Graphene.Test.Objects
{
    public class IntrospectionFieldTests
    {
        public object TestType(string fieldName, Func<ResolveObjectContext, IGraphQLFieldType> resolve, string selection = null)
        {
            var type = new __Field();
            var query = "__Field{" + fieldName + selection + "}";
            var dictionary = TestHelpers.QueryAType(type, "__Field", query, resolve);
            return dictionary[fieldName];
        }

        [Test]
        public void Name()
        {
            var field = new GraphQLObjectField<object, object>
            {
                Name = "foo"
            };

            var actual = TestType("name", _ => field);
            Assert.AreEqual("foo", actual);
        }

        [Test]
        public void Description()
        {
            var field = new GraphQLObjectField<object, object>
            {
                Description = "This is foo"
            };

            var actual = TestType("description", _ => field);
            Assert.AreEqual("This is foo", actual);
        }

        [Test]
        public void Type()
        {
            var field = new GraphQLObjectField
            {
                Type = TestSchemas.CreateBossType()
            };

            var actual = TestType("type", _ => field, "{name}");
            var dictionary = (IDictionary<string, object>)actual;

            Assert.AreEqual("Boss", dictionary["name"]);
        }

        [Test]
        public void Arguments()
        {
            var field = new GraphQLObjectField<object, object>
            {
                Name = "foo",
                Arguments = new IGraphQLArgument[]
                {
                    new GraphQLArgument
                    {
                        Name = "argument1",
                        Description = "This is argument1",
                        DefaultValue = "foo 1",
                        Type = new GraphQLId()
                    },
                    new GraphQLArgument
                    {
                        Name = "argument2",
                        Description = "This is argument2",
                        DefaultValue = "foo 2",
                        Type = new GraphQLFloat()
                    }
                }
            };

            var actual = TestType("args", _ => field, "{name,description,defaultValue,type{name}}");
            var list = (List<object>)actual;
            var field1 = (IDictionary<string, object>)list[0];
            var field2 = (IDictionary<string, object>)list[1];

            Assert.AreEqual("argument1", field1["name"]);
            Assert.AreEqual("This is argument1", field1["description"]);
            Assert.AreEqual("foo 1", field1["defaultValue"]);
            Assert.AreEqual("ID", ((IDictionary<string, object>)field1["type"])["name"]);

            Assert.AreEqual("argument2", field2["name"]);
            Assert.AreEqual("This is argument2", field2["description"]);
            Assert.AreEqual("foo 2", field2["defaultValue"]);
            Assert.AreEqual("Float", ((IDictionary<string, object>)field2["type"])["name"]);
        }
    }
}