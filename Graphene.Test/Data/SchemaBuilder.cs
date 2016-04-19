using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Object;

namespace Graphene.Test.Data
{
    public class SchemaBuilder
    {
        private IEnumerable<IGraphQLArgument> _arguments;
        private Func<ResolveObjectContext, User> _resolve;

        public SchemaBuilder()
        {
            _resolve = context =>
                Data.GetData()
                    .FirstOrDefault(
                        x =>
                            context.Arguments.All(arg => arg.Name != "id") ||
                            x.Id == Convert.ToInt32(context.Arguments.First(arg => arg.Name == "id").Value));
        }

        public SchemaBuilder WithArguments(IEnumerable<IGraphQLArgument> arguments)
        {
            _arguments = arguments;
            return this;
        }

        public SchemaBuilder WithResolve(Func<ResolveObjectContext, User> resolve)
        {
            _resolve = resolve;
            return this;
        }

        public GraphQLSchema Build()
        {
            var userType = TestSchemas.CreateUserType();

            return new GraphQLSchema(TestSchemas.GetTypeList())
            {
                QueryType = new GraphQLObjectType
                {
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLObjectField<User>
                        {
                            Name = "user",
                            Arguments = _arguments,
                            Resolve = _resolve,
                            Type = userType
                        }
                    }
                }
            };
        }
    }
}