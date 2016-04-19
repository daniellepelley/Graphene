using System.Collections.Generic;
using System.Linq;
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

        public IGraphQLType LookUpType(string typeName)
        {
            if (!_dictionary.ContainsKey(typeName))
            {
                throw new GraphQLException("Typename {0} is not in the dictionary", typeName);
            }

            return _dictionary[typeName];
        }

        public void AddType(string typeName, IGraphQLType type)
        {
            if (!_dictionary.ContainsKey(typeName))
            {
                _dictionary.Add(typeName, type);
            }
        }
    }

    public interface ITypeList
    {
        IGraphQLType LookUpType(string typeName);
        void AddType(string typeName, IGraphQLType type);
    }

    public class ChainType : IGraphQLType
    {
        public static ITypeList TypeList = new TypeList();

        private readonly string[] _types;

        static ChainType()
        {
            TypeList.AddType("NonNull", new GraphQLNonNull(new ChainType()));
            TypeList.AddType("List", new GraphQLList(new ChainType()));
            TypeList.AddType("__Type", new __Type());
            TypeList.AddType("String", new GraphQLString());
            TypeList.AddType("__Schema", new __Schema());
        }

        public ChainType(params string[] types)
        {
            _types = types;
        }

        public string Kind
        {
            get { return GetCurrentType().Kind; }
        }

        public string Name
        {
            get { return GetCurrentType().Name; }
        }

        public string Description
        {
            get { return GetCurrentType().Description; }
        }

        public IGraphQLType OfType
        {
            get
            {
                if (_types.Length >= 2)
                {
                    return new ChainType(_types.Skip(1).ToArray());
                }
                return null;
            }
        }

        public IGraphQLType GetCurrentType()
        {
            return TypeList.LookUpType(_types.First());
        }

    }
}