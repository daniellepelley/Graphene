namespace Graphene.Core.Model
{
    public class Field : IHasSelections
    {
        public Field()
        {
            Selections = new Selection[0];
            Directives = new Directive[0];
            Arguments = new Argument[0];
        }

        public string Name { get; set; }
        public string Alias { get; set; }
        public Selection[] Selections { get; set; }
        public Directive[] Directives { get; set; }
        public Argument[] Arguments { get; set; }
    }
}