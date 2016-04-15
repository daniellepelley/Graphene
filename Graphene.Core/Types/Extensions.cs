//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Graphene.Core.Types.Introspection;

//namespace Graphene.Core.Types
//{
//    public static class Extensions
//    {
//        public static GraphQLObjectField AsIntrospective(this IGraphQLObject source)
//        {
//            var fields = new IGraphQLFieldType[]
//            {
//                new GraphQLScalarField<object, object>
//                {
//                    Name = "name",
//                    OfType = new[] {"GraphQLString"},
//                    Resolve = context => source.Name
//                },
//                new GraphQLScalarField<object, object>
//                {
//                    Name = "description",
//                    OfType = new[] {"GraphQLString"},
//                    Resolve = context => source.Description
//                }
//            }.ToList();

//            if (source is GraphQLObjectField)
//            {
//                fields.Add(new GraphQLObjectField
//                {
//                    Name = "fields",
//                    OfType = new[] { "GraphQLSchemaList", "__Field" },
//                    Fields = new List<IGraphQLFieldType>
//                    {
//                        new GraphQLScalarField<object, object>
//                        {
//                            Name = "name",
//                            OfType = new[] {"GraphQLString"},
//                            Resolve = Resolve
//                        },                    
//                    },
//                    Resolve = context => ((GraphQLObjectField)context.Source).Fields
//                });

//                fields.Add(new GraphQLScalarField<object, object>
//                {
//                    Name = "kind",
//                    Description = "The type that query operations will be rooted at.",
//                    OfType = new[] { "GraphQLNonNull", "__TypeKind" },
//                    Resolve = context => ((GraphQLObjectField)source).Kind
//                });
//            }

//            return new GraphQLObjectField
//            {
//                Fields = fields
//            };
//        }

//        private static object Resolve(ResolveFieldContext<object> context)
//        {
//            if (context.Source is GraphQLObjectField)
//            {
//                return ((GraphQLObjectField) context.Source)[context.FieldName];
//            }
//            return ((GraphQLScalarField<object, object>)context.Source).Name;
//        }
//    }
//}
