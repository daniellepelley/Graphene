namespace Graphene.Example.Data.EntityFramework
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public Address Address { get; set; }
    }
}