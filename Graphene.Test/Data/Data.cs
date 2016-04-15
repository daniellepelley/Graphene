using System.Linq;
using Graphene.Test.Spike;

namespace Graphene.Test.Data
{
    public static class Data
    {
        public static IQueryable<User> GetData()
        {
            return GetTestUsersFromDatabase().Select(Map).AsQueryable();
        }

        private static User Map(TestUserDatabase testUserDatabase)
        {
            if (testUserDatabase == null)
            {
                return null;
            }

            return new User
            {
                Id = testUserDatabase.Id,
                Name = testUserDatabase.Firstname + "_" + testUserDatabase.Lastname,
                Boss = MapBoss(testUserDatabase.Boss)
            };
        }

        private static Boss MapBoss(TestUserDatabase testUserDatabase)
        {
            if (testUserDatabase == null)
            {
                return null;
            }

            return new Boss
            {
                Id = testUserDatabase.Id,
                Name = testUserDatabase.Firstname + "_" + testUserDatabase.Lastname,
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