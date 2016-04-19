using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.FieldTypes
{
    public class GraphQLObjectFieldBase : IGraphQLFieldType
    {
        public IGraphQLFieldType this[string name]
        {
            get
            {
                //TODO: Seperate out this into different class

                var type = Type;

                var chainType = type as ChainType;

                while(type != null)
                {
                    if (chainType != null)
                    {
                        type = chainType.GetCurrentType();
                        chainType = (ChainType)chainType.OfType;
                    }

                    if (type is GraphQLObjectType)
                    {
                        return ((GraphQLObjectType)type).Fields.FirstOrDefault(x => x.Name == name);
                    }

                    type = type.OfType;
                }
                throw new GraphQLException("Field {0} not found on {1} {2}", name, Type.Name, Type.Kind);
            }
        }

        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IGraphQLArgument> Arguments { get; set; }
        public IGraphQLType Type { get; set; }
        public bool IsDeprecated { get; set; }
        public string DeprecationReason { get; set; }

        public GraphQLObjectFieldBase()
        {
            Arguments = new IGraphQLArgument[0];
        }
    }
}