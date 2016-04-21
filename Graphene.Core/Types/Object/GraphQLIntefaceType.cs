namespace Graphene.Core.Types.Object
{
    public class GraphQLIntefaceType : GraphQLObjectTypeBase
    {
        public override string Kind
        {
            get { return "INTERFACE"; }
        }
    }
}