using System;
using System.Collections.Generic;

namespace Graphene.Core.Types
{
    public class GraphQLObjectType : IGraphObjectType, IGraphQLFieldType
    {
        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType OfType { get; set; }
        public virtual object ResolveToObject(IResolveObjectContext context)
        {
            return Resolve(context);
        }

        public List<IGraphQLFieldType> Fields { get; set; }
        public virtual Func<IResolveObjectContext, object> Resolve { get; set; }
        public IGraphQLFieldType[] Arguments { get; set; }
    }


    public static class Extensions
    {
        public static void AddField<T>(this GraphQLObjectType source, string name, Func<object, T> resolveFunc)
        {
            source.Fields.Add(new GraphQLFieldType<T>
            {
                Name = name,
                Resolve = resolveFunc
            });
        }


    }
}