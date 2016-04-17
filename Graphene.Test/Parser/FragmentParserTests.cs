using System.Linq;
using Graphene.Core.Lexer;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class FragmentParserTests
    {
        [Test]
        public void SimpleFragment()
        {
            var sut = new FragmentParser();

            var query = @"fragment InputValue on __InputValue {
                            name
                            description
                            type { ...TypeRef }
                            defaultValue

                          }";


            var fragments = sut.Parse(new GraphQLLexerFeed(query));

            var fragment = fragments.First();

            Assert.AreEqual("InputValue", fragment.Name);
            Assert.AreEqual("__InputValue", fragment.Type);

            Assert.AreEqual("name", fragment.Selections.ElementAt(0).Field.Name);
            Assert.AreEqual("description", fragment.Selections.ElementAt(1).Field.Name);
            Assert.AreEqual("type", fragment.Selections.ElementAt(2).Field.Name);
            Assert.AreEqual("...TypeRef", fragment.Selections.ElementAt(2).Field.Selections.First().Field.Name);
            Assert.AreEqual("defaultValue", fragment.Selections.ElementAt(3).Field.Name);
            Assert.AreEqual(4, fragment.Selections.Length);
        }

        [Test]
        public void SimpleFragment2()
        {
            var sut = new FragmentParser();

            var query = @"fragment FullType on __Type {
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
                          }";


            var fragments = sut.Parse(new GraphQLLexerFeed(query));
            var fragment = fragments.First();

            Assert.AreEqual("FullType", fragment.Name);
            Assert.AreEqual("__Type", fragment.Type);
            Assert.AreEqual("fields", fragment.Selections.ElementAt(3).Field.Name);
            Assert.AreEqual(6, fragment.Selections.ElementAt(3).Field.Selections.Length);
            Assert.AreEqual("...InputValue", fragment.Selections.ElementAt(3).Field.Selections.ElementAt(2).Field.Selections.ElementAt(0).Field.Name);
        }                          
    }
}