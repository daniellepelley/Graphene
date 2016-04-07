using System.Collections.Generic;
using System.Diagnostics;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test
{
    public class QueryTest
    {
        [Test]
        public void CanBuildAQuery()
        {
            var userType = new GraphQLObjectType
            {
                Name = "User",
                Fields = new[]
                {
                    new GraphQLFieldType
                    {
                        Name = "id",
                        OfType = new GraphQLFieldType
                        {
                            Kind = "STRING"
                        }
                    },
                    new GraphQLFieldType
                    {
                        Name = "name",
                        OfType = new GraphQLFieldType
                        {
                            Kind = "STRING"
                        }
                    }
                }
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "Query",
                    Fields = new[]
                    {
                        new GraphQLFieldType
                        {
                            Name = "user",
                            OfType = new GraphQLFieldType
                            {
                                Kind = "STRING"
                            }
                        }
                    }
                }
            };

            var document =
                new DocumentParser().Parse(
                    @"{__schema{queryType{name},mutationType{name},subscriptionType{name},types{kind,name,description}}}");

            var expected = @"{""data"":{""__schema"":{""types"":[";
        }
    }

    public class TypeIntrospectionTests
    {
        [Test]
        public void GraphQLStringAllFields()
        {
            var stringType = new GraphQLString();

            var actual = JsonConvert.SerializeObject(QueryType(stringType, new []{ "name", "description", "kind" }));

            var expected = @"{""name"":""String"",""description"":""This is a string"",""kind"":""SCALAR""}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLStringNameOnly()
        {
            var stringType = new GraphQLString();

            var actual = JsonConvert.SerializeObject(QueryType(stringType, new[] { "name" }));

            var expected = @"{""name"":""String""}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLStringNameDescriptionAndKind()
        {
            var stringType = new GraphQLString();

            var actual = JsonConvert.SerializeObject(QueryType(stringType, new[] { "description", "kind" }));

            var expected = @"{""description"":""This is a string"",""kind"":""SCALAR""}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLBooleanNameDescriptionAndKind()
        {
            var stringType = new GraphQLBoolean();

            var actual = JsonConvert.SerializeObject(QueryType(stringType, new[] {"name", "description", "kind"}));

            var expected = @"{""name"":""Boolean"",""description"":""This is a boolean"",""kind"":""SCALAR""}";

            Assert.AreEqual(expected, actual);
        }

        private IDictionary<string, string> QueryType(IGraphQLType graphQLType, string[] fields)
        {
            var graphQlTypeSelector = new GraphQlTypeSelector();
            return graphQlTypeSelector.Process(graphQLType, fields);
        }
    }
}