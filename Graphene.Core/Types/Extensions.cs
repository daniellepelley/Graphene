using System.Collections.Generic;
using System.Linq;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public static class Extensions
    {
        public static IEnumerable<IGraphQLFieldType> GetFields(this IGraphQLType source)
        {
            var objectType = source as GraphQLObjectType;
            if (objectType != null)
            {
                return objectType.Fields;
            }

            var chainType = source as IGraphQLChainType;
            if (chainType != null)
            {
                return chainType.Fields;
            }

            return new IGraphQLFieldType[0];
        }

        public static bool HasField(this IGraphQLType source, string fieldName)
        {
            return source.GetFields().Any(x => x.Name != fieldName);
        }

        public static IGraphQLFieldType GetField(this IGraphQLType source, string fieldName)
        {
            return source.GetFields().FirstOrDefault(x => x.Name == fieldName);
        }

        public static IGraphQLFieldType GetRootField(this IGraphQLType source, string fieldName)
        {
            return source.GetFields().FirstOrDefault(x => x.Name == fieldName);
        }

    }
}
