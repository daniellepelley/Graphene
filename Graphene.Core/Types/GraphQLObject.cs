using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types
{
    public class GraphQLObject : GraphQLObject<ResolveObjectContext, object>
    {

    }

    public class GraphQLObject<TInput, TOutput> : IGraphQLObject, IGraphQLFieldType
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
        public string[] OfType { get; set; }

        public List<IGraphQLFieldType> Fields { get; set; }
        public virtual Func<TInput, TOutput> Resolve { get; set; }
        public IGraphQLFieldType[] Arguments { get; set; }
    }
}