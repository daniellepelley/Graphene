namespace Graphene.Core.Constants
{
    public static class GraphQLKinds
    {
        public const string Scalar = "SCALAR";
        public const string Object = "OBJECT";
        public const string Interface = "INTERFACE";
        public const string Union = "UNION";
        public const string List = "LIST";
        public const string NonNull = "NON_NULL";
        public const string Enum = "ENUM";
        public const string InputObject = "INPUT_OBJECT";
    }

    public static class GraphQLTypes
    {
        public const string String = "String";
        public const string Boolean = "Boolean";
        public const string Float = "Float";
        public const string Int = "Int";

        public const string NonNull = "NonNull";
        public const string List = "List";

        public const string __Type = "__Type";
    }
}