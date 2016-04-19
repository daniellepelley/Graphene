using System.Collections.Generic;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Object
{
    public class GraphQLObjectType : IGraphQLType
    {
        public string Kind
        {
            get { return "OBJECT"; }
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType OfType { get; set; }

        public IEnumerable<IGraphQLFieldType> Fields { get; set; }

        public GraphQLObjectType()
        {
            Fields = new IGraphQLFieldType[0];
        }
    }
}