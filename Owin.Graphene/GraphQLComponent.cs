using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace Owin.Graphene
{
    public class GraphQLComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _appFunc;
        private readonly GraphQLSchema _schema;

        private readonly IExecutionEngine _executionEngine;

        public GraphQLComponent(Func<IDictionary<string, object>, Task> appFunc, GraphQLSchema schema)
        {
            _appFunc = appFunc;
            _schema = schema;
            _executionEngine = new ExecutionEngine();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);

            if (IsGraphQLRoute(owinContext.Request))
            {
                var query = GetQueryString(owinContext.Request);
                await ProcessQuery(query, owinContext);
            }
            else
            {
                await _appFunc(environment);
            }
        }

        private bool IsGraphQLRoute(IOwinRequest request)
        {
            return request != null &&
                   !string.IsNullOrEmpty(request.Path.Value) &&
                   request.Path.Value.ToLower() == "/graphql";
        }

        private string GetQueryString(IOwinRequest request)
        {
            if (request.Method == "GET")
            {
                return request.Query["query"];
            }
            
            if (request.Method == "POST")
            {
                var query = new StreamReader(request.Body).ReadToEnd();
                query = query
                    .Replace(((char) 9).ToString(), string.Empty)
                    .Replace(((char) 10).ToString(), string.Empty)
                    .Replace(((char) 13).ToString(), string.Empty)
                    .Replace(@"/n", string.Empty)
                    .Replace(@"\n", string.Empty);

                var qo = JsonConvert.DeserializeObject<QueryObject>(query);
                return qo.Query;
            }
            return null;
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
    }
}