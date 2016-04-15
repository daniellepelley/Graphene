namespace Graphene.Core.Types
{
    public interface IGraphQLType
    {
        string Kind { get; }
        string Name { get; }
        string Description { get; }
        string[] OfType { get; }
    }
}