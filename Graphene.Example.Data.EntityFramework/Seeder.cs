namespace Graphene.Example.Data.EntityFramework
{
    public class Seeder
    {
        public void Seed(GraphQLDemoDataContext context)
        {
            20.Times(() =>
                context.Persons.Add(new Person
                {
                    Firstname = Faker.Name.First(),
                    Lastname = Faker.Name.Last(),
                    Address = CreateAddress()
                })
                );

            context.SaveChanges();

            20.Times(() =>
                context.Companies.Add(new Company
                {
                    Name = Faker.Company.Name(),
                    CatchPhrase = Faker.Company.CatchPhrase(),
                    Address = CreateAddress()
                })
                );

            context.SaveChanges();
        }

        private static Address CreateAddress()
        {
            return new Address
            {
                StreetAddress = Faker.Address.StreetAddress(),
                SecondaryAddress = Faker.Address.SecondaryAddress(),
                StreetName = Faker.Address.StreetName(),
                City  = Faker.Address.City(),
                Country  = Faker.Address.UkCountry(),
                County = Faker.Address.UkCounty(),
                PostCode = Faker.Address.UkPostCode().ToUpper()
            };
        }
    }
}