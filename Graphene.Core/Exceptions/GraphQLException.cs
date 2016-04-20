using System;

namespace Graphene.Core.Exceptions
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

    public static class ExceptionMessages
    {
        public const string FieldsOnObjectsMustBeUniquelyNamed = "Fields on objects must be uniquely named";

    }
}