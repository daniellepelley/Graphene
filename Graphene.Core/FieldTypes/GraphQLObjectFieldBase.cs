using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types.Introspection;

namespace Graphene.Core.Types
{
    public interface IToExecutionBranch
    {
        ExecutionBranch ToExecutionBranch(Selection[] selections, IDictionary<string, object> arguments);
    }

    public class GraphQLObjectFieldBase
    {
        public IGraphQLFieldType this[string name]
        {
            get { return GraphQLObjectType.Fields.FirstOrDefault(x => x.Name == name); }
        }

        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public Func<GraphQLObjectType> GraphQLObjectType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string[] OfType { get; set; }
        public IEnumerable<IGraphQLArgument> Arguments { get; set; } 

        public GraphQLObjectFieldBase()
        {
            GraphQLObjectType = () => new GraphQLObjectType();
        }
    }
}