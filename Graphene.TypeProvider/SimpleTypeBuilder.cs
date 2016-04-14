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
        public GraphQLObject<object> Build(Type type)
        {
            var graphQLObjectType = new GraphQLObject<object>
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

        private void MapField(PropertyInfo propertyInfo, GraphQLObject<object> graphQLObject)
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
                return new GraphQLScalar<object, object>
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

    //public class GenericTypeBuilder
    //{
    //    private GraphQLObjectBase CreateGraphQLObject(Type type)
    //    {
    //        var d1 = typeof(GraphQLObject<>);
    //        Type[] typeArgs = { type };
    //        var makeme = d1.MakeGenericType(typeArgs);
    //        return (GraphQLObjectBase)Activator.CreateInstance(makeme);
    //    }

    //    private GraphQLObjectBase CreateGraphQLObject(Type inputType, Type outputType)
    //    {
    //        var d1 = typeof(GraphQLObject<>);
    //        Type[] typeArgs = { inputType, outputType };
    //        var makeme = d1.MakeGenericType(typeArgs);
    //        return (GraphQLObjectBase)Activator.CreateInstance(makeme);
    //    }

    //    private GraphQLScalarBase CreateGraphQLScalar(Type inputType, Type outputType)
    //    {
    //        var d1 = typeof(GraphQLScalar<>);
    //        Type[] typeArgs = { inputType, outputType };
    //        var makeme = d1.MakeGenericType(typeArgs);
    //        return (GraphQLScalarBase)Activator.CreateInstance(makeme);
    //    }

    //    public GraphQLObjectBase Build(Type type)
    //    {
    //        var graphQLObjectType = CreateGraphQLObject(type);

    //        graphQLObjectType.Name = type.Name;
    //        graphQLObjectType.Fields = new List<IGraphQLFieldType>();
          
    //        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

    //        foreach (var property in properties)
    //        {
    //            MapField(type, property, graphQLObjectType);
    //        }

    //        return graphQLObjectType;
    //    }

    //    private void MapField(Type parentType, PropertyInfo propertyInfo, GraphQLObjectBase graphQLObject)
    //    {
    //        graphQLObject.Fields.Add(CreateField(parentType, propertyInfo));
    //    }

    //    private IGraphQLFieldType CreateField(Type parentType,  PropertyInfo propertyInfo)
    //    {
    //        if (new[]
    //        {
    //            typeof (string),
    //            typeof (int)
    //        }.Contains(propertyInfo.PropertyType))
    //        {
    //            var scalar = CreateGraphQLObject(parentType, propertyInfo.PropertyType);
    //            scalar.Name = propertyInfo.Name;

    //            return new GraphQLScalar<object, object>
    //            {
    //                Name = propertyInfo.Name,
    //                Resolve = context => propertyInfo.GetValue(context.Source)
    //            };
    //        }
    //        return new GraphQLObject
    //        {
    //            Name = propertyInfo.Name,
    //            Resolve = context => propertyInfo.GetValue(context.Source),
    //            Fields = propertyInfo.PropertyType.GetProperties().Select(CreateField).ToList()
    //        };
    //    }
    //}
}
