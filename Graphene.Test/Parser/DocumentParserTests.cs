using System.Linq;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class DocumentParserTests
    {
        [Test]
        public void Parse()
        {
            var sut = new DocumentParser();
            var result = sut.Parse(@"{user(id:1){name}}");
            var operation = result.Operations.First();

            Assert.AreEqual("user", operation.Directives.First().Name);
            Assert.AreEqual("name", operation.Selections.First().Field.Name);
        }

        [Test]
        public void WithoutBrackets()
        {
            var sut = new DocumentParser();
            var result = sut.Parse(@"user(id:1){name}");
            var operation = result.Operations.First();

            Assert.AreEqual("user", operation.Directives.First().Name);
            Assert.AreEqual("name", operation.Selections.First().Field.Name);
        }

        [Test]
        public void Without2Fields()
        {
            var sut = new DocumentParser();
            var result = sut.Parse(@"user(id:1){name,age}");
            var operation = result.Operations.First();

            Assert.AreEqual("user", operation.Directives.First().Name);
            Assert.AreEqual("name", operation.Selections.ElementAt(0).Field.Name);
            Assert.AreEqual("age", operation.Selections.ElementAt(1).Field.Name);
        }

        [Test]
        public void WithFragments()
        {
            var sut = new DocumentParser();

            var query = @"query IntrospectionQuery {
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
                          }";

            var result = sut.Parse(query);
            var operation = result.Operations.First();

            Assert.AreEqual("IntrospectionQuery", operation.Directives.First().Name);
            Assert.AreEqual("__schema", operation.Selections.First().Field.Name);
            Assert.AreEqual(3, result.Fragments.Length);
        }
    }
}

