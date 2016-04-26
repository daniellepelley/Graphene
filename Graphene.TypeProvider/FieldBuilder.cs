using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.TypeProvider
{
    public class FieldBuilder
    {
        public GraphQLList<TOutput> CreateAggregateRoot<TOutput>(IQueryable<TOutput> queryable, string name, ITypeList typeList)
        {
            Create(typeof(TOutput), typeList);
            return new GraphQLList<TOutput>
            {
                Name = name,
                Type = new ChainType(typeList, typeof(TOutput).Name),
                Resolve = context => queryable
            };
        }

        public void Create(Type type, ITypeList typeList)
        {
            var list = new List<IGraphQLFieldType>();

            foreach (var propertyInfo in type.GetProperties())
            {
                var field = BuildGraphQLFieldType(type, propertyInfo.PropertyType, propertyInfo.Name);
                field.Name = propertyInfo.Name;

                field.Type = GetType(propertyInfo.PropertyType, typeList);

                list.Add(field);
            }

            var graphQLType = new GraphQLObjectType
            {
                Fields = list,
                Name = type.Name
            };

            if (!typeList.HasType(type.Name))
            {
                typeList.AddType(type.Name, graphQLType);                
            }
        }

        private IGraphQLType GetType(Type type, ITypeList typeList)
        {
            if (type == typeof(string))
            {
                return new ChainType(typeList, "String");
            }
            if (type == typeof(int))
            {
                return new ChainType(typeList, "Int");
            }
            Create(type, typeList);
            return new ChainType(typeList, type.Name);
        }

        public IGraphQLFieldType BuildGraphQLFieldType(Type inputType, Type outputType, string name)
        {
            var method = GetType().GetMethod("BuildFieldType");
            var genericMethod = method.MakeGenericMethod(inputType, outputType);
            return (IGraphQLFieldType)genericMethod.Invoke(this, new object[] { name });
        }

        public IGraphQLFieldType BuildFieldType<TInput, TTOutput>(string name)
        {
            if (typeof (TTOutput) == typeof (string) ||
                typeof (TTOutput) == typeof (int))
            {
                var graphQLFieldType = new GraphQLScalarField<TInput, TTOutput>();

                var parameterExpression = Expression.Parameter(typeof (TInput), "source");
                var propertyExpression = Expression.Property(parameterExpression, name);

                var lambda =
                    Expression.Lambda<Func<TInput, TTOutput>>(propertyExpression, parameterExpression).Compile();
                graphQLFieldType.Resolve = context => lambda(context.Source);

                return graphQLFieldType;
            }
            else
            {
                var graphQLFieldType = new GraphQLObjectField<TInput, TTOutput>();

                var parameterExpression = Expression.Parameter(typeof(TInput), "source");
                var propertyExpression = Expression.Property(parameterExpression, name);

                var lambda =
                    Expression.Lambda<Func<TInput, TTOutput>>(propertyExpression, parameterExpression).Compile();
                graphQLFieldType.Resolve = context => lambda(context.Source);

                return graphQLFieldType;
            }
        }

    }
}