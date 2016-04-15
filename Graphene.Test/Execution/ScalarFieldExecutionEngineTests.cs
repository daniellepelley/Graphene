using System;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Spike;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    //public class ScalarFieldExecutionEngineTests
    //{
    //    [Test]
    //    public void WhenArgumentIsFound()
    //    {
    //        var sut = new ScalarFieldExecutionEngine();

    //        var schema = CreateGraphQLSchema();

    //        var resolveFieldContext = new ResolveFieldContext<object>
    //        {
    //            Source = new User
    //            {
    //                Name = "Dan"
    //            },
    //            Parent = new ResolveObjectContext<object>
    //            {
    //                ObjectFieldType = schema.Query
    //            },
    //            FieldName = "name",
    //            ScalarFieldType = (GraphQLScalarField)schema.Query.Fields[1]
    //        };

    //        var result = sut.Execute(resolveFieldContext);

    //        Assert.AreEqual("name", result.Key);
    //        Assert.AreEqual("Dan", result.Value);
    //    }

    //    private static GraphQLSchema CreateGraphQLSchema(IGraphQLFieldType[] arguments = null)
    //    {
    //        var schema = new GraphQLSchema
    //        {
    //            Query = new GraphQLObjectField
    //            {
    //                Name = "user",
    //                Arguments = arguments,
    //                Resolve = context => Data.GetData().Where(x => !context.Arguments.ContainsKey("id") || x.Id == Convert.ToInt32(context.Arguments["id"])),
    //                Fields = new IGraphQLFieldType[]
    //                {
    //                    new GraphQLScalarField
    //                    {
    //                        Name = "id",
    //                        Resolve = context => ((User) context.Source).Id.ToString()
    //                    },
    //                    new GraphQLScalarField
    //                    {
    //                        Name = "name",
    //                        Resolve = context => ((User) context.Source).Name
    //                    }
    //                }.ToList()
    //            }
    //        };
    //        return schema;
    //    }
    //}
}