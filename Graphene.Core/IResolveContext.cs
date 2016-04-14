namespace Graphene.Core
{
    public interface IResolveContext<T> : IResolveContext
    {
        T Source { get; set; }
    }
}