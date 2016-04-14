using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Graphene.Core.Types;

namespace Graphene.TypeProvider
{
    public class SimpleTypeBuilder
    {
        public GraphQLObject Build(Type type)
        {
            var graphQLObjectType = new GraphQLObject
            {
                Name = type.Name,
                Fields = new List<IGraphQLFieldType>()
            };

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                MapField(property, graphQLObjectType);
            }

            return graphQLObjectType;
        }

        private void MapField(PropertyInfo propertyInfo, GraphQLObject graphQLObject)
        {
            graphQLObject.Fields.Add(CreateField(propertyInfo));
        }

        private IGraphQLFieldType CreateField(PropertyInfo propertyInfo)
        {
            if (new[]
            {
                typeof (string),
                typeof (int)
            }.Contains(propertyInfo.PropertyType))
            {
                return new GraphQLScalar
                {
                    Name = propertyInfo.Name,
                    Resolve = context => propertyInfo.GetValue(context.Source)
                };
            }
            return new GraphQLObject
            {
                Name = propertyInfo.Name,
                Resolve = context => propertyInfo.GetValue(context.Source),
                Fields = propertyInfo.PropertyType.GetProperties().Select(CreateField).ToList()
            };
        }
    }
}
