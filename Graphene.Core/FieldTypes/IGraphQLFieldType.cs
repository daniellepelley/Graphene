using System.Collections.Generic;
using Graphene.Core.Types.Introspection;

namespace Graphene.Core.Types
{
    public interface IGraphQLFieldType
    {
        string Name { get; set; }
        string Kind { get; }
        string Description { get; set; }
        string[] OfType { get; set; }
        IGraphQLFieldType this[string name] { get; }
        IEnumerable<IGraphQLArgument> Arguments { get; set; } 
    }
}