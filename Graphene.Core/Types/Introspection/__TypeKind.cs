using System.Security.Cryptography.X509Certificates;

namespace Graphene.Core.Types.Introspection
{
    public class __TypeKind : GraphQLEnum<IGraphQLKind>
    {
        public __TypeKind()
        {
            Name = "__TypeKind";
            Description = @"An enum describing what kind of type a given `__Type` is.";

            Values = new IGraphQLKind[]
            {
                new GraphQLKind
                {
                    Name = GraphQLKinds.Scalar,
                    Description = "Indicates this type is a scalar."
                },
                new GraphQLKind
                {
                    Name = GraphQLKinds.Object,
                    Description = "Indicates this type is an object. `fields` and `interfaces` are valid fields."
                },
                new GraphQLKind
                {
                    Name = GraphQLKinds.Interface,
                    Description = "Indicates this type is an interface. `fields` and `possibleTypes` are valid fields."
                },
                new GraphQLKind
                {
                    Name = GraphQLKinds.Union,
                    Description = "Indicates this type is a union. `possibleTypes` is a valid field."
                },
                new GraphQLKind
                {
                    Name = GraphQLKinds.Enum,
                    Description = "Indicates this type is an enum. `enumValues` is a valid field."
                },
                new GraphQLKind
                {
                    Name = GraphQLKinds.InputObject,
                    Description = "Indicates this type is an input object. `inputFields` is a valid field."
                },
                new GraphQLKind
                {
                    Name = GraphQLKinds.List,
                    Description = "Indicates this type is a list. `ofType` is a valid field."
                },
                new GraphQLKind
                {
                    Name = GraphQLKinds.NonNull,
                    Description = "Indicates this type is a non-null. `ofType` is a valid field."
                }
            };
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

    public interface IGraphQLKind
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public class GraphQLKind : IGraphQLKind
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}