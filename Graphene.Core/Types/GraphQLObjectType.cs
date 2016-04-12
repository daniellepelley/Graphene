using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types
{
    public class GraphQLObjectType : IGraphObjectType, IGraphQLFieldType
    {
        public IGraphQLFieldType this[string name]
        {
            get { return Fields.FirstOrDefault(x => x.Name == name); }
        }

        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType OfType { get; set; }
        public virtual object ResolveToObject(ResolveObjectContext context)
        {
            return Resolve(context);
        }

        public List<IGraphQLFieldType> Fields { get; set; }
        public virtual Func<ResolveObjectContext, object> Resolve { get; set; }
        public IGraphQLFieldType[] Arguments { get; set; }
    }
}