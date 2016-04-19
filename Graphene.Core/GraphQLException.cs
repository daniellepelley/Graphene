using System;

namespace Graphene.Core
{
    public class GraphQLException : Exception
    {
        public GraphQLException(string message)
            :base(message)
        { }

        public GraphQLException(string format, params object[] values)
            : base(string.Format(format, values))
        { }
    }
}