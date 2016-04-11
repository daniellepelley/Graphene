using System;

namespace Graphene.Execution
{
    public class GraphQLException : Exception
    {
        public GraphQLException(string message)
            :base(message)
        { }    
    }
}