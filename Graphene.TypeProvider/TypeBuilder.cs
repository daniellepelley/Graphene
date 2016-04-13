using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;

namespace Graphene.TypeProvider
{
    public class TypeBuilder
    {
        public GraphQLObjectType Build(Type type)
        {
            var graphQLObjectType = new GraphQLObjectType
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

        private void MapField(PropertyInfo propertyInfo, GraphQLObjectType graphQLObjectType)
        {
            graphQLObjectType.Fields.Add(CreateField(propertyInfo));
        }

        private IGraphQLFieldType CreateField(PropertyInfo propertyInfo)
        {
            if (new[]
            {
                typeof (string),
                typeof (int)
            }.Contains(propertyInfo.PropertyType))
            {
                return new GraphQLFieldScalarType
                {
                    Name = propertyInfo.Name,
                    Resolve = context => propertyInfo.GetValue(context.Source)
                };
            }
            return new GraphQLObjectType
            {
                Name = propertyInfo.Name,
                Resolve = context => propertyInfo.GetValue(context.Source),
                Fields = propertyInfo.PropertyType.GetProperties().Select(CreateField).ToList()
            };
        }
    }
}
