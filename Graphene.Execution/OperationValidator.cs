using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphene.Core.Exceptions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Types;
using Graphene.Core.Types.Scalar;

namespace Graphene.Execution
{
    public class OperationValidator
    {
        private ITypeList _typeList;

        public OperationValidator(ITypeList typeList)
        {
            _typeList = typeList;
        }

        public string[] Validate(GraphQLSchema schema, Operation operation)
        {
            var output = new List<string>();

            var queryType = schema.GetMergedRoot();

            output.AddRange(Validate(operation.Selections, queryType.GetFields().ToArray(), queryType.Name));
            
            return output.ToArray();
        }

        public string[] Validate(Selection[] selections, IGraphQLFieldType[] fieldTypes, string typeName)
        {
            var output = new List<string>();

            foreach (var selection in selections)
            {
                var graphQLFieldType = fieldTypes.FirstOrDefault(x => x.Name == selection.Field.Name);

                if (graphQLFieldType == null)
                {
                    throw new GraphQLException("Cannot query field '{0}' on type '{1}'.", selection.Field.Name, typeName);
                }

                output.AddRange(Validate(selection.Field, graphQLFieldType, typeName));
            }
            return output.ToArray();
        }

        public string[] Validate(Field field, IGraphQLFieldType graphQLFieldType, string typeName)
        {
            var output = new List<string>();

            var graphQLArguments = graphQLFieldType.Arguments != null
                ? graphQLFieldType.Arguments.ToArray()
                : new IGraphQLArgument[0];

            var type =_typeList.LookUpType(graphQLFieldType.Type.Last());

            output.AddRange(Validate(field.Selections, type.GetFields().ToArray(), type.Name));
            output.AddRange(Validate(field.Arguments, graphQLArguments, graphQLFieldType.Name, typeName));
            return output.ToArray();
        }

        public string[] Validate(Argument[] arguments, IGraphQLArgument[] graphQLArguments, string fieldName, string typeName)
        {
            if (arguments == null)
            {
                return new string[0];
            }

            if (graphQLArguments == null)
            {
                graphQLArguments = new IGraphQLArgument[0];
            }

            var output = arguments
                .GroupBy(x => x.Name)
                .Where(g => g.Count() > 1)
                .Select(duplicate => string.Format(@"There can be only one argument named ""{0}""", duplicate.Key))
                .ToList();

            foreach (var argument in arguments)
            {
                var graphQLArgument = graphQLArguments.FirstOrDefault(x => x.Name == argument.Name);

                if (graphQLArgument == null)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append(string.Format(@"Unknown argument ""{0}""", argument.Name));

                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        stringBuilder.Append(string.Format(@" on field ""{0}""", fieldName));
                    }

                    if (!string.IsNullOrEmpty(typeName))
                    {
                        stringBuilder.Append(string.Format(@" on type ""{0}""", typeName));
                    }

                    stringBuilder.Append(".");

                    output.Add(stringBuilder.ToString());
                }
                else if (graphQLArgument.Type is GraphQLString)
                {
                    var str = argument.Value as string;
                    if (string.IsNullOrEmpty(str))
                    {
                        output.Add(string.Format(@"Argument '{0}' has invalid value {1}. Expected type 'String'", argument.Name,
                                argument.Value));
                    }
                }
                else if (graphQLArgument.Type is GraphQLInt)
                {
                    if (!(argument.Value is int))
                    {
                        output.Add(string.Format(@"Argument '{0}' has invalid value {1}. Expected type 'Int'", argument.Name,
                                argument.Value));
                    }
                }
            }
            return output.ToArray();
        }
    }
}