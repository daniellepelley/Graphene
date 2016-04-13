using System;
using Graphene.Core.FieldTypes;

namespace Graphene.Core.Types.Introspection
{
    public class GraphQLSchemaFieldType : IGraphQLFieldType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        string[] IGraphQLFieldType.OfType { get; set; }

        public IGraphQLFieldType this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object ResolveToObject(ResolveObjectContext context)
        {
            return Resolve(context.Schema);
        }

        public Type OfType { get; set; }
        public Func<GraphQLSchema, string> Resolve { get; set; }
        public string Args { get; set; }
    }
}