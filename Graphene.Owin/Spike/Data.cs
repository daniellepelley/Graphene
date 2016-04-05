using System.Linq;

namespace Graphene.Owin.Spike
{
    public static class Data
    {
        public static IQueryable<TestUser> GetData()
        {
            return new[]
            {
                new TestUser
                {
                    Id = "1",
                    Name = "Dan",
                    Age = 20,
                    Boss = new TestUser
                    {
                        Id = "5",
                        Name = "Bill",
                        Age = 50
                    }
                },
                new TestUser
                {
                    Id = "2",
                    Name = "Lee",
                    Age = 30
                },
                new TestUser
                {
                    Id = "3",
                    Name = "Nick",
                    Age = 40
                }
            }.AsQueryable();
        }
    }
}