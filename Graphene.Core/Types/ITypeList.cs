using System.Collections.Generic;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public interface ITypeList : IEnumerable<IGraphQLType>
    {
        IGraphQLType LookUpType(string typeName);
        void AddType(string typeName, IGraphQLType type);
    }
}