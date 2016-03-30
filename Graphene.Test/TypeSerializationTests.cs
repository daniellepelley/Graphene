using System.IO;
using NUnit.Framework;

namespace Graphene.Test
{
    public class TypeSerializationTests
    {
        [Test]
        public void StringTypeTest()
        {
            var json = File.ReadAllText("StringType.json");

            var sut = new GraphQlType
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
            var json = File.ReadAllText("BooleanType.json");

            var sut = new GraphQlType
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
            var json = File.ReadAllText("InterfaceFieldExample.json");

            var sut = new GraphQlField
            {
                Name = "hero",
                Type = new GraphQlFieldType
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
            var json = File.ReadAllText("FieldTypeExample.json");

            var sut = new GraphQlFieldType
            {
                Kind = "NON_NULL",
                OfType = new GraphQlFieldType
                {
                    Kind = "LIST",
                    OfType = new GraphQlFieldType
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
            var json = File.ReadAllText("HumanFieldExample.json");

            var sut = new GraphQlField
            {
                Name = "human",
                Args = new[]
                {
                    new GraphQlArg
                    {
                        Name = "id",
                        Description = "idofthehuman",
                        Type = new GraphQlFieldType
                        {
                            Kind = "NON_NULL",
                            OfType = new GraphQlFieldType
                            {
                                Kind = "SCALAR",
                                Name = "String"
                            }
                        }
                    }
                },
                Type = new GraphQlFieldType
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
            var json = File.ReadAllText("QueryExample.json");

            var sut = new GraphQlType
            {
                Kind = "OBJECT",
                Name = "Query",
                Fields = new[]
                {
                    new GraphQlField
                    {
                        Name = "hero",
                        Type = new GraphQlFieldType
                        {
                            Kind = "INTERFACE",
                            Name = "Character"
                        }
                    },
                    new GraphQlField
                    {
                        Name = "human",
                        Args = new[]
                        {
                            new GraphQlArg
                            {
                                Name = "id",
                                Description = "idofthehuman",
                                Type = new GraphQlFieldType
                                {
                                    Kind = "NON_NULL",
                                    OfType = new GraphQlFieldType
                                    {
                                        Kind = "SCALAR",
                                        Name = "String"
                                    }
                                }
                            }
                        },
                        Type = new GraphQlFieldType
                        {
                            Kind = "OBJECT",
                            Name = "Human"
                        }
                    },
                    new GraphQlField
                    {
                        Name = "droid",
                        Args = new[]
                        {
                            new GraphQlArg
                            {
                                Name = "id",
                                Description = "idofthedroid",
                                Type = new GraphQlFieldType
                                {
                                    Kind = "NON_NULL",
                                    OfType = new GraphQlFieldType
                                    {
                                        Kind = "SCALAR",
                                        Name = "String"
                                    }
                                }
                            }
                        },
                        Type = new GraphQlFieldType
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
