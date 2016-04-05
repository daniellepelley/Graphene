using System;
using Graphene.Core;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Test.Execution;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test
{
    public class GraphQLQueryHandlerTests
    {
        [Test]
        public void SimpleQueryReturnsCorrectResponse1()
        {
            AssertResponse(@"{user(id:""1""){name}}", @"{""data"":{""user"":{""name"":""Dan""}}}");
        }

        [Test]
        public void SimpleQueryReturnsCorrectResponse2()
        {
            AssertResponse(@"{user(id:""2""){name}}", @"{""data"":{""user"":{""name"":""Lee""}}}");
        }

        [Test]
        public void SimpleQueryReturnsCorrectResponse3()
        {
            JsonConvert.DeserializeObject<TestUser>(@"{""data"":{""user"":{""name"":""Nick""}}}");

            AssertResponse(@"{user(id:""3""){name}}", @"{""data"":{""user"":{""name"":""Nick""}}}");
        }

        private static void AssertResponse(string query, string expected)
        {
            var mockParser = new Mock<IDocumentParser>();

            mockParser.Setup(x => x.Parse(query))
                .Returns(new Document());

            var func = new Func<int, string>(x => expected);

            var sut = new GraphQLQueryHandler(mockParser.Object);
            var actual = sut.Handle(query);

            Assert.AreEqual(typeof(Document).FullName, actual);
        }

        [Test]
        public void HandleCallsParser()
        {
            var mockParser = new Mock<IDocumentParser>();

            mockParser.Setup(x => x.Parse(It.IsAny<string>()))
                .Returns(new Document());

            var sut = new GraphQLQueryHandler(mockParser.Object);
            sut.Handle(@"{user(id:""2""){name}}");
            
            mockParser.Verify(x => x.Parse(@"{user(id:""2""){name}}"), Times.Once);
        }


    }
}
