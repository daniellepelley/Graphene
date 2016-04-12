using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core
{
    public class ResolveFieldContext
    {
        public string FieldName { get; set; }
        public GraphQLFieldScalarType GraphQLObjectType { get; set; }
        public GraphQLObjectType Parent { get; set; }
        public object Source { get; set; }

        public object GetValue()
        {
            return GetValue(Source);
        }

        public object GetValue(object source)
        {
            var field = Parent.Fields.FirstOrDefault(x => x.Name == FieldName) as GraphQLFieldScalarType;
            return field.Resolve(this);
        }

        public Dictionary<string, object> Arguments { get; set; }

        public object RootValue { get; set; }

        public GraphQLSchema Schema { get; set; }
        public Operation Operation { get; set; }

        public TType Argument<TType>(string name)
        {
            if (Arguments.ContainsKey(name))
            {
                return (TType)Arguments[name];
            }

            return default(TType);
        }
    }

    public class ResolveObjectContext : IResolveObjectContext
    {
        public string FieldName { get; set; }

        public Field FieldAst { get; set; }

        //public FieldType FieldDefinition { get; set; }

        //public GraphType ReturnType { get; set; }

        public GraphQLObjectType Parent { get; set; }

        public GraphQLObjectType GraphQLObjectType { get; set; }

        public Selection[] Selections { get; set; }

        public Dictionary<string, object> Arguments { get; set; }

        public object RootValue { get; set; }

        public object Source { get; set; }

        public GraphQLSchema Schema { get; set; }

        public Operation Operation { get; set; }
        public Selection Selection { get; set; }

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

        public ResolveObjectContext Clone()
        {
            return (ResolveObjectContext)this.MemberwiseClone();
        }
    }

    public interface IResolveObjectContext
    {
        string FieldName { get; set; }

        Field FieldAst { get; set; }

        GraphQLObjectType Parent { get; set; }

        GraphQLObjectType GraphQLObjectType { get; set; }

        Selection[] Selections { get; set; }

        Dictionary<string, object> Arguments { get; set; }

        object RootValue { get; set; }

        object Source { get; set; }

        GraphQLSchema Schema { get; set; }

        Operation Operation { get; set; }
    }
}