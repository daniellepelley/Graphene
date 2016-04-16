namespace Graphene.Core.Model
{
    public class Operation : IHasSelections
    {
        public string Name { get; set; }
        public Directive[] Directives { get; set; }
        public Selection[] Selections { get; set; }
    }

    public interface IHasSelections
    {
        Selection[] Selections { get; set; }
    }
}