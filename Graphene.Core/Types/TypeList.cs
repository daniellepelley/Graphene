using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (typeName == "NonNull")
            {
                return new GraphQLNonNull();
            }

            if (typeName == "List")
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