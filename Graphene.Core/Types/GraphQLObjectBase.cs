using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public interface IToExecutionBranch
    {
        ExecutionBranch ToExecutionBranch(Selection[] selections, IDictionary<string, object> arguments);
    }

    public class GraphQLObjectBase
    {
        public IGraphQLFieldType this[string name]
        {
            get { return Fields.FirstOrDefault(x => x.Name == name); }
        }

        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string[] OfType { get; set; }
        public List<IGraphQLFieldType> Fields { get; set; }
        public IGraphQLFieldType[] Arguments { get; set; }
    }
}