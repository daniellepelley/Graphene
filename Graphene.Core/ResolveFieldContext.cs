using System.Collections.Generic;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core
{
    public class ResolveFieldContext
    {
        public string FieldName { get; set; }

        public Field FieldAst { get; set; }

        //public FieldType FieldDefinition { get; set; }

        //public GraphType ReturnType { get; set; }

        public ObjectGraphType ParentType { get; set; }

        public Dictionary<string, object> Arguments { get; set; }

        public object RootValue { get; set; }

        public object Source { get; set; }

        public GraphQLSchema Schema { get; set; }

        public Operation Operation { get; set; }

        //public Fragments Fragments { get; set; }

        //public Variables Variables { get; set; }

        //public CancellationToken CancellationToken { get; set; }

        public TType Argument<TType>(string name)
        {
            if (Arguments.ContainsKey(name))
            {
                return (TType)Arguments[name];
            }

            return default(TType);
        }
    }
}