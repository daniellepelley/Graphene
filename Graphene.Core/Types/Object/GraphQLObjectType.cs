namespace Graphene.Core.Types.Object
{
    public class GraphQLObjectType : GraphQLObjectTypeBase
    {
        public override string Kind
        {
            get { return "OBJECT"; }
        }
    }
}