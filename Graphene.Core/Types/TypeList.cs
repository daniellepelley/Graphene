using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Constants;
using Graphene.Core.Exceptions;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class TypeList : ITypeList
    {
        private readonly Dictionary<string, IGraphQLType> _dictionary;

        public TypeList()
        {
            _dictionary = new Dictionary<string, IGraphQLType>();
        }

        public static ITypeList Create()
        {
            var typeList = new TypeList();
            typeList.AddType("__Schema", new __Schema(typeList));
            typeList.AddType("__Type", new __Type(typeList));
            typeList.AddType("__TypeKind", new __TypeKind());
            typeList.AddType("GraphQLBoolean", new GraphQLBoolean());
            typeList.AddType("__Field", new __Field(typeList));
            typeList.AddType("__InputValue", new __InputValue(typeList));
            typeList.AddType("__EnumValue", new __EnumValue(typeList));
            typeList.AddType("__Directive", new __Directive(typeList));
            typeList.AddType(GraphQLTypes.NonNull, new GraphQLNonNull());
            typeList.AddType(GraphQLTypes.List, new GraphQLList());
            typeList.AddType(GraphQLTypes.String, new GraphQLString());
            typeList.AddType(GraphQLTypes.Boolean, new GraphQLBoolean());
            typeList.AddType(GraphQLTypes.Float, new GraphQLFloat());
            typeList.AddType("GraphQLEnum", new GraphQLEnum());
            typeList.AddType("Int", new GraphQLInt());
            return typeList;
        }

        public IGraphQLType LookUpType(string typeName)
        {
            if (typeName == GraphQLTypes.NonNull)
            {
                return new GraphQLNonNull();
            }

            if (typeName == GraphQLTypes.List)
            {
                return new GraphQLList();
            }

            if (!_dictionary.ContainsKey(typeName))
            {
                throw new GraphQLException("Typename {0} is not in the dictionary", typeName);
            }

            return _dictionary[typeName];
        }

        public void AddType(string typeName, IGraphQLType type)
        {
            if (_dictionary.ContainsKey(typeName))
            {
                throw new GraphQLException("A type named '{0}' has already been registered.", typeName);
            }
            _dictionary.Add(typeName, type);
        }

        public bool HasType(string typeName)
        {
            return _dictionary.ContainsKey(typeName);
        }

        public IEnumerator<IGraphQLType> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TypeList Clone()
        {
            var output = new TypeList();

            foreach (var type in _dictionary)
            {
                output.AddType(type.Key, type.Value);
            }
            return output;
        }
    }
}