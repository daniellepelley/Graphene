namespace Graphene.Core.Types
{
    public interface IGraphQLObject : IGraphQLType
    {
        IGraphQLFieldType this[string name] { get; }
    }
}