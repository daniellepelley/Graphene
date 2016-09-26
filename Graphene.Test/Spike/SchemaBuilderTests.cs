using System.Linq;
using Graphene.Core.Types;
using Graphene.Test.Data;
using NUnit.Framework;
using SchemaBuilder = Graphene.Spike.SchemaBuilder;

namespace Graphene.Test.Spike
{
    public class SchemaBuilderTests
    {
        [Test]
        public void BuildSchema()
        {
            var builder = new SchemaBuilder();
            Assert.IsInstanceOf<GraphQLSchema>(builder.Build());
        }

        [Test]
        public void BuildSchemaWith1Field()
        {
            var builder = new SchemaBuilder();
            builder.WithField("user", new [] { TestSchemas.CreateUserType().Name });

            var schema = builder.Build();

            Assert.AreEqual(1, schema.QueryType.Fields.Count());
            Assert.AreEqual("user", schema.QueryType.Fields.First().Name);
            Assert.AreEqual(TestSchemas.CreateUserType().Name, schema.QueryType.Fields.First().Type.Last());
        }

        [Test]
        public void BuildSchemaWith2Fields()
        {
            var schema = new SchemaBuilder()
                .RegisterTypes()
                .WithField(x => x.Name("user").Type(TestSchemas.CreateUserType()))
                .WithField("boss", new [] { TestSchemas.CreateBossType().Name })
                .Build();

            Assert.AreEqual(2, schema.QueryType.Fields.Count());
            Assert.AreEqual("user", schema.QueryType.Fields.ElementAt(0).Name);
            Assert.AreEqual(TestSchemas.CreateUserType().Name, schema.QueryType.Fields.ElementAt(0).Type.Last());
            Assert.AreEqual("boss", schema.QueryType.Fields.ElementAt(1).Name);
            Assert.AreEqual(TestSchemas.CreateBossType().Name, schema.QueryType.Fields.ElementAt(1).Type.Last());
        }

        [Test]
        public void BuildSchemaWithTypes()
        {
            var schema = new SchemaBuilder()
                .RegisterTypes(TestSchemas.CreateBossType(), TestSchemas.CreateUserType())
                .WithField(x =>
                    x.Name("user").Type("User"))
                .WithField("boss", x => x.Type("Boss"))
                .Build();

            Assert.AreEqual(2, schema.QueryType.Fields.Count());
            Assert.AreEqual("user", schema.QueryType.Fields.ElementAt(0).Name);
            Assert.AreEqual(TestSchemas.CreateUserType().Name, schema.QueryType.Fields.ElementAt(0).Type.Last());
            Assert.AreEqual("boss", schema.QueryType.Fields.ElementAt(1).Name);
            Assert.AreEqual(TestSchemas.CreateBossType().Name, schema.QueryType.Fields.ElementAt(1).Type.Last());
        }

        [Test]
        public void CanBeBuiltMultipleTimes()
        {
            var schemaBuilder = new SchemaBuilder()
                .RegisterTypes(TestSchemas.CreateBossType(), TestSchemas.CreateUserType())
                .WithField(x =>
                    x.Name("user").Type("User"))
                .WithField("boss", x => x.Type("Boss"));

            schemaBuilder.Build();

            Assert.DoesNotThrow(() => schemaBuilder.Build());
        }
    }
}