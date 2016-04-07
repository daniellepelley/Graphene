using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphene.Core.Model;
using Graphene.Core.Types;
using Graphene.Schema;
using Newtonsoft.Json;
using GraphQLSchema = Graphene.Core.Types.GraphQLSchema;

namespace Graphene.Execution
{
    public class ExecutionEngine : IExecutionEngine
    {
        public string Execute(IGraphQLSchema iGraphQLSchema, Document document)
        {
            GraphQLSchema schema = (GraphQLSchema)iGraphQLSchema;

            if (schema.Query == null)
            {
                throw new Exception("Query empty");    
            }

            var operation = document.Operations.First();

            var output = new List<object>();

            var fieldValues = new Dictionary<string, object>();

            output.Add(fieldValues);

            foreach (var selection in operation.Selections)
            {
                var schemaField = schema.Query.Fields.FirstOrDefault(x => x.Name == selection.Field.Name);

                if (schemaField == null)
                {
                    throw new Exception(string.Format("Field {0} does not exist", selection.Field.Name));
                }

                var context = new ResolveFieldContext
                {
                    Schema = schema,
                    FieldName = selection.Field.Name,
                    Operation = operation
                };

                fieldValues.Add(schemaField.Name, schemaField.Resolve(context));
            }

            return JsonConvert.SerializeObject(output);
        }
    }
}
