using System;

namespace Graphene.Core
{
    public class GraphQLException : Exception
    {
        public GraphQLException(string message)
            :base(message)
        { }    
    }
}