using System;
using System.Collections.Generic;
using Graphene.Core;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Spike
{
    public class SchemaBuilder
    {
        private readonly GraphQLSchema _schema;
        private readonly List<IGraphQLFieldType> _fields;
        private readonly TypeList _typeList;

        public SchemaBuilder()
            :this(new TypeList())
        { }

        public SchemaBuilder(TypeList typeList)
        {
            _typeList = typeList;
            _schema = new GraphQLSchema(_typeList)
            {
                QueryType = new GraphQLObjectType
                {
                    Name = "Root"
                }
            };
            _fields = new List<IGraphQLFieldType>();

        }


        public GraphQLSchema Build()
        {
            _schema.QueryType.Fields = _fields;
            _typeList.AddType(_schema.QueryType.Name, _schema.QueryType);
            RegisterTypes();
            return _schema;
        }

        public SchemaBuilder WithField(string name, GraphQLObjectType createUserType)
        {
            _fields.Add(new GraphQLObjectField
            {
                Name = name,
                Type = createUserType
            });
            return this;
        }

        public SchemaBuilder WithField(string name, Action<ObjectFieldBuilder<object>> action)
        {
            var builder = new ObjectFieldBuilder<object>(_typeList).Name(name);
            action(builder);
            _fields.Add(builder.Build());
            return this;
        }

        public SchemaBuilder WithField(Action<ObjectFieldBuilder<object>> action)
        {
            var builder = new ObjectFieldBuilder<object>(_typeList);
            action(builder);
            _fields.Add(builder.Build());
            return this;
        }

        public SchemaBuilder WithField<T>(Action<ObjectFieldBuilder<T>> action)
        {
            var builder = new ObjectFieldBuilder<T>(_typeList);
            action(builder);
            _fields.Add(builder.Build());
            return this;
        }

        public SchemaBuilder RegisterType(IGraphQLType type)
        {
            _typeList.AddType(type.Name, type);
            return this;
        }

        public SchemaBuilder RegisterTypes(params IGraphQLType[] types)
        {
            foreach (var type in types)
            {
                _typeList.AddType(type.Name, type);                
            }
            return this;
        }

        private void RegisterTypes()
        {
            _typeList.AddType("__Schema", new __Schema(_typeList));
            _typeList.AddType("__Type", new __Type(_typeList));
            _typeList.AddType("GraphQLEnum", new GraphQLEnum<IGraphQLKind> { Name = "__TypeKind" });
            _typeList.AddType("__TypeKind", new __TypeKind());
            _typeList.AddType("__Field", new __Field(_typeList));
            _typeList.AddType("__InputValue", new __InputValue(_typeList));
            _typeList.AddType("__EnumValue", new __EnumValue(_typeList));
            _typeList.AddType("__Directive", new __Directive(_typeList));
            //_typeList.AddType("NonNull", new GraphQLNonNull());
            //_typeList.AddType("List", new GraphQLList());
            _typeList.AddType("GraphQLEnum", new GraphQLEnum());
            _typeList.AddType("Boolean", new GraphQLBoolean());
            _typeList.AddType("String", new GraphQLString());
            _typeList.AddType("Int", new GraphQLInt());
        }
    }

    public class ObjectFieldBuilder<T>
    {
        private readonly GraphQLObjectField<T> _graphQLObjectField;
        private readonly TypeList _typeList;

        public ObjectFieldBuilder(TypeList typeList)
        {
            _typeList = typeList;
            _graphQLObjectField = new GraphQLObjectField<T>();
        }

        public IGraphQLFieldType Build()
        {
            return _graphQLObjectField;
        }

        public ObjectFieldBuilder<T> Name(string name)
        {
            _graphQLObjectField.Name = name;
            return this;
        }

        public ObjectFieldBuilder<T> Type(GraphQLObjectType type)
        {
            _typeList.AddType(type.Name, type);
            _graphQLObjectField.Type = new ChainType(_typeList,type.Name);
            return this;
        }

        public ObjectFieldBuilder<T> Arguments(params IGraphQLArgument[] arguments)
        {
            _graphQLObjectField.Arguments = arguments;
            return this;
        }


        public ObjectFieldBuilder<T> Resolve(Func<ResolveObjectContext, T> resolve)
        {
            _graphQLObjectField.Resolve = resolve;
            return this;
        }

        public ObjectFieldBuilder<T> Type(params string[] types)
        {
            _graphQLObjectField.Type = new ChainType(_typeList, types);
            return this;
        }
    }
} 