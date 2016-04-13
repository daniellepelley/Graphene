using System.Collections.Generic;
using System.Linq;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core
{
    public class ResolveFieldContext : IResolveContext
    {
        public string FieldName { get; set; }
        public GraphQLFieldScalarType ScalarType { get; set; }
        public ResolveObjectContext Parent { get; set; }
        
        public object Source { get; set; }

        public object GetValue()
        {
            return GetValue(Source);
        }

        public object GetValue(object source)
        {
            var field = Parent.ObjectType[FieldName] as GraphQLFieldScalarType;
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

    public class ResolveObjectContext : IResolveContext
    {
        public string FieldName { get; set; }

        public Field FieldAst { get; set; }

        public ResolveObjectContext Parent { get; set; }

        public GraphQLObjectType ObjectType { get; set; }

        public Selection[] Selections { get; set; }

        public Dictionary<string, object> Arguments { get; set; }

        public object RootValue { get; set; }

        public object Source { get; set; }

        public GraphQLSchema Schema { get; set; }

        public Operation Operation { get; set; }
        public Selection Selection { get; set; }

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
            var context = (ResolveObjectContext)this.MemberwiseClone();
            context.Parent = this;
            return context;
        }
    }

    public interface IResolveContext
    {
        string FieldName { get; set; }

        ResolveObjectContext Parent { get; set; }

        //Selection[] Selections { get; set; }

        Dictionary<string, object> Arguments { get; set; }

        object RootValue { get; set; }

        object Source { get; set; }

        GraphQLSchema Schema { get; set; }

        Operation Operation { get; set; }
    }
}