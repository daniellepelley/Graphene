namespace Graphene.Core.Model
{
    public class Fragment
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Directive[] Directives { get; set; }
        public Selection[] Selections { get; set; }
    }
}