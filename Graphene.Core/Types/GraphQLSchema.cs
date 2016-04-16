using System.Collections.Generic;

namespace Graphene.Core.Types
{
    public class GraphQLSchema : IGraphQLSchema
    {
        public GraphQLObjectFieldBase Query { get; set; }

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

        public IEnumerable<IGraphQLType> GetTypes()
        {
            return new IGraphQLType[]
            {
                new GraphQLString(),
                new GraphQLBoolean()
            };
        }
    }

    public interface IGraphQLSchema
    {
        
    }
}