using System.Collections.Generic;
using Graphene.Core.Types;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test
{
    public class MappingTests
    {
        private readonly GraphQlTypeSelector _graphQlTypeSelector = new GraphQlTypeSelector();

        [Test]
        public void GraphQLStringAllFields()
        {
            var sut = new GraphQLString();

            var actual = Map(sut, new []{ "name", "description", "kind" });
            var expected = @"{""name"":""String"",""description"":""This is a string"",""kind"":""SCALAR""}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLStringAllFieldsReOrdered()
        {
            var sut = new GraphQLString();

            var actual = Map(sut, new[] { "description", "kind", "name" });
            var expected = @"{""description"":""This is a string"",""kind"":""SCALAR"",""name"":""String""}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLStringNameOnly()
        {
            var sut = new GraphQLString();

            var actual = Map(sut, new[] { "name" });
            var expected = @"{""name"":""String""}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLStringNameDescriptionAndKind()
        {
            var stringType = new GraphQLString();

            var actual = Map(stringType, new[] { "description", "kind" });
            var expected = @"{""description"":""This is a string"",""kind"":""SCALAR""}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GraphQLBooleanNameDescriptionAndKind()
        {
            var stringType = new GraphQLBoolean();

            var actual = Map(stringType, new[] {"name", "description", "kind"});
            var expected = @"{""name"":""Boolean"",""description"":""This is a boolean"",""kind"":""SCALAR""}";

            Assert.AreEqual(expected, actual);
        }

        private string Map(IGraphQLType graphQLType, string[] fields)
        {
            return JsonConvert.SerializeObject(_graphQlTypeSelector.Process(graphQLType, fields));
        }
    }
}