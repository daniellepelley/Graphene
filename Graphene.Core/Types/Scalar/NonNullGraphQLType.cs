using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types
{
    public class NonNullGraphQLType : IGraphQLType
    {
        private readonly IGraphQLType _graphQLType;

        public NonNullGraphQLType(IGraphQLType graphQLType)
        {
            _graphQLType = graphQLType;
        }

        public string Kind
        {
            get { return "NON_NULL"; }
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public string[] OfType
        {
            get { return GetTypes(); }

        }

        private string[] GetTypes()
        {
            var output = new List<string>
            {
                _graphQLType.Name
            };

            if (_graphQLType.OfType != null)
            {
                output.AddRange(_graphQLType.OfType);
            }
            else if (_graphQLType.OfType != null)
            {
                output.Add(_graphQLType.Name);
            }

            return output.ToArray();
        }
    }
}