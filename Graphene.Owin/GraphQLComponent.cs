using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using Graphene.Core;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;
using Graphene.Execution;
using Graphene.Owin.Spike;
using Graphene.Spike;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace Graphene.Owin
{
    public class GraphQLComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _appFunc;
        private readonly GraphQLSchema _schema;
        private IExecutionEngine _executionEngine;

        public GraphQLComponent(Func<IDictionary<string, object>, Task> appFunc)
        {
            _appFunc = appFunc;
            _schema = CreateIntrospectionSchema();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);

            if (owinContext.Request.Method == "POST")
            {
                var query = new StreamReader(owinContext.Request.Body).ReadToEnd();
                query = query
                    .Replace(((char)9).ToString(), string.Empty)
                    .Replace(((char)10).ToString(), string.Empty)
                    .Replace(((char)13).ToString(), string.Empty)
                    .Replace(@"/n", string.Empty)
                    .Replace(@"\n", string.Empty);

                var qo = JsonConvert.DeserializeObject<QueryObject>(query);

                await ProcessQuery("{ " + qo.Query + " }", owinContext);
            }
            else if (owinContext.Request.Path.Value == "/graphqlspike")
            {
                _executionEngine = new SpikeExecutionEngine<TestUser>(Data.GetData());

                if (owinContext.Request.Method == "GET")
                {
                    var query = owinContext.Request.Query["query"];
                    await ProcessQuery(query, owinContext);
                }
                else if (owinContext.Request.Method == "POST")
                {
                    var query = new StreamReader(owinContext.Request.Body).ReadToEnd();
                    await ProcessQuery(query, owinContext);
                }
            }
            else if (owinContext.Request.Path.Value == "/graphql")
            {
                _executionEngine = new ExecutionEngine();
                if (owinContext.Request.Method == "GET")
                {
                    var query = owinContext.Request.Query["query"];
                    await ProcessQuery(query, owinContext);
                }
            }
            else if (owinContext.Request.Path.Value == "/graphdocument")
            {
                var query = owinContext.Request.Query["query"];
                var document = new DocumentParser().Parse(query);
                var json = JsonConvert.SerializeObject(document);
                await owinContext.Response.WriteAsync(json);
            }
            else
            {
                await _appFunc(environment);
            }
        }

        private async Task ProcessQuery(string query, OwinContext owinContext)
        {
            query = query.Replace(@"\n", " ");

            var document = new DocumentParser().Parse(query);

            var schema = query.Contains("IntrospectionQuery")
                ? CreateIntrospectionSchema()
                : CreateSchema();


            var result = _executionEngine.Execute(schema, document);

            var json = JsonConvert.SerializeObject(result);

            owinContext.Response.Headers.Set("Content-Type", "application/json");
            await owinContext.Response.WriteAsync(json);
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


            return new SchemaBuilder()
                .WithField(x =>
                    x.Name("user")
                    .Type(userType)
                    .Arguments(new GraphQLArgument { Name = "id", Type = new GraphQLString() })
                    .Resolve(_ => Data.GetData().First()))
                .Build();


            //var schema = new GraphQLSchema
            //{
            //    QueryType =  new GraphQLObjectType
            //        {
            //            Name = "Query",
            //            Fields = new IGraphQLFieldType[]
            //            {
            //                new GraphQLObjectField<TestUser>
            //                {
            //                    Name = "user",
            //                    Arguments = new []
            //                    {
            //                        new GraphQLArgument { Name = "id", Type = new GraphQLString() }
            //                    },
            //                    Resolve = _ => Data.GetData().First() ,      
            //                    Type = userType
            //                }
            //            }
            //    }
            //};


            //return schema;
        }


        private static GraphQLSchema CreateIntrospectionSchema()
        {
            return new SchemaBuilder()
                .WithField(x =>
                    x.Name("__schema")
                    .Type("__Schema")
                    .Resolve(context => CreateSchema()))
                .Build();

            //return new GraphQLSchema
            //{
            //    QueryType =  new GraphQLObjectType
            //        {
            //            Fields = new IGraphQLFieldType[]
            //            {
            //                new GraphQLObjectField<GraphQLSchema>
            //                {
            //                    Name = "__schema",
            //                    Type = new ChainType() __Schema(),
            //                    Resolve = _ => CreateSchema()
            //                }
            //            }   
            //    }
            //};
        }

    }
}