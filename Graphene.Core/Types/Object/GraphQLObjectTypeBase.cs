using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Exceptions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Object
{
    public abstract class GraphQLObjectTypeBase : IGraphQLType
    {
        public abstract string Kind { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType OfType { get; set; }

        private IEnumerable<IGraphQLFieldType> _fields;

        public IEnumerable<IGraphQLFieldType> Fields
        {
            get { return _fields; }
            set
            {
                if (value.Any() &&
                    value.Select(x => x.Name).Distinct().Count() != value.Count())
                {
                    throw new GraphQLException(ExceptionMessages.FieldsOnObjectsMustBeUniquelyNamed);
                }

                _fields = value;
            }
        }

        protected GraphQLObjectTypeBase()
        {
            _fields = new IGraphQLFieldType[0];
        }
    }
}