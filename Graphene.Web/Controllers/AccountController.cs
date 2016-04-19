using System.Threading.Tasks;
using Graphene.Core.Types.Introspection;

namespace GraphQL.GraphiQL.Controllers
{
    public class GraphQLQuery
    {
        public string Query { get; set; }
        public string Variables { get; set; }
    }
}
