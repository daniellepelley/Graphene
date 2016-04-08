using System;
using System.Linq;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Spike;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class NestedExecutionEngineTests
    {
        [Test]
        public void RunsExecute()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(Id: 1) {Id, Name, Boss {Id, Name}}}";
            var document = new DocumentParser().Parse(query);

            var expected =
                @"[{""Id"":1,""Name"":""Dan_Smith"",""Boss"":{""Id"":4,""Name"":""Boss_Smith""}}]";
            var result = sut.Execute(schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute2()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(Id: 1) {Name, Boss {Name}}}";
            var document = new DocumentParser().Parse(query);

            var expected =
                @"[{""Name"":""Dan_Smith"",""Boss"":{""Name"":""Boss_Smith""}}]";
            var result = sut.Execute(schema, document);
            Assert.AreEqual(expected, result);
        }

        private static GraphQLSchema CreateGraphQLSchema()
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "user",
                    Resolve = context => Data.GetData().Where(x => !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"])),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldType<int>
                        {
                            Name = "Id",
                            Resolve = context => ((TestUser) context.Source).Id
                        },
                        new GraphQLFieldType<string>
                        {
                            Name = "Name",
                            Resolve = context => ((TestUser) context.Source).Name
                        },
                        new GraphQLObjectType
                        {
                            Name = "Boss",
                            Resolve = context => ((TestUser) context.Source).Boss,
                            Fields = new IGraphQLFieldType[]
                            {
                                new GraphQLFieldType<int>
                                {
                                    Name = "Id",
                                    Resolve = context => ((TestUser) context.Source).Id
                                },
                                new GraphQLFieldType<string>
                                {
                                    Name = "Name",
                                    Resolve = context => ((TestUser) context.Source).Name
                                }
                            }.ToList()
                        }
                    }.ToList()
                }
            };
            return schema;
        }
    }
}