using System;
using System.Collections.Generic;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public class GraphQLFieldObjectType : IGraphQLFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType OfType { get; set; }

        public virtual Func<IResolveObjectContext, object> Resolve { get; set; }
    }

    public class GraphQLFieldScalarType : IGraphQLFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType OfType { get; set; }

        public virtual Func<ResolveFieldContext, object> Resolve { get; set; }
    }
    
    public interface IGraphQLFieldType
    {
        string Name { get; set; }
        string Description { get; set; }
        IGraphQLType OfType { get; set; }
    }

    //public class GraphQLFieldScalarType<TOutput> : GraphQLFieldObjectType
    //{

    //    public override string ResolveToObject(IResolveObjectContext context)
    //    {
    //        return Resolve(context);
    //    }
    //}


}