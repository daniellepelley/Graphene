using System;
using System.Collections.Generic;

namespace Graphene.Core.Types
{
    public class GraphQLObjectType : IGraphObjectType
    {
        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<GraphQLFieldType> Fields { get; set; }
    }

    public static class Extensions
    {
        public static void AddField<T>(this GraphQLObjectType source, string name, Func<object, object> resolveFunc)
        {
            source.Fields.Add(new GraphQLFieldType
            {
                Name = name,
                Resolve = context => resolveFunc(context).ToString()
            });
        }


    }
}