namespace Graphene.Core.Types.Introspection
{
    public class __TypeKind : IGraphObjectType
    {
        public string Kind { get; private set; }

        public string Name
        {
            get { return "__TypeKind"; }
        }

        public string Description
        {
            get
            {
                return @"An enum describing what kind of type a given `__Type` is.";
            }
        }

        //values: {
        //  SCALAR: {
        //    value: TypeKind.SCALAR,
        //    description: 'Indicates this type is a scalar.'
        //  },
        //  OBJECT: {
        //    value: TypeKind.OBJECT,
        //    description: 'Indicates this type is an object. ' +
        //                 '`fields` and `interfaces` are valid fields.'
        //  },
        //  INTERFACE: {
        //    value: TypeKind.INTERFACE,
        //    description: 'Indicates this type is an interface. ' +
        //                 '`fields` and `possibleTypes` are valid fields.'
        //  },
        //  UNION: {
        //    value: TypeKind.UNION,
        //    description: 'Indicates this type is a union. ' +
        //                 '`possibleTypes` is a valid field.'
        //  },
        //  ENUM: {
        //    value: TypeKind.ENUM,
        //    description: 'Indicates this type is an enum. ' +
        //                 '`enumValues` is a valid field.'
        //  },
        //  INPUT_OBJECT: {
        //    value: TypeKind.INPUT_OBJECT,
        //    description: 'Indicates this type is an input object. ' +
        //                 '`inputFields` is a valid field.'
        //  },
        //  LIST: {
        //    value: TypeKind.LIST,
        //    description: 'Indicates this type is a list. ' +
        //                 '`ofType` is a valid field.'
        //  },
        //  NON_NULL: {
        //    value: TypeKind.NON_NULL,
        //    description: 'Indicates this type is a non-null. ' +
        //                 '`ofType` is a valid field.'
        //  },
    }
}