using System.Linq;

namespace Graphene.Test.Spike
{
    public class TestUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TestUser Boss { get; set; }
    }

    public static class Data
    {
        public static IQueryable<TestUser> GetData()
        {
            return GetTestUsersFromDatabase().Select(Map).AsQueryable();
        }

        private static TestUser Map(TestUserDatabase testUserDatabase)
        {
            if (testUserDatabase == null)
            {
                return null;
            }

            return new TestUser
            {
                Id = testUserDatabase.Id,
                Name = testUserDatabase.Firstname + "_" + testUserDatabase.Lastname,
                Boss = Map(testUserDatabase.Boss)
            };
        }

        public static IQueryable<TestUserDatabase> GetTestUsersFromDatabase()
        {
            return new[]
            {
                new TestUserDatabase
                {
                    Id = 1,
                    Firstname= "Dan",
                    Lastname = "Smith",
                    Boss = new TestUserDatabase
                    {
                        Id = 4,
                        Firstname= "Boss",
                        Lastname = "Smith"
                   }
                },
                new TestUserDatabase
                {
                    Id = 2,
                    Firstname= "Lee",
                    Lastname = "Smith",
                    Boss = new TestUserDatabase
                    {
                        Id = 4,
                        Firstname= "Boss",
                        Lastname = "Lee"
                   }
                },
                new TestUserDatabase
                {
                    Id = 3,
                    Firstname= "Nick",
                    Lastname = "Smith"
                }
            }.AsQueryable();

        }
    }
}