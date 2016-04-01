namespace Graphene.Core
{
    public interface IGraphQLQueryHandler
    {
        string Handle(string query);
    }
}
