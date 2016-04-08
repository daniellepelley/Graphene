namespace Graphene.Test.Spike
{
    public class TestUserDatabase
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public TestUserDatabase Boss { get; set; }
    }
}