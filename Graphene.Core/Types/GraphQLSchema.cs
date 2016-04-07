namespace Graphene.Core.Types
{
    public class GraphQLSchema : IGraphQLSchema
    {
        public GraphQLObjectType Query { get; set; }

        public string GetQueryType()
        {
            throw new System.NotImplementedException();
        }

        public string GetMutationType()
        {
            throw new System.NotImplementedException();
        }

        public string GetSubscriptionType()
        {
            throw new System.NotImplementedException();
        }

        public string GetDirectives()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ObjectGraphType
    {

    }

    public interface IGraphQLSchema
    {
        
    }
}