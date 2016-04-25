using System.Data.Entity;

namespace Graphene.Example.Data.EntityFramework
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
    }
}