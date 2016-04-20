using System;
using System.Linq;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;

namespace Graphene.Core
{
    public class ResolveObjectContext<T> : ResolveObjectContext, IResolveContext<T>
    {
        public T Source { get; set; }
        //public ResolveObjectContext<T> Clone()
        //{
        //    var context = (ResolveObjectContext<T>)this.MemberwiseClone();
        //    context.Parent = this;
        //    return context;
        //}
    }

    public class ResolveObjectContext : IResolveContext
    {
        public string FieldName { get; set; }

        public GraphQLObjectField ObjectFieldType { get; set; }

        public Argument[] Arguments { get; set; }

        public T GetArgument<T>(string name)
        {
            var value = Arguments.FirstOrDefault(x => x.Name == name);

            if (value == null)
            {
                return default(T);
            }

            return (T)value.Value;
        }

    }
}