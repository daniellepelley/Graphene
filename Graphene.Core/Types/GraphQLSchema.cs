namespace Graphene.Core.Types
{
    public class GraphQLSchema : IGraphQLSchema
    {
        public GraphQLObject<object> Query { get; set; }

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

    //public class ObjectGraphType
    //{

    //}

    public interface IGraphQLSchema
    {
        
    }
}