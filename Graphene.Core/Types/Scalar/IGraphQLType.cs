using System.Collections.Generic;
using Graphene.Core.Constants;
using Graphene.Core.FieldTypes;

namespace Graphene.Core.Types.Scalar
{
    public interface IGraphQLType
    {
        string Kind { get; }
        string Name { get; }
        string Description { get; }
    }

    public interface IGraphQLChainType : IGraphQLType
    {
        IGraphQLType OfType { get; }
        IEnumerable<IGraphQLFieldType> Fields { get; }
    }

    public class GraphQLList : IGraphQLType
    {
        public string Kind
        {
            get { return GraphQLKinds.List; }
        }

        public string Name
        {
            get { return GraphQLTypes.List; }
        }

        public string Description
        {
            get { return "Describes a list"; }
        }
    }
}