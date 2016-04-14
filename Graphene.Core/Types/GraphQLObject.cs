using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public class GraphQLObject : GraphQLObject<object, object>
    {

    }

    public abstract class GraphQLObjectBase
    {
        public abstract ExecutionRoot ToExecutionRoot(Selection[] selections, IDictionary<string, object> arguments);
    }

    public class GraphQLObject<TInput, TOutput> : IGraphQLObject, IInputField<TInput>
    {
        public IGraphQLFieldType this[string name]
        {
            get { return Fields.FirstOrDefault(x => x.Name == name); }
        }

        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public ExecutionRoot ToExecutionBranch(Selection[] selections, Func<TInput> getter)
        {
            var executionRoot = new ExecutionBranch<TInput, TOutput>(Name, Resolve, getter);

            foreach (var selection in selections)
            {
                if (this[selection.Field.Name] is GraphQLScalar<TOutput>)
                {
                    var graphQLScalar = (GraphQLScalar<TOutput>)this[selection.Field.Name];

                    var node = graphQLScalar.ToExecutionNode(executionRoot.GetOutput);
                    executionRoot.AddNode(node);
                }
                else if (this[selection.Field.Name] is IInputField<TOutput>)
                {
                    var graphQLObject = (IInputField<TOutput>)this[selection.Field.Name];

                    var branch = graphQLObject.ToExecutionBranch(selection.Field.Selections, executionRoot.GetOutput);
                    executionRoot.AddNode(branch);
                }
            }

            return executionRoot;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string[] OfType { get; set; }

        public List<IGraphQLFieldType> Fields { get; set; }
        public virtual Func<ResolveObjectContext<TInput>, TOutput> Resolve { get; set; }
        public IGraphQLFieldType[] Arguments { get; set; }
    }

    public class GraphQLObject<TOutput> : GraphQLObjectBase, IGraphQLObject
    {
        public IGraphQLFieldType this[string name]
        {
            get { return Fields.FirstOrDefault(x => x.Name == name); }
        }

        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public ExecutionRoot ToExecutionBranch(Selection[] selections, Func<TOutput> getInput)
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string[] OfType { get; set; }

        public List<IGraphQLFieldType> Fields { get; set; }
        public virtual Func<ResolveObjectContext, TOutput> Resolve { get; set; }
        public IGraphQLFieldType[] Arguments { get; set; }

        public override ExecutionRoot ToExecutionRoot(Selection[] selections, IDictionary<string, object> arguments)
        {
            var executionRoot = new ExecutionRoot<TOutput>(Name, arguments, Resolve);

            foreach (var selection in selections)
            {
                if (this[selection.Field.Name] is GraphQLScalar<TOutput>)
                {
                    var graphQLScalar = (GraphQLScalar<TOutput>)this[selection.Field.Name];

                    var node = graphQLScalar.ToExecutionNode(executionRoot.GetOutput);
                    executionRoot.AddNode(node);
                }
                else if (this[selection.Field.Name] is IInputField<TOutput>)
                {
                    var graphQLObject = (IInputField<TOutput>)this[selection.Field.Name];

                    var branch = graphQLObject.ToExecutionBranch(selection.Field.Selections, executionRoot.GetOutput);
                    executionRoot.AddNode(branch);
                }
            }

            return executionRoot;
        }
    }
}