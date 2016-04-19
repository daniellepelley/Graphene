using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Types.Scalar;

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

        public Argument[] Arguments { get; set; }
    }
}