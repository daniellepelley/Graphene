using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types
{
    public class GraphQLObjectType : IGraphQLType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] OfType { get; set; }

        public IEnumerable<IGraphQLFieldType> Fields { get; set; }

        public GraphQLObjectType()
        {
            Fields = new IGraphQLFieldType[0];
        }

        public bool HasField(string name)
        {
            return Fields.Count(x => x.Name == name) == 1;
        }
    }
}