using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class ArgumentsExecutionEngineTests
    {
        [Test]
        public void WhenArgumentIsFound()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = @"{user(id:""1"") {id, name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected =
                @"{""data"":{""user"":{""id"":1,""name"":""Dan_Smith""}}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        private static object Execute(ExecutionEngine sut, GraphQLSchema schema, Document document)
        {
            return JsonConvert.SerializeObject(sut.Execute(schema, document));
        }

        private static GraphQLSchema CreateGraphQLSchema(IEnumerable<IGraphQLArgument> arguments = null)
        {
            return new SchemaBuilder()
                .WithArguments(arguments)
                .WithResolve(Resolve)
                .Build();
        }

        private static User Resolve(ResolveObjectContext context)
        {
            return Data.Data.GetData().FirstOrDefault(x => context.Arguments.All(arg => arg.Name != "id") || x.Id == Convert.ToInt32(context.Arguments.First(arg => arg.Name == "id").Value));
        }
    }
}