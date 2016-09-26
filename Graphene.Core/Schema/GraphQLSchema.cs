using System.Collections.Generic;
using System.Linq;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class GraphQLSchema : IGraphQLSchema
    {
        private readonly GraphQLObjectType _introspectionType;
        private ITypeList Types { get; set; }

        public GraphQLObjectType QueryType { get; set; }
        public IGraphQLType MutationType { get; set; }

        public GraphQLSchema(ITypeList typeList)
        {
            Types = typeList;
            _introspectionType = new GraphQLObjectType
            {
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLObjectField<GraphQLSchema>
                    {
                        Name = "__schema",
                        Type = new [] {"__Schema"},
                        Resolve = _ => this
                    },
                    new GraphQLObjectField<IGraphQLType>
                    {
                        Name = "__type",
                        Type = new [] { "__Type" },
                        Arguments = new IGraphQLArgument[]
                        {
                            new GraphQLArgument
                            {
                                Name = "name",
                                Type = new ChainType(Types, "String")
                            }
                        },
                        Resolve = context => Types.LookUpType(context.GetArgument<string>("name"))
                    }
                }
            };
        }

        public GraphQLObjectType GetMergedRoot()
        {
            return new GraphQLObjectType
            {
                Name = QueryType.Name,
                Fields = QueryType.Fields.Concat(_introspectionType.Fields)
            };
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

        public ITypeList TypeList
        {
            get { return Types; }
        }
    }

    public interface IGraphQLSchema
    {
        
    }
}