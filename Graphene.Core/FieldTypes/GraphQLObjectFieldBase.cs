using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Constants;
using Graphene.Core.Exceptions;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.FieldTypes
{
    public class GraphQLObjectFieldBase : IGraphQLFieldType
    {
        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IGraphQLArgument> Arguments { get; set; }
        public string[] Type { get; set; }
        public bool IsDeprecated { get; set; }
        public string DeprecationReason { get; set; }

        protected GraphQLObjectFieldBase()
        {
            Arguments = new IGraphQLArgument[0];
        }
    }
}