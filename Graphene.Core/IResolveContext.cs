using Graphene.Core.Model;

namespace Graphene.Core
{
    public interface IResolveContext
    {
        string FieldName { get; set; }
        Argument[] Arguments { get; set; }
    }

    public interface IResolveContext<T> : IResolveContext
    {
        T Source { get; set; }
    }
}