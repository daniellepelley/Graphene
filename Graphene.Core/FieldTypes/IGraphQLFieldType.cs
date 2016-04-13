using Graphene.Core.Types;

namespace Graphene.Core.FieldTypes
{
    public interface IGraphQLFieldType
    {
        string Name { get; set; }
        string Description { get; set; }
        string[] OfType { get; set; }
        IGraphQLFieldType this[string name] { get; }
    }
}