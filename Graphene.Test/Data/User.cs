namespace Graphene.Test.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boss Boss { get; set; }
        public string[] Things { get; set; }
    }
}