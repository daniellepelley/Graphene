namespace Graphene.Owin.Spike
{
    public class TestUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public TestUser Boss { get; set; }
    }
}