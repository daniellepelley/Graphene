namespace Graphene.Core.Types.Scalar
{
    public class GraphQLString : GraphQLScalarBase, IGraphQLType
    {
        public string Name
        {
            get { return "String"; }
        }

        public string Description
        {
            get { return "The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text."; }
        }
    }
}

