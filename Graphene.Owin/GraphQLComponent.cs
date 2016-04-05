using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Configuration;
using Graphene.Core;
using Graphene.Core.Parsers;
using Graphene.Owin.Spike;
using Graphene.Spike;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace Graphene.Owin
{
    public class GraphQLComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _appFunc;

        public GraphQLComponent(Func<IDictionary<string, object>, Task> appFunc)
        {
            _appFunc = appFunc;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);

            if (owinContext.Request.Path.Value == "/graphql")
            {
                if (owinContext.Request.Method == "GET")
                {
                    var query = owinContext.Request.Query["query"];
                    var document = new DocumentParser().Parse(query);
                    var json = new SpikeExecutionEngine<TestUser>(Data.GetData()).Execute(document);
                    await owinContext.Response.WriteAsync(json);
                }
                else if (owinContext.Request.Method == "POST")
                {
                    var query = new StreamReader(owinContext.Request.Body).ReadToEnd();
                    var document = new DocumentParser().Parse(query);
                    var json = new SpikeExecutionEngine<TestUser>(Data.GetData()).Execute(document);
                    await owinContext.Response.WriteAsync(json);
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
    }
}