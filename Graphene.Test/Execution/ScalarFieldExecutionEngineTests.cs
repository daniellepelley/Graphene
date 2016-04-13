using System;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Spike;
using Moq;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class ScalarFieldExecutionEngineTests
    {
        [Test]
        public void WhenArgumentIsFound()
        {
            var sut = new ScalarFieldExecutionEngine();

            var schema = CreateGraphQLSchema();

            var resolveFieldContext = new ResolveFieldContext
            {
                Source = new TestUser
                {
                    Name = "Dan"
                },
                Parent = new ResolveObjectContext
                {
                    ObjectType = schema.Query
                },
                FieldName = "name",
                ScalarType = (GraphQLScalar)schema.Query.Fields[1]
            };

            var result = sut.Execute(resolveFieldContext);

            Assert.AreEqual("name", result.Key);
            Assert.AreEqual("Dan", result.Value);
        }

        private static GraphQLSchema CreateGraphQLSchema(IGraphQLFieldType[] arguments = null)
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObject
                {
                    Name = "user",
                    Arguments = arguments,
                    Resolve = context => Data.GetData().Where(x => !context.Arguments.ContainsKey("id") || x.Id == Convert.ToInt32(context.Arguments["id"])),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLScalar
                        {
                            Name = "id",
                            Resolve = context => ((TestUser) context.Source).Id.ToString()
                        },
                        new GraphQLScalar
                        {
                            Name = "name",
                            Resolve = context => ((TestUser) context.Source).Name
                        }
                    }.ToList()
                }
            };
            return schema;
        }
    }
}