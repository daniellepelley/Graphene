using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Types.Introspection;

namespace Graphene.Core.Types
{
    public class GraphQLObjectFieldBase
    {
        private GraphQLObjectType _type = new GraphQLObjectType();

        public IGraphQLFieldType this[string name]
        {
            get { return GraphQLObjectType().Fields.FirstOrDefault(x => x.Name == name); }
        }

        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public Func<GraphQLObjectType> GraphQLObjectType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string[] OfType { get; set; }
        public IEnumerable<IGraphQLArgument> Arguments { get; set; } 

        public GraphQLObjectFieldBase()
        {
            GraphQLObjectType = () => _type;
            Arguments = new IGraphQLArgument[0];
        }
    }
}