namespace Graphene.Core
{
    public interface IGraphQLParser
    {
        object Parse(string query);
    }
}