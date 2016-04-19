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
            builder.WithField("user", TestSchemas.CreateUserType());

            Assert.AreEqual(1, builder.Build().QueryType.Fields.Count());
            Assert.AreEqual("user", builder.Build().QueryType.Fields.First().Name);
            Assert.AreEqual(TestSchemas.CreateUserType().Name, builder.Build().QueryType.Fields.First().Type.Name);
        }

        [Test]
        public void BuildSchemaWith2Fields()
        {
            var schema = new SchemaBuilder()
                .RegisterTypes()
                .WithField(x => x.Name("user").Type(TestSchemas.CreateUserType()))
                .WithField("boss", TestSchemas.CreateBossType())
                .Build();

            Assert.AreEqual(2, schema.QueryType.Fields.Count());
            Assert.AreEqual("user", schema.QueryType.Fields.ElementAt(0).Name);
            Assert.AreEqual(TestSchemas.CreateUserType().Name, schema.QueryType.Fields.ElementAt(0).Type.Name);
            Assert.AreEqual("boss", schema.QueryType.Fields.ElementAt(1).Name);
            Assert.AreEqual(TestSchemas.CreateBossType().Name, schema.QueryType.Fields.ElementAt(1).Type.Name);
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
            Assert.AreEqual(TestSchemas.CreateUserType().Name, schema.QueryType.Fields.ElementAt(0).Type.Name);
            Assert.AreEqual("boss", schema.QueryType.Fields.ElementAt(1).Name);
            Assert.AreEqual(TestSchemas.CreateBossType().Name, schema.QueryType.Fields.ElementAt(1).Type.Name);
        }
    }
}