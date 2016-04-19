using System.Collections.Generic;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class GraphQLSchema : IGraphQLSchema
    {
        public ITypeList Types { get; private set; }

        public GraphQLObjectType QueryType { get; set; }
        public IGraphQLType MutationType { get; set; }

        public GraphQLSchema(ITypeList typeList)
        {
            Types = typeList;
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

        public IEnumerable<IGraphQLType> GetTypes()
        {
            return Types;
        }
    }

    public interface IGraphQLSchema
    {
        
    }
}