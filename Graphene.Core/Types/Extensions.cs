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
        public static GraphQLObject AsIntrospective(this IGraphQLObject source)
        {
            var fields = new IGraphQLFieldType[]
            {
                new GraphQLScalar<object, object>
                {
                    Name = "name",
                    OfType = new[] {"GraphQLString"},
                    Resolve = context => source.Name
                },
                new GraphQLScalar<object, object>
                {
                    Name = "description",
                    OfType = new[] {"GraphQLString"},
                    Resolve = context => source.Description
                }
            }.ToList();

            if (source is GraphQLObject)
            {
                fields.Add(new GraphQLObject
                {
                    Name = "fields",
                    OfType = new[] { "GraphQLSchemaList", "__Field" },
                    Fields = new List<IGraphQLFieldType>
                    {
                        new GraphQLScalar<object, object>
                        {
                            Name = "name",
                            OfType = new[] {"GraphQLString"},
                            Resolve = Resolve
                        },                    
                    },
                    Resolve = context => ((GraphQLObject)context.Source).Fields
                });

                fields.Add(new GraphQLScalar<object, object>
                {
                    Name = "kind",
                    Description = "The type that query operations will be rooted at.",
                    OfType = new[] { "GraphQLNonNull", "__TypeKind" },
                    Resolve = context => ((GraphQLObject)source).Kind
                });
            }

            return new GraphQLObject
            {
                Fields = fields
            };
        }

        private static object Resolve(ResolveFieldContext<object> context)
        {
            if (context.Source is GraphQLObject)
            {
                return ((GraphQLObject) context.Source)[context.FieldName];
            }
            return ((GraphQLScalar<object, object>)context.Source).Name;
        }
    }
}
