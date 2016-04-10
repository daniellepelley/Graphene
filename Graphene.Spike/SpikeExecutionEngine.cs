using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;
using Graphene.Execution;
using Newtonsoft.Json;

namespace Graphene.Spike
{
    public class SpikeExecutionEngine<T> : IExecutionEngine
    {
        private readonly IQueryable<T> _source;

        public SpikeExecutionEngine(IQueryable<T> source)
        {
            _source = source;
        }

        public object Execute(IGraphQLSchema schema, Document document)
        {
            return ExecuteQuery(_source, document);
        }

        private string ExecuteQuery(IQueryable<T> source, Document document)
        {
            var operation = document.Operations.First();
            var fields = operation.Selections.Select(x => x.Field.Name);
            var arguments = operation.Directives.First().Arguments;
            var result = source
                .Filter(arguments)
                .SelectPartially(fields)
                .ToArray();

            var json = JsonConvert.SerializeObject(result);
            return json;
        }
    }
}