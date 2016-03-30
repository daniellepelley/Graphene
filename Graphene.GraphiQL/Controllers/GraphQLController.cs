using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;

namespace Graphene.GraphiQL.Controllers
{
    public class GraphQLController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public object Index(GraphQLQuery query)
        {
            var path = @"C:\Users\Danny\Source\Repos\Graphene\Graphene.Test\StarWarsData.json";
            var result = Request.CreateResponse(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return result;
        }
    }

    //public class ExecutionResult
    //{
    //    public object Data { get; set; }
    //}

    public class GraphQLQuery
    {
        public string Query { get; set; }
        public string Variables { get; set; }
    }
}
