namespace Graphene.Core.Model
{
    public class Field : IHasSelections
    {
        public string Name { get; set; }
        public Selection[] Selections { get; set; }
    }
}