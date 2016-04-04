namespace Graphene.Core
{
    public class Operation
    {
        public string Name { get; set; }

        public Directive[] Directives { get; set; }

        public Selection[] Selections { get; set; }
    }
}