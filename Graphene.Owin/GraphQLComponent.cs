using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Graphene.Core;
using Microsoft.Owin;

namespace Graphene.Owin
{
    public class GraphQLComponent
    {
        private readonly Func<IDictionary<string, object>, Task> _appFunc;
        private readonly IGraphQLQueryHandler _graphQLQueryHandler;

        public GraphQLComponent(Func<IDictionary<string, object>, Task> appFunc)
        {
            _appFunc = appFunc;
            _graphQLQueryHandler = new GraphQLQueryHandler();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);

            if (owinContext.Request.Path.Value == "/graphql")
            {
                var query = owinContext.Request.Query["query"];
                var data = _graphQLQueryHandler.Handle(query);
                await owinContext.Response.WriteAsync(data);
            }
            else
            {
                await _appFunc(environment);   
            }
        }
    }
}