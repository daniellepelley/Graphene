using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphene.Core.Types.Introspection
{
    //public class IntrospectionTypeWrapper : ScalarFieldType
    //{
    //    public IntrospectionTypeWrapper(ScalarFieldType GraphQLObjectField)
    //    {
    //        Fields = new IGraphQLFieldType[]
    //        {
    //            new GraphQLScalarField
    //            {
    //                Name = "kind",
    //                Description = "The type that query operations will be rooted at.",
    //                //OfType = new GraphQLNonNull<__TypeKind>>(),
    //                Resolve = context => GraphQLObjectField.Kind
    //            },
    //            new GraphQLScalarField
    //            {
    //                Name = "name",
    //                //OfType = typeof (GraphQLString),
    //                Resolve = context => GraphQLObjectField.Name
    //            },
    //            new GraphQLScalarField
    //            {
    //                Name = "description",
    //                //OfType = typeof (GraphQLString),
    //                Resolve = context => GraphQLObjectField.Description
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
