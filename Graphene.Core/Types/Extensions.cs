using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphene.Core.Types.Introspection;

namespace Graphene.Core.Types
{
    public static class Extensions
    {
        public static GraphQLObjectType AsIntrospective(this GraphQLObjectType graphQLObjectType)
        {
            return new GraphQLObjectType
            {
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLFieldScalarType
                    {
                        Name = "kind",
                        Description = "The type that query operations will be rooted at.",
                        //OfType = new GraphQLNonNull<__TypeKind>>(),
                        Resolve = context => graphQLObjectType.Kind
                    },
                    new GraphQLFieldScalarType
                    {
                        Name = "name",
                        //OfType = typeof (GraphQLString),
                        Resolve = context => graphQLObjectType.Name
                    },
                    new GraphQLFieldScalarType
                    {
                        Name = "description",
                        //OfType = typeof (GraphQLString),
                        Resolve = context => graphQLObjectType.Description
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "fields",
                        OfType = typeof (GraphQLList<GraphQLNonNull<__Field>>),
                        Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                        Resolve = schema => string.Empty
                    }
                }.ToList()
            };

        }

    }
}
