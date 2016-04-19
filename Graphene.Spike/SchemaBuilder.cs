using System.Collections.Generic;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Spike
{
    public class SchemaBuilder
    {
        private readonly GraphQLSchema _schema;

        private readonly List<IGraphQLFieldType> _fields;

        public SchemaBuilder()
        {
            _schema = new GraphQLSchema(new TypeList());
            _schema.QueryType = new GraphQLObjectType();
            _fields = new List<IGraphQLFieldType>();
        }

        public GraphQLSchema Build()
        {
            _schema.QueryType.Fields = _fields;
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
    }
}