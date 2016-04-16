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
        public GraphQLObjectField<object> Build(Type type)
        {
            var graphQLObjectType = new GraphQLObjectField<object>
            {
                Name = type.Name
            };

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                MapField(property, graphQLObjectType);
            }

            return graphQLObjectType;
        }

        private void MapField(PropertyInfo propertyInfo, GraphQLObjectField<object> graphQLObjectField)
        {
            var list = graphQLObjectField.GraphQLObjectType().Fields.ToList();
            list.Add(CreateField(propertyInfo));
            graphQLObjectField.GraphQLObjectType().Fields = list;
        }

        private IGraphQLFieldType CreateField(PropertyInfo propertyInfo)
        {
            if (new[]
            {
                typeof (string),
                typeof (int)
            }.Contains(propertyInfo.PropertyType))
            {
                return new GraphQLScalarField<object, object>
                {
                    Name = propertyInfo.Name,
                    Resolve = context => propertyInfo.GetValue(context.Source)
                };
            }


            return new GraphQLObjectField<object, object>
            {
                Name = propertyInfo.Name,
                Resolve = context => propertyInfo.GetValue(context.Source),
                GraphQLObjectType = () => new GraphQLObjectType
                {
                    Fields = propertyInfo.PropertyType.GetProperties().Select(CreateField).ToList()
                }
            };
        }
    }

    //public class GenericTypeBuilder
    //{
    //    private GraphQLObjectFieldBase CreateGraphQLObject(Type type)
    //    {
    //        var d1 = typeof(GraphQLObjectField<>);
    //        Type[] typeArgs = { type };
    //        var makeme = d1.MakeGenericType(typeArgs);
    //        return (GraphQLObjectFieldBase)Activator.CreateInstance(makeme);
    //    }

    //    private GraphQLObjectFieldBase CreateGraphQLObject(Type inputType, Type outputType)
    //    {
    //        var d1 = typeof(GraphQLObjectField<>);
    //        Type[] typeArgs = { inputType, outputType };
    //        var makeme = d1.MakeGenericType(typeArgs);
    //        return (GraphQLObjectFieldBase)Activator.CreateInstance(makeme);
    //    }

    //    private GraphQLScalarBase CreateGraphQLScalar(Type inputType, Type outputType)
    //    {
    //        var d1 = typeof(GraphQLScalarField<>);
    //        Type[] typeArgs = { inputType, outputType };
    //        var makeme = d1.MakeGenericType(typeArgs);
    //        return (GraphQLScalarBase)Activator.CreateInstance(makeme);
    //    }

    //    public GraphQLObjectFieldBase Build(Type type)
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

    //    private void MapField(Type parentType, PropertyInfo propertyInfo, GraphQLObjectFieldBase graphQLObject)
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

    //            return new GraphQLScalarField<object, object>
    //            {
    //                Name = propertyInfo.Name,
    //                Resolve = context => propertyInfo.GetValue(context.Source)
    //            };
    //        }
    //        return new GraphQLObjectField
    //        {
    //            Name = propertyInfo.Name,
    //            Resolve = context => propertyInfo.GetValue(context.Source),
    //            Fields = propertyInfo.PropertyType.GetProperties().Select(CreateField).ToList()
    //        };
    //    }
    //}
}
