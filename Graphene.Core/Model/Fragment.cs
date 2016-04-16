namespace Graphene.Core.Model
{
    public class Fragment : IHasSelections
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Directive[] Directives { get; set; }
        public Selection[] Selections { get; set; }
    }
}