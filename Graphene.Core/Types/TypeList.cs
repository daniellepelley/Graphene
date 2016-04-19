using System.Collections;
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

        public IEnumerator<IGraphQLType> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public interface ITypeList : IEnumerable<IGraphQLType>
    {
        IGraphQLType LookUpType(string typeName);
        void AddType(string typeName, IGraphQLType type);
    }

    public class ChainType : IGraphQLType
    {
        private readonly string[] _types;
        private readonly ITypeList _typeList;

        public ChainType(ITypeList typeList, params string[] types)
        {
            _typeList = typeList;
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
                    return new ChainType(_typeList, _types.Skip(1).ToArray());
                }
                return null;
            }
        }

        public IGraphQLType GetCurrentType()
        {
            return _typeList.LookUpType(_types.First());
        }

    }
}