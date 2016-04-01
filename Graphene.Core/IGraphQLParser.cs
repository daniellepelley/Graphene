namespace Graphene.Core
{
    public interface IGraphQLParser
    {
        QueryObject Parse(string query);
    }
}