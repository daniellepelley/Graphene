using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphene.Core;
using Moq;
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
            AssertResponse(@"{user(id:""3""){name}}", @"{""data"":{""user"":{""name"":""Nick""}}}");
        }

        private static void AssertResponse(string query, string expected)
        {
            var mockParser = new Mock<IGraphQLParser>();

            var func = new Func<int, string>(x => expected);

            var sut = new GraphQLQueryHandler(mockParser.Object, func);
            var actual = sut.Handle(query);

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void HandleCallsParser()
        {
            var mockParser = new Mock<IGraphQLParser>();

            var func = new Func<int, string>(x => string.Empty);

            var sut = new GraphQLQueryHandler(mockParser.Object, func);
            sut.Handle(@"{user(id:""2""){name}}");
            
            mockParser.Verify(x => x.Parse(@"{user(id:""2""){name}}"), Times.Once);
        }

        private TestUser[] GetData()
        {
            return new[]
            {
                new TestUser
                {
                    Id = 1,
                    Name= "Dan"
                },
                                new TestUser
                {
                    Id = 2,
                    Name= "Lee"
                },
                                new TestUser
                {
                    Id = 3,
                    Name= "Nick"
                }
            };
        }
    }

    public class TestUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
