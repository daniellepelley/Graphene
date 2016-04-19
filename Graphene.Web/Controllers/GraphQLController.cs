using System.Linq;
using System.Web.Http;
using Graphene.Core.FieldTypes;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;
using Graphene.Execution;
using Graphene.Owin.Spike;
using Graphene.Spike;
using Newtonsoft.Json;

namespace GraphQL.GraphiQL.Controllers
{
    public class GraphQLController : ApiController
    {
        private static TypeList _typeList = new TypeList();

        [HttpGet]
        public object Get(string query)
        {
            var engine = new ExecutionEngine();

            if (query.Contains("IntrospectionQuery"))
            {
                var document = new DocumentParser().Parse("{" + query + "}");
                return engine.Execute(CreateIntrospectionSchema(), document);
            }
            else
            {
                var document = new DocumentParser().Parse(query);
                return engine.Execute(CreateSchema(), document);
            }
        }

        public object Post(GraphQLQuery query)
        {
            var engine = new ExecutionEngine();

            if (query.Query.Contains("IntrospectionQuery"))
            {
                var document = new DocumentParser().Parse("{" + query.Query + "}");
                return engine.Execute(CreateIntrospectionSchema(), document);
            }
            else
            {
                var document = new DocumentParser().Parse(query.Query);
                return engine.Execute(CreateSchema(), document);
            }
        }

        private static GraphQLSchema CreateSchema()
        {


            var userType = new GraphQLObjectType
            {
                Name = "User",
                Description = "This is GraphQL and it is very cool, when you get the damn thing working",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<TestUser, int>
                    {
                        Name = "id",
                        Type = new ChainType(_typeList, "Int"),
                        Resolve = context => context.Source.Id
                    },
                    new GraphQLScalarField<TestUser, string>
                    {
                        Name = "Name",
                        Type = new ChainType(_typeList, "String"),
                        Resolve = context => context.Source.Name
                    }
                }
            };

            var customerType = new GraphQLObjectType
            {
                Name = "Customer",
                Description = "This is a customer type",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<TestUser, int>
                    {
                        Name = "id",
                        Type = new ChainType(_typeList, "Int"),
                        Resolve = context => context.Source.Id
                    },
                    new GraphQLScalarField<TestUser, string>
                    {
                        Name = "name",
                        Type = new ChainType(_typeList, "String"),
                        Resolve = context => context.Source.Name
                    },
                    new GraphQLScalarField<TestUser, int>
                    {
                        Name = "age",
                        Type = new ChainType(_typeList, "Int"),
                        Resolve = context => context.Source.Age
                    }
                }
            };

            return new SchemaBuilder()
                .WithField<TestUser>(x =>
                    x.Name("user")
                        .Type(userType)
                        .Arguments(new GraphQLArgument { Name = "id", Type = new GraphQLString() })
                        .Resolve(_ => Data.GetData().First()))
                .WithField<TestUser>(x =>
                    x.Name("customer")
                        .Type(customerType)
                        .Arguments(new GraphQLArgument { Name = "id", Type = new GraphQLString() })
                        .Resolve(_ => Data.GetData().First()))
                .Build();
        }


        private static GraphQLSchema CreateIntrospectionSchema()
        {
            return new SchemaBuilder(_typeList)
                .WithField<GraphQLSchema>(x =>
                    x.Name("__schema")
                        .Type("__Schema")
                        .Resolve(context => CreateSchema()))
                .Build();

            //return new GraphQLSchema
            //{
            //    Query = new GraphQLObjectField
            //    {
            //        Name = "IntrospectionQuery",
            //        GraphQLObjectType = () => new GraphQLObjectType
            //        {
            //            Fields = new IGraphQLFieldType[]
            //            {
            //                new GraphQLObjectField<GraphQLSchema>
            //                {
            //                    Name = "__schema",
            //                    GraphQLObjectType = () => new __Schema(),
            //                    Resolve = _ => CreateSchema()
            //                }
            //            }
            //        },
            //        Resolve = _ => CreateSchema()
            //    }
            //};
        }

    }
}