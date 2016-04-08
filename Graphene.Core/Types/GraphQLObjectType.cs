using System;
using System.Collections.Generic;

namespace Graphene.Core.Types
{
    public class GraphQLObjectType<TOutput> : IGraphObjectType
    {
        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<IGraphQLFieldType> Fields { get; set; }
        public virtual Func<ResolveFieldContext, TOutput> Resolve { get; set; }
    }

    public class GraphQLObjectType : GraphQLObjectType<object>
    {

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