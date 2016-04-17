using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Graphene.Owin.Spike;
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
                return JsonConvert.SerializeObject(engine.Execute(CreateIntrospectionSchema(), document));
            }
            else
            {
                var document = new DocumentParser().Parse(query.Query);
                return JsonConvert.SerializeObject(engine.Execute(CreateSchema(), document));
            }
        }

        private static GraphQLSchema CreateSchema()
        {

            var userType = new GraphQLObjectType
            {
                Name = "User",
                Fields = new IGraphQLFieldType[]
                        {
                            new GraphQLScalarField<TestUser, int>
                        {
                            Name = "Id",
                            Resolve = context => context.Source.Id
                        },
                        new GraphQLScalarField<TestUser, string>
                        {
                            Name = "Name",
                            Resolve = context => context.Source.Name
                        }
                    }
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectField
                {
                    Name = "Query",
                    GraphQLObjectType = () => new GraphQLObjectType
                    {
                        Name = "Query",
                        Fields = new IGraphQLFieldType[]
                        {
                            new GraphQLObjectField<TestUser>
                            {
                                Name = "user",
                                Arguments = new []
                                {
                                    new GraphQLArgument { Name = "id", Type = new GraphQLString() }
                                },
                                Resolve = _ => Data.GetData().First() ,      
                                OfType = new[] {"user"},
                                GraphQLObjectType = () => userType
                            }
                        }
                    },
                    Resolve = _ => string.Empty
                }
            };


            schema.Types = new IGraphQLType[]
            {
                schema.Query.GraphQLObjectType(),
                new GraphQLString(),
                userType,
                new __Schema(), 
                new __Type(),
                new __TypeKind(),
                new GraphQLBoolean(),
                new __Field(),
                new __InputValue(),
                new __EnumValue(),
                new __Directive()
            };


            return schema;
        }


        private static GraphQLSchema CreateIntrospectionSchema()
        {
            return new GraphQLSchema
            {
                Query = new GraphQLObjectField
                {
                    Name = "IntrospectionQuery",
                    GraphQLObjectType = () => new GraphQLObjectType
                    {
                        Fields = new IGraphQLFieldType[]
                        {
                            new GraphQLObjectField<GraphQLSchema>
                            {
                                Name = "__schema",
                                GraphQLObjectType = () => new __Schema(),
                                Resolve = _ => CreateSchema()
                            }
                        }
                    },
                    Resolve = _ => CreateSchema()
                }
            };
        }

    }

    public class GraphQLQuery
    {
        public string Query { get; set; }
        public string Variables { get; set; }
    }
}
