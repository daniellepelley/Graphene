using System;
using System.Collections.Generic;
using Graphene.Core.FieldTypes;
using Graphene.Core.Parsers;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class GraphQLArgument : IGraphQLArgument
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType Type { get; set; }
        public string DefaultValue { get; set; }
    }

    public class Directive
    {
        public Directive()
        {
            
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IGraphQLArgument> Arguments { get; set; }
        public bool OnOperation { get; set; }
        public bool OnFragment { get; set; }
        public bool OnField { get; set; }
    }
}