using System;
using System.Collections.Generic;

namespace Graphene.Core.Types.Introspection
{
    [Obsolete("Should be phased out")]
    public class GraphQLSchemaFieldType : IGraphQLFieldType
    {
        public string Name { get; set; }
        public string Kind { get { return "TO DELETE"; } }
        public string Description { get; set; }
        string[] IGraphQLFieldType.OfType { get; set; }

        public IGraphQLFieldType this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IGraphQLArgument> Arguments { get; set; }

        public object ResolveToObject(ResolveObjectContext context)
        {
            return null;
        }

        public Type OfType { get; set; }
        public Func<GraphQLSchema, string> Resolve { get; set; }
        public string Args { get; set; }
    }
}