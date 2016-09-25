using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;

namespace Graphene.TypeProvider
{
    public class SimpleTypeBuilder
    {
        public GraphQLObjectField<object> Build(Type type)
        {
            var graphQLObjectType = new GraphQLObjectField<object>
            {
                Name = type.Name,
                Type = new GraphQLObjectType()
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
            var graphQLObjectType = (GraphQLObjectType) graphQLObjectField.Type;

            var list = graphQLObjectType.Fields.ToList();
            list.Add(CreateField(propertyInfo));
            graphQLObjectType.Fields = list;
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
                    Resolve = context => propertyInfo.GetValue(context.Source),
                };
            }

            return new GraphQLObjectField<object, object>
            {
                Name = propertyInfo.Name,
                Resolve = context => propertyInfo.GetValue(context.Source),
                Type = new GraphQLObjectType
                {
                    Fields = propertyInfo.PropertyType.GetProperties().Select(CreateField).ToList()
                }
            };
        }
    }

    public static class ExpressionTree
    {
        private static MemberExpression CreateMemberExpression<T>(string propertyName)
        {
            var pe = Expression.Parameter(typeof(T), "source");
            return Expression.Property(pe, "Name");
        }

        private static Expression CreateAggregateRoot<T>(string propertyName)
        {
            var pe = Expression.Parameter(typeof(T), "source");
            var me = Expression.Property(pe, "Name");
            return Expression.Lambda<Func<T, string>>(me, pe);
        }

        private static Func<TEntity, object> GetPropGetter<TEntity>(string propertyName)
        {
            var parameterExpression = Expression.Parameter(typeof(TEntity), "x");

            var body = propertyName.Split('.')
                .Aggregate<string, Expression>(parameterExpression, (current, property) =>
                    Expression.Condition(
                    Expression.Equal(current, Expression.Default(current.Type)),
                    Expression.Default(Expression.PropertyOrField(current, property).Type),
                    Expression.PropertyOrField(current, property)));

            return Expression.Lambda<Func<TEntity, object>>(body, parameterExpression).Compile();
        }
    }

    //public class FieldBuilder
    //{
    //    public GraphQLList<TOutput> CreateAggregateRoot<TOutput>(IQueryable<TOutput> queryable, string name, ITypeList typeList)
    //    {
    //        Create(typeof(TOutput), typeList);
    //        return new GraphQLList<TOutput>
    //        {
    //            Name = name,
    //            Type = new ChainType(typeList, typeof(TOutput).Name),
    //            Resolve = context => queryable
    //        };
    //    }

    //    public void Create(Type type, ITypeList typeList)
    //    {
    //        var list = new List<IGraphQLFieldType>();

    //        foreach (var propertyInfo in type.GetProperties())
    //        {
    //            var field = BuildGraphQLFieldType(type, propertyInfo.PropertyType, propertyInfo.Name);
    //            field.Name = propertyInfo.Name;

    //            field.Type = GetType(propertyInfo.PropertyType, typeList);

    //            list.Add(field);
    //        }

    //        var graphQLType = new GraphQLObjectType
    //        {
    //            Fields = list,
    //            Name = type.Name
    //        };

    //        if (!typeList.HasType(type.Name))
    //        {
    //            typeList.AddType(type.Name, graphQLType);                
    //        }
    //    }

    //    private IGraphQLType GetType(Type type, ITypeList typeList)
    //    {
    //        if (type == typeof(string))
    //        {
    //            return new ChainType(typeList, "String");
    //        }
    //        if (type == typeof(int))
    //        {
    //            return new ChainType(typeList, "Int");
    //        }
    //        Create(type, typeList);
    //        return new ChainType(typeList, type.Name);
    //    }

    //    public IGraphQLFieldType BuildGraphQLFieldType(Type inputType, Type outputType, string name)
    //    {
    //        var method = GetType().GetMethod("BuildFieldType");
    //        var genericMethod = method.MakeGenericMethod(inputType, outputType);
    //        return (IGraphQLFieldType)genericMethod.Invoke(this, new object[] { name });
    //    }

    //    public IGraphQLFieldType BuildFieldType<TInput, TTOutput>(string name)
    //    {
    //        if (typeof (TTOutput) == typeof (string) ||
    //            typeof (TTOutput) == typeof (int))
    //        {
    //            var graphQLFieldType = new GraphQLScalarField<TInput, TTOutput>();

    //            var parameterExpression = Expression.Parameter(typeof (TInput), "source");
    //            var propertyExpression = Expression.Property(parameterExpression, name);

    //            var lambda =
    //                Expression.Lambda<Func<TInput, TTOutput>>(propertyExpression, parameterExpression).Compile();
    //            graphQLFieldType.Resolve = context => lambda(context.Source);

    //            return graphQLFieldType;
    //        }
    //        else
    //        {
    //            var graphQLFieldType = new GraphQLObjectField<TInput, TTOutput>();

    //            var parameterExpression = Expression.Parameter(typeof(TInput), "source");
    //            var propertyExpression = Expression.Property(parameterExpression, name);

    //            var lambda =
    //                Expression.Lambda<Func<TInput, TTOutput>>(propertyExpression, parameterExpression).Compile();
    //            graphQLFieldType.Resolve = context => lambda(context.Source);

    //            return graphQLFieldType;
    //        }
    //    }

    //}


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
