using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core
{
    public class ResolveFieldContext<T> : ResolveFieldContext, IResolveContext<T>
    {
        public T Source { get; set; }
    }

    public class ResolveFieldContext : IResolveContext
    {
        public string FieldName { get; set; }
        public GraphQLScalarField<object, GraphQLScalarBase> ScalarFieldType { get; set; }
        public ResolveObjectContext Parent { get; set; }

        public IDictionary<string, object> Arguments { get; set; }

        //public object RootValue { get; set; }

        //public GraphQLSchema Schema { get; set; }
        //public Operation Operation { get; set; }

        //public TType Argument<TType>(string name)
        //{
        //    if (Arguments.ContainsKey(name))
        //    {
        //        return (TType)Arguments[name];
        //    }

        //    return default(TType);
        //}
    }

    public interface IResolveContext
    {
        string FieldName { get; set; }

        //ResolveObjectContext Parent { get; set; }

        IDictionary<string, object> Arguments { get; set; }

        //GraphQLSchema Schema { get; set; }

        //Operation Operation { get; set; }
    }

}