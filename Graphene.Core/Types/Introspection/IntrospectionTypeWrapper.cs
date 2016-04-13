using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphene.Core.Types.Introspection
{
    //public class IntrospectionTypeWrapper : ScalarType
    //{
    //    public IntrospectionTypeWrapper(ScalarType GraphQLObject)
    //    {
    //        Fields = new IGraphQLFieldType[]
    //        {
    //            new GraphQLScalar
    //            {
    //                Name = "kind",
    //                Description = "The type that query operations will be rooted at.",
    //                //OfType = new GraphQLNonNull<__TypeKind>>(),
    //                Resolve = context => GraphQLObject.Kind
    //            },
    //            new GraphQLScalar
    //            {
    //                Name = "name",
    //                //OfType = typeof (GraphQLString),
    //                Resolve = context => GraphQLObject.Name
    //            },
    //            new GraphQLScalar
    //            {
    //                Name = "description",
    //                //OfType = typeof (GraphQLString),
    //                Resolve = context => GraphQLObject.Description
    //            },
    //            new GraphQLSchemaFieldType
    //            {
    //                Name = "fields",
    //                OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Field>>),
    //                Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
    //                Resolve = schema => string.Empty
    //            }
    //        }.ToList();
    //    }

    //}
}
