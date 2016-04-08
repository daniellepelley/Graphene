using System.Linq;

namespace Graphene.Test.Spike
{
    public class TestUser
    {
        public static IQueryable<TestUser> GetData()
        {
            return GetTestUsersFromDatabase().Select(x =>
                new TestUser
                {
                    Id = x.Id,
                    Name = x.Firstname + "_" + x.Lastname
                });
        }

        public static IQueryable<TestUserDatabase> GetTestUsersFromDatabase()
        {
            return new[]
            {
                new TestUserDatabase
                {
                    Id = "1",
                    Firstname= "Dan",
                    Lastname = "Smith"
                },
                new TestUserDatabase
                {
                    Id = "2",
                    Firstname= "Lee",
                    Lastname = "Smith"
                },
                new TestUserDatabase
                {
                    Id = "3",
                    Firstname= "Nick",
                    Lastname = "Smith"
                }
            }.AsQueryable();

        }


        public string Id { get; set; }
        public string Name { get; set; }
    }
}