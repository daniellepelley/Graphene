using System;
using System.Collections.Generic;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public abstract class GraphQLFieldType : IGraphQLFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType OfType { get; set; }

        public abstract object ResolveToObject(ResolveFieldContext context);
    }

    public interface IGraphQLFieldType
    {
        string Name { get; set; }
        string Description { get; set; }
        IGraphQLType OfType { get; set; }
        object ResolveToObject(ResolveFieldContext context);
    }

    public class GraphQLFieldType<TOutput> : GraphQLFieldType
    {
        public virtual Func<ResolveFieldContext, TOutput> Resolve { get; set; }
        public override object ResolveToObject(ResolveFieldContext context)
        {
            return Resolve(context);
        }
    }


}