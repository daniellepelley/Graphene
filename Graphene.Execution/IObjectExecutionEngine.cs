using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public interface IObjectExecutionEngine
    {
        object Execute(Selection[] selections, GraphQLObjectType graphQLObject, ResolveFieldContext objectContext);
    }
}