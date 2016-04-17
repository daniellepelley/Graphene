using System.Collections.Generic;

namespace Graphene.Core.Types
{
    public class GraphQLSchema : IGraphQLSchema
    {
        public IGraphQLType[] Types { get; set; }

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
            if (Types != null)
            {
                return Types;
            }

            return new IGraphQLType[]
            {
                Query.GraphQLObjectType(),
                new GraphQLString(),
                new GraphQLBoolean()
            };
        }
    }

    public interface IGraphQLSchema
    {
        
    }
}