using System.Collections.Generic;
using System.Linq;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Object
{
    public class GraphQLObjectType : IGraphQLType
    {
        public IGraphQLFieldType this[string fieldName]
        {
            get { return Fields.FirstOrDefault(x => x.Name == fieldName); }
        }

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

        public bool HasField(string name)
        {
            return Fields.Count(x => x.Name == name) == 1;
        }
    }
}