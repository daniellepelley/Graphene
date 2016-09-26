using System;
using System.Collections;
using System.Collections.Generic;

namespace Graphene.Example.Data.EntityFramework
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public Address Address { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}