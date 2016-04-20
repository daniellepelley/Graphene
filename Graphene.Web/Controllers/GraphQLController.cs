//using System.Web.Http;
//using Graphene.Core.Parsers;
//using Graphene.Execution;
//using Graphene.Web.Models;

//namespace Graphene.Web.Controllers
//{
//    public class GraphQLController : ApiController
//    {
//        [HttpGet]
//        public object Get(string query)
//        {
//            var engine = new ExecutionEngine();

//            if (query.Contains("IntrospectionQuery"))
//            {
//                var document = new DocumentParser().Parse("{" + query + "}");
//                return engine.Execute(CreateIntrospectionSchema(), document);
//            }
//            else
//            {
//                var document = new DocumentParser().Parse(query);
//                return engine.Execute(CreateSchema(), document);
//            }
//        }

//        public object Post(GraphQLQuery query)
//        {
//            var engine = new ExecutionEngine();

//            if (query.Query.Contains("IntrospectionQuery"))
//            {
//                var document = new DocumentParser().Parse("{" + query.Query + "}");
//                return engine.Execute(ExampleSchemas.CreateIntrospectionSchema(), document);
//            }
//            else
//            {
//                var document = new DocumentParser().Parse(query.Query);
//                return engine.Execute(ExampleSchemas.CreateSchema(), document);
//            }
//        }


//    }
//}