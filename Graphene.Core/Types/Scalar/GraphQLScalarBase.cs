namespace Graphene.Core.Types
{
    public abstract class GraphQLScalarBase
    {
        public string Kind
        {
            get { return GraphQLKinds.Scalar; }
        }

        public string[] OfType { get; set; }
    }

    public class GraphQLEnum : IGraphQLType
    {
        public string Kind
        {
            get { return GraphQLKinds.Enum; }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public string[] OfType { get; set; }
    }

    public class GraphQLEnum<T> : GraphQLEnum
    {
        public T[] Values { get; set; }
    }

    public class GraphQLScalarBase<T> : GraphQLScalarBase
    {
        
    }
}
