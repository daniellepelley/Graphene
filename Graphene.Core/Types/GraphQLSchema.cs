using System.Collections.Generic;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class GraphQLSchema : IGraphQLSchema
    {
        public ITypeList Types { get; private set; }

        public GraphQLObjectType QueryType { get; set; }
        public IGraphQLType MutationType { get; set; }

        public GraphQLSchema(ITypeList typeList)
        {
            Types = typeList;
        }

        public string GetMutationType()
        {
            throw new System.NotImplementedException();
        }

        public string GetSubscriptionType()
        {
            throw new System.NotImplementedException();
        }

        public GraphQLDirective[] GetDirectives()
        {
            return new[]
            {
                new GraphQLDirective
                {
                    Name = "include",
                    Description = "Directs the executor to include this field or fragment only when the `if` argument is true.",
                    Arguments = new IGraphQLArgument[]
                    {
                        new GraphQLArgument
                        {
                          Name = "if",
                          Description = "Included when true.",
                          Type = new ChainType(Types, "NonNull", "Boolean"),
                          DefaultValue = null      
                        }
                    },
                    OnOperation = false,
                    OnFragment = true,
                    OnField = true
                },
                new GraphQLDirective
                {
                    Name = "skip",
                    Description = "Directs the executor to skip this field or fragment when the `if` argument is true.",
                    Arguments = new IGraphQLArgument[]
                    {
                        new GraphQLArgument
                        {
                          Name = "if",
                          Description = "Skipped when true.",
                          Type = new ChainType(Types, "NonNull", "Boolean"),
                          DefaultValue = null      
                        }
                    },
                    OnOperation = false,
                    OnFragment = true,
                    OnField = true
                }
            };
        }

        public IEnumerable<IGraphQLType> GetTypes()
        {
            return Types;
        }
    }

    public interface IGraphQLSchema
    {
        
    }
}