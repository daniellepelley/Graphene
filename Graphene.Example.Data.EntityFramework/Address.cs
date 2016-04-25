namespace Graphene.Example.Data.EntityFramework
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string SecondaryAddress { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
    }
}