namespace Graphene.Core.Types
{
    public static class GraphQLKinds
    {
        public static string Scalar = "SCALAR";
    }

    public class GraphQLScalar
    {
        public string Kind
        {
            get { return GraphQLKinds.Scalar; }
        }
    }

    public interface IGraphQLType
    {
        string Kind { get; }
        string Name { get; }
        string Description { get; }
    }

    public class GraphQLString : GraphQLScalar, IGraphQLType
    {
        public string Name
        {
            get { return "String"; }
        }

        public string Description
        {
            get { return "This is a string"; }
        }
    }

    public class GraphQLBoolean : GraphQLScalar, IGraphQLType
    {
        public string Name
        {
            get { return "Boolean"; }
        }

        public string Description
        {
            get { return "This is a boolean"; }
        }
    }

    public class GraphQLObjectType
    {
        public string Name { get; set; }
        public GraphQLFieldType[] Fields { get; set; }
    }

    public class GraphQLFieldType
    {
        public string Name { get; set; }
        public GraphQLFieldType OfType { get; set; }
        public string Kind { get; set; }
    }

    public class GraphQLSchema
    {
        public GraphQLObjectType Query { get; set; }
    }
}
