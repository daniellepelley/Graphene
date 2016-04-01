namespace Graphene.Core
{
    public class GraphQLQueryHandler : IGraphQLQueryHandler
    {
        public string Handle(string query)
        {
            return @"{""data"": {""user"": null}}";
        }
    }
}