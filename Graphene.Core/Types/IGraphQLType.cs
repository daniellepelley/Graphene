namespace Graphene.Core.Types
{
    public interface IGraphQLType
    {
        string Kind { get; }
        string Name { get; }
        string Description { get; }
    }

    public interface IGraphQLObject : IGraphQLType
    {
        IGraphQLFieldType this[string name] { get; }
    }
}