using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using Graphene.Core;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
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
            _schema = CreateSchema();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);

            if (owinContext.Request.Path.Value == "/graphqlspike")
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
                else if (owinContext.Request.Method == "POST")
                {
                    var query = new StreamReader(owinContext.Request.Body).ReadToEnd();
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
            var result = _executionEngine.Execute(_schema, document);

            var json = JsonConvert.SerializeObject(result);

            owinContext.Response.Headers.Set("Content-Type", "application/json");
            await owinContext.Response.WriteAsync(json);
        }

        private GraphQLSchema CreateSchema()
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObject
                {
                    Name = "user",
                    Resolve =
                        context =>
                            Data.GetData()
                                .Where(
                                    x =>
                                        (!context.Arguments.ContainsKey("id") ||
                                         x.Id.ToString() == context.Arguments["id"].ToString()) &&
                                        (!context.Arguments.ContainsKey("name") ||
                                         x.Name.Contains(context.Arguments["name"].ToString()))),
                    OfType = new[] {"user"},
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLScalar<TestUser, int>
                        {
                            Name = "Id",
                            Resolve = context => context.Source.Id
                        },
                        new GraphQLScalar<TestUser, string>
                        {
                            Name = "Name",
                            Resolve = context => context.Source.Name
                        },
                        new GraphQLObject<TestUser, TestUser>
                        {
                            Name = "Boss",
                            Resolve = context => context.Source.Boss,
                            Fields = new IGraphQLFieldType[]
                            {
                                new GraphQLScalar<TestUser, int>
                                {
                                    Name = "Id",
                                    Resolve = context => context.Source.Id
                                },
                                new GraphQLScalar<TestUser, string>
                                {
                                    Name = "Name",
                                    Resolve = context => context.Source.Name
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