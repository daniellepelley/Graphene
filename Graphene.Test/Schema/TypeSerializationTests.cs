using System.IO;
using Graphene.Schema;
using NUnit.Framework;

namespace Graphene.Test.Schema
{
    public class TypeSerializationTests
    {
        [Test]
        public void StringTypeTest()
        {
            var json = File.ReadAllText(@"Schema\StringType.json");

            var sut = new GraphQLType
            {
                Kind = "SCALAR",
                Name = "String"
            };
            var actual = Json.Serialize(sut);

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void BooleanTypeTest()
        {
            var json = File.ReadAllText(@"Schema\BooleanType.json");

            var sut = new GraphQLType
            {
                Kind = "SCALAR",
                Name = "Boolean"
            };
            var actual = Json.Serialize(sut);
            
            Assert.AreEqual(json, actual);
        }

        [Test]
        public void InterfaceFieldTest()
        {
            var json = File.ReadAllText(@"Schema\InterfaceFieldExample.json");

            var sut = new GraphQLField
            {
                Name = "hero",
                Type = new GraphQLFieldType
                {
                    Kind = "INTERFACE",
                    Name = "Character"
                }
            };
            var actual = Json.Serialize(sut);

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void FieldTypeTest()
        {
            var json = File.ReadAllText(@"Schema\FieldTypeExample.json");

            var sut = new GraphQLFieldType
            {
                Kind = "NON_NULL",
                OfType = new GraphQLFieldType
                {
                    Kind = "LIST",
                    OfType = new GraphQLFieldType
                    {
                        Kind = "OBJECT",
                        Name = "__Directive"
                    }
                }
            };

            var actual = Json.Serialize(sut);

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void HumanFieldTest()
        {
            var json = File.ReadAllText(@"Schema\HumanFieldExample.json");

            var sut = new GraphQLField
            {
                Name = "human",
                Args = new[]
                {
                    new GraphQLArg
                    {
                        Name = "id",
                        Description = "idofthehuman",
                        Type = new GraphQLFieldType
                        {
                            Kind = "NON_NULL",
                            OfType = new GraphQLFieldType
                            {
                                Kind = "SCALAR",
                                Name = "String"
                            }
                        }
                    }
                },
                Type = new GraphQLFieldType
                {
                    Kind = "OBJECT",
                    Name = "Human"
                }
            };
            var actual = Json.Serialize(sut);

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void QueryTypeTest()
        {
            var json = File.ReadAllText(@"Schema\QueryExample.json");

            var sut = new GraphQLType
            {
                Kind = "OBJECT",
                Name = "Query",
                Fields = new[]
                {
                    new GraphQLField
                    {
                        Name = "hero",
                        Type = new GraphQLFieldType
                        {
                            Kind = "INTERFACE",
                            Name = "Character"
                        }
                    },
                    new GraphQLField
                    {
                        Name = "human",
                        Args = new[]
                        {
                            new GraphQLArg
                            {
                                Name = "id",
                                Description = "idofthehuman",
                                Type = new GraphQLFieldType
                                {
                                    Kind = "NON_NULL",
                                    OfType = new GraphQLFieldType
                                    {
                                        Kind = "SCALAR",
                                        Name = "String"
                                    }
                                }
                            }
                        },
                        Type = new GraphQLFieldType
                        {
                            Kind = "OBJECT",
                            Name = "Human"
                        }
                    },
                    new GraphQLField
                    {
                        Name = "droid",
                        Args = new[]
                        {
                            new GraphQLArg
                            {
                                Name = "id",
                                Description = "idofthedroid",
                                Type = new GraphQLFieldType
                                {
                                    Kind = "NON_NULL",
                                    OfType = new GraphQLFieldType
                                    {
                                        Kind = "SCALAR",
                                        Name = "String"
                                    }
                                }
                            }
                        },
                        Type = new GraphQLFieldType
                        {
                            Kind = "OBJECT",
                            Name = "Droid"
                        }
                    }
                }
            };
            var actual = Json.Serialize(sut);

            Assert.AreEqual(json, actual);
        }
    }
}
