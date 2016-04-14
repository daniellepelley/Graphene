using System.Collections.Generic;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core
{
    public class ResolveObjectContext<T> : ResolveObjectContext, IResolveContext<T>
    {
        public T Source { get; set; }
        //public ResolveObjectContext<T> Clone()
        //{
        //    var context = (ResolveObjectContext<T>)this.MemberwiseClone();
        //    context.Parent = this;
        //    return context;
        //}
    }

    public class ResolveObjectContext : IResolveContext
    {
        public string FieldName { get; set; }

        //public Field FieldAst { get; set; }

        //public ResolveObjectContext Parent { get; set; }

        public GraphQLObject ObjectType { get; set; }

        //public Selection[] Selections { get; set; }

        public IDictionary<string, object> Arguments { get; set; }

        //public GraphQLSchema Schema { get; set; }

        //public Operation Operation { get; set; }
        //public Selection Selection { get; set; }

        //public TType Argument<TType>(string name)
        //{
        //    if (Arguments.ContainsKey(name))
        //    {
        //        return (TType)Arguments[name];
        //    }

        //    return default(TType);
        //}
    }
}