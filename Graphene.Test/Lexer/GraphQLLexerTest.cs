using System.Diagnostics;
using System.Linq;
using System.Text;
using Graphene.Core.Lexer;
using NUnit.Framework;

namespace Graphene.Test.Lexer
{
    public class GraphQLLexerTests
    {
        [TestCase(0, "name")]
        [TestCase(1, "{")]
        [TestCase(2, "}")]
        public void Test1(int index, string expected)
        {
            var parserFeed = new GraphQLLexer("name{}");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expected, ((LexerToken)output[index]).Value);
        }

        [TestCase(0, "person")]
        [TestCase(1, "{")]
        [TestCase(2, "name")]
        [TestCase(3, "}")]
        public void Test2(int index, string expected)
        {
            var parserFeed = new GraphQLLexer("person {name}");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expected, ((LexerToken)output[index]).Value);
        }

        [TestCase(0, "person", GraphQLTokenType.Name)]
        [TestCase(1, "(", GraphQLTokenType.ParenL)]
        [TestCase(2, "id", GraphQLTokenType.Name)]
        [TestCase(3, ":", GraphQLTokenType.Colon)]
        [TestCase(4, "1", GraphQLTokenType.Name)]
        [TestCase(5, ")", GraphQLTokenType.ParenR)]
        [TestCase(6, "{", GraphQLTokenType.BraceL)]
        [TestCase(7, "name", GraphQLTokenType.Name)]
        [TestCase(8, "address", GraphQLTokenType.Name)]
        [TestCase(9, "{", GraphQLTokenType.BraceL)]
        [TestCase(10, "street", GraphQLTokenType.Name)]
        [TestCase(11, "town", GraphQLTokenType.Name)]
        [TestCase(12, "}", GraphQLTokenType.BraceR)]
        [TestCase(13, "}", GraphQLTokenType.BraceR)]
        public void Test3(int index, string expectedValue, string expectedType)
        {
            var parserFeed = new GraphQLLexer("person(id :  1)   {name, address { street, town }}");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expectedValue, ((LexerToken)output[index]).Value);
            Assert.AreEqual(expectedType, ((LexerToken)output[index]).Type);
        }

        [TestCase(0, "should", GraphQLTokenType.Name)]
        [TestCase(1, "pick", GraphQLTokenType.Name)]
        [TestCase(2, "up", GraphQLTokenType.Name)]
        [TestCase(3, "...", GraphQLTokenType.Spread)]
        [TestCase(4, "{", GraphQLTokenType.BraceL)]
        [TestCase(5, "spreads", GraphQLTokenType.Name)]
        public void Test4(int index, string expectedValue, string expectedType)
        {
            var parserFeed = new GraphQLLexer("should pick up ...{ spreads");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expectedValue, ((LexerToken)output[index]).Value);
            Assert.AreEqual(expectedType, ((LexerToken)output[index]).Type);
        }
    }

    public class GraphQLLexerFeedMatchTests
    {
        [TestCase(0, "alias")]
        [TestCase(1, ":")]
        [TestCase(2, "name")]
        public void TestPass(int index, string expected)
        {
            var parserFeed = new GraphQLLexerFeed("alias : name");
            var output = parserFeed.Match(GraphQLTokenType.Name, GraphQLTokenType.Colon, GraphQLTokenType.Name);
            Assert.AreEqual(expected, ((LexerToken) output[index]).Value);
        }

        [Test]
        public void TestFail()
        {
            var parserFeed = new GraphQLLexerFeed("alias ( name");
            var output = parserFeed.Match(GraphQLTokenType.Name, GraphQLTokenType.Colon, GraphQLTokenType.Name);
            Assert.AreEqual(0, output.Length);
        }

        [Test]
        public void IfMoreThanLengthRemaining()
        {
            var parserFeed = new GraphQLLexerFeed("alias :");
            var output = parserFeed.Match(GraphQLTokenType.Name, GraphQLTokenType.Colon, GraphQLTokenType.Name);
            Assert.AreEqual(0, output.Length);
        }

        [TestCase(0, "person", GraphQLTokenType.Name)]
        [TestCase(1, "(", GraphQLTokenType.ParenL)]
        [TestCase(2, "id", GraphQLTokenType.Name)]
        [TestCase(3, ":", GraphQLTokenType.Colon)]
        [TestCase(4, "1", GraphQLTokenType.Name)]
        [TestCase(5, ")", GraphQLTokenType.ParenR)]
        [TestCase(6, "{", GraphQLTokenType.BraceL)]
        [TestCase(7, "name", GraphQLTokenType.Name)]
        [TestCase(8, "address", GraphQLTokenType.Name)]
        [TestCase(9, "{", GraphQLTokenType.BraceL)]
        [TestCase(10, "street", GraphQLTokenType.Name)]
        [TestCase(11, "town", GraphQLTokenType.Name)]
        [TestCase(12, "}", GraphQLTokenType.BraceR)]
        [TestCase(13, "}", GraphQLTokenType.BraceR)]
        public void IgnoresComments(int index, string expectedValue, string expectedType)
        {
            var query = @"
#Comment
  person(id :  1)#Comment
  {#Comment
    name#Comment
    address #Comment { ignore }
    {#Comment
      street #Comment
      town     #Comment
    }#Comment
  }#Comment
";

            var parserFeed = new GraphQLLexer(query);
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expectedValue, ((LexerToken)output[index]).Value);
            Assert.AreEqual(expectedType, ((LexerToken)output[index]).Type);
        }

        [Test]
        [Ignore("Timeing test")]
        public void CanParseBasicQueryInUnderOneMicrosecond()
        { 
            var parserFeed = new GraphQLLexer("person(id :  1)   {name, address { street, town }}");

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (var i = 0; i < 100000; i++)
            {
                parserFeed.All().ToArray();
            }

            var ticksPerLex = stopWatch.ElapsedTicks / 100000;
            Assert.IsTrue(ticksPerLex < 10);
        }

        [Test]
        public void CanParseBasicQueryInUnderOneMicrosecond2()
        {
            var query = @"
#Comment
  person(id :  1)#Comment
  {#Comment
    name#Comment
    address #Comment { ignore }
    {#Comment
      street #Comment
      town     #Comment
    }#Comment
  }#Comment
";

            

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (var i = 0; i < 100; i++)
            {
                var parserFeed = new GraphQLLexer(query);
                parserFeed.All().ToArray();
            }

            var ticksPerLex = stopWatch.ElapsedTicks / 100000;
            Assert.IsTrue(ticksPerLex < 10);
        }

        [Test]
        public void CanParseBasicQueryInUnderOneMicrosecond3()
        {
            var query = @"{query IntrospectionQuery {
                            __schema {
                              queryType { name }
                              mutationType { name }
                              subscriptionType { name }
                              types {
                                ...FullType
                              }
                              directives {
                                name
                                description
                                args {
                                  ...InputValue
                                }
                                onOperation
                                onFragment
                                onField
                              }
                            }
                          }

                          fragment FullType on __Type {
                            kind
                            name
                            description
                            fields(includeDeprecated: true) {
                              name
                              description
                              args {
                                ...InputValue
                              }
                              type {
                                ...TypeRef
                              }
                              isDeprecated
                              deprecationReason
                            }
                            inputFields {
                              ...InputValue
                            }
                            interfaces {
                              ...TypeRef
                            }
                            enumValues(includeDeprecated: true) {
                              name
                              description
                              isDeprecated
                              deprecationReason
                            }
                            possibleTypes {
                              ...TypeRef
                            }
                          }

                          fragment InputValue on __InputValue {
                            name
                            description
                            type { ...TypeRef }
                            defaultValue
                          }

                          fragment TypeRef on __Type {
                            kind
                            name
                            ofType {
                              kind
                              name
                              ofType {
                                kind
                                name
                                ofType {
                                  kind
                                  name
                                }
                              }
                            }
                          }
                        }";

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (var i = 0; i < 100; i++)
            {
                var parserFeed = new GraphQLLexer(query);
                parserFeed.All().ToArray();
            }

            var ticksPerLex = stopWatch.ElapsedMilliseconds;
            //Assert.IsTrue(ticksPerLex < 10);
        }
    }

    public class GraphQLLexerCursorTests
    {
        [Test]
        public void MatchTest()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < 100000; i++)
            {
                sb.Append("123abc");
            }

            Stopwatch stopwatch = new Stopwatch();

            GraphQLLexerCursor sut = new GraphQLLexerCursor(sb.ToString());

            byte[] bytes123 = Encoding.ASCII.GetBytes("123");
            byte[] bytesabc = Encoding.ASCII.GetBytes("abc");

            stopwatch = new Stopwatch();
            stopwatch.Start();
            sut = new GraphQLLexerCursor(sb.ToString());
            for (int i = 0; i < 100000; i++)
            {
                sut.TakeWhile(bytes123);
                sut.TakeWhile(bytesabc);
            }
            stopwatch.Stop();
            var result3 = stopwatch.ElapsedMilliseconds;

            //for (int i = 0; i < 100000; i++)
            //{
            //    sut.TakeWhile("123");
            //    sut.TakeWhile("abc");
            //}

        }

    }
}