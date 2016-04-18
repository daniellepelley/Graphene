using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types
{
    public class GraphQLNonNull : IGraphQLType
    {
        private readonly IGraphQLType _graphQLType;

        public GraphQLNonNull(IGraphQLType graphQLType)
        {
            _graphQLType = graphQLType;
        }

        public string Kind
        {
            get { return GraphQLKinds.NonNull; }
        }

        public string Name
        {
            get { return null; }
        }

        public string Description
        {
            get { return "This is a NonNull"; }
        }

        public IGraphQLType OfType
        {
            get { return _graphQLType; }

        }
    }
}