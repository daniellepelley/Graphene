using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.TypeProvider
{
    public class FieldBuilder
    {
        public GraphQLList<TOutput> CreateAggregateRoot<TOutput>(IQueryable<TOutput> queryable, string name,
            ITypeList typeList)
        {
            Create(typeof(TOutput), typeList);
            return new GraphQLList<TOutput>
            {
                Name = name,
                Type = new[] {typeof(TOutput).Name},
                Resolve = context => queryable
            };
        }

        public void Create(Type type, ITypeList typeList)
        {
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var innerType = type.GetGenericArguments().First();
                Create(innerType, typeList);
                return;
            }

            var graphQLType = new GraphQLObjectType
            {
                Name = type.Name
            };

            if (!typeList.HasType(type.Name))
            {
                typeList.AddType(type.Name, graphQLType);
            }

            var list = new List<IGraphQLFieldType>();

            foreach (var propertyInfo in type.GetProperties(
                          BindingFlags.Public | 
                          BindingFlags.Instance | 
                          BindingFlags.DeclaredOnly))
            {
                var field = BuildGraphQLFieldType(type, propertyInfo.PropertyType, propertyInfo.Name);
                field.Name = propertyInfo.Name;

                var fieldType = GetType(propertyInfo.PropertyType, typeList);

                field.Type = fieldType;

                list.Add(field);
            }

            graphQLType.Fields = list;
        }

        private string[] GetType(Type type, ITypeList typeList)
        {
            if (type == typeof(string))
            {
                return new[] {"String"};
            }
            if (type == typeof(int))
            {
                return new[] {"Int"};
            }
            if (type == typeof(float))
            {
                return new[] { "Float" };
            }
            if (type == typeof(bool))
            {
                return new[] { "Boolean" };
            }
            if (!typeList.HasType(type.Name))
            {
                Create(type, typeList);
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return new[] {"List", type.GetGenericArguments().First().Name};
            }

            return new[] {type.Name};
        }

        public IGraphQLFieldType BuildGraphQLFieldType(Type inputType, Type outputType, string name)
        {
            if (typeof(IEnumerable).IsAssignableFrom(outputType) && outputType.GenericTypeArguments.Any())
            {
                var method = GetType().GetMethod("BuildListFieldType");
                var genericMethod = method.MakeGenericMethod(inputType, outputType.GenericTypeArguments.First());
                return (IGraphQLFieldType) genericMethod.Invoke(this, new object[] {name});
            }
            else
            {
                var method = GetType().GetMethod("BuildFieldType");
                var genericMethod = method.MakeGenericMethod(inputType, outputType);
                return (IGraphQLFieldType) genericMethod.Invoke(this, new object[] {name});
            }
        }
        
        public IGraphQLFieldType BuildFieldType<TInput, TOutput>(string name)
        {
            if (typeof(TOutput) == typeof(string) ||
                typeof(TOutput) == typeof(int))
            {
                var graphQLFieldType = new GraphQLScalarField<TInput, TOutput>();

                var parameterExpression = Expression.Parameter(typeof(TInput), "source");
                var propertyExpression = Expression.Property(parameterExpression, name);

                var lambda =
                    Expression.Lambda<Func<TInput, TOutput>>(propertyExpression, parameterExpression).Compile();
                graphQLFieldType.Resolve = context => lambda(context.Source);

                return graphQLFieldType;
            }
            else
            {
                var graphQLFieldType = new GraphQLObjectField<TInput, TOutput>();

                var parameterExpression = Expression.Parameter(typeof(TInput), "source");
                var propertyExpression = Expression.Property(parameterExpression, name);

                var lambda =
                    Expression.Lambda<Func<TInput, TOutput>>(propertyExpression, parameterExpression).Compile();
                graphQLFieldType.Resolve = context => lambda(context.Source);

                return graphQLFieldType;
            }
        }

        public IGraphQLFieldType BuildListFieldType<TInput, TOutput>(string name)
        {
            var graphQLFieldType = new GraphQLList<TInput, TOutput>();

            var parameterExpression = Expression.Parameter(typeof(TInput), "source");
            var propertyExpression = Expression.Property(parameterExpression, name);

            var lambda =
                Expression.Lambda<Func<TInput, IEnumerable<TOutput>>>(propertyExpression, parameterExpression)
                    .Compile();
            graphQLFieldType.Resolve = context => lambda(context.Source);

            return graphQLFieldType;
        }
    }
}