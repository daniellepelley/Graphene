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
            return new SchemaBuilder()
                .WithType<TestUser>(type => type
                    .Name("User")
                    .Description("This is a User, which has a Boss")
                    .WithScalarField<TestUser, int>(field => field
                        .Name("id")
                        .Type("Int")
                        .Resolve(context => context.Source.Id))
                    .WithScalarField<TestUser, string>(field => field
                        .Name("name")
                        .Type("String")
                        .Resolve(context => context.Source.Name))
                    .WithObjectField<TestUser, TestUser>(field => field
                        .Name("boss")
                        .Type("User")
                        .Resolve(context => context.Source.Boss)))

                .WithType<TestUser>(type => type
                    .Name("User")
                    .Description("This is a Cusomter, which also has an age and a friend")
                    .WithScalarField<TestUser, int>(field => field
                        .Name("id")
                        .Type("Int")
                        .Resolve(context => context.Source.Id))
                    .WithScalarField<TestUser, string>(field => field
                        .Name("name")
                        .Type("String")
                        .Resolve(context => context.Source.Name))
                    .WithObjectField<TestUser, int>(field => field
                        .Name("age")
                        .Type("User")
                        .Resolve(context => context.Source.Age))
                    .WithObjectField<TestUser, TestUser>(field => field
                        .Name("friend")
                        .Type("User")
                        .Resolve(context => context.Source.Boss)))

                .WithField<TestUser>(x =>
                    x.Name("user")
                        .Type("User")
                        .Arguments(new GraphQLArgument { Name = "id", Type = new GraphQLString() })
                        .Resolve(_ => Data.GetData().First()))
                
                 .WithField<TestUser>(x =>
                    x.Name("customer")
                        .Type("Customer")
                        .Arguments(new GraphQLArgument { Name = "id", Type = new GraphQLString() })
                        .Resolve(_ => Data.GetData().First()))
                .Build();
        }


        private static GraphQLSchema CreateIntrospectionSchema()
        {
            return new SchemaBuilder()
                .WithField<GraphQLSchema>(x =>
                    x.Name("__schema")
                        .Type("__Schema")
                        .Resolve(context => CreateSchema()))
                .Build();
        }

    }
}