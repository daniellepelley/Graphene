using System.Linq;

namespace Graphene.Web.Models
{
    public static class Data
    {
        public static IQueryable<TestUser> GetData()
        {
            return new[]
            {
                new TestUser
                {
                    Id = 1,
                    Name = "Dan",
                    Age = 20,
                    Boss = new TestUser
                    {
                        Id = 5,
                        Name = "Bill",
                        Age = 50
                    }
                },
                new TestUser
                {
                    Id = 2,
                    Name = "Lee",
                    Age = 30
                },
                new TestUser
                {
                    Id = 3,
                    Name = "Nick",
                    Age = 40
                },
                new TestUser
                {
                    Id = 4,
                    Name = "Steve",
                    Age = 27,
                    Boss = new TestUser
                    {
                        Id = 6,
                        Name = "Mike",
                        Age = 54
                    }
                },
                new TestUser
                {
                    Id = 7,
                    Name = "Ian",
                    Age = 34
                },
                new TestUser
                {
                    Id = 8,
                    Name = "Brian",
                    Age = 40
                }
            }.AsQueryable();
        }
    }
}