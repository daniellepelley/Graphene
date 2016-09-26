using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Graphene.Core;
using Graphene.Core.Constants;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Example.Data.EntityFramework;
using NUnit.Framework;

namespace Graphene.TypeProvider.Test
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTrue { get; set; }
        public float Number { get; set; }
        public Address Address { get; set; }
        public ICollection<Example.Data.EntityFramework.Customer> Customers { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EntityFrameworkTests
    {
        [Test]
        public void CreatesAType()
        {
            var sut = new FieldBuilder();

            var typeList = TypeList.Create();

            sut.Create(typeof(Company), typeList);

            var result = typeList.LookUpType("Company") as GraphQLObjectType;

            var id = result.Fields.ElementAt(0);
            Assert.AreEqual("Id", id.Name);
            Assert.AreEqual("Int", id.Type.Last());

            var name = result.Fields.ElementAt(1);
            Assert.AreEqual("Name", name.Name);
            Assert.AreEqual(GraphQLTypes.String, name.Type.Last());

            var isTrue = result.Fields.ElementAt(2);
            Assert.AreEqual("IsTrue", isTrue.Name);
            Assert.AreEqual(GraphQLTypes.Boolean, isTrue.Type.Last());

            var number = result.Fields.ElementAt(3);
            Assert.AreEqual("Number", number.Name);
            Assert.AreEqual(GraphQLTypes.Float, number.Type.Last());

            var address = result.Fields.ElementAt(4);
            Assert.AreEqual("Address", address.Name);
            Assert.AreEqual("Address", address.Type.Last());

            var addressType = typeList.LookUpType(address.Type.Last());

            var addressId = addressType.GetFields().ElementAt(0);
            Assert.AreEqual("Id", addressId.Name);
            Assert.AreEqual("Int", addressId.Type.Last());

            var addressStreetAddress = addressType.GetFields().ElementAt(1);
            Assert.AreEqual("StreetAddress", addressStreetAddress.Name);
            Assert.AreEqual(GraphQLTypes.String, addressStreetAddress.Type.Last());

            var customers = result.Fields.ElementAt(5);
            Assert.AreEqual("Customers", customers.Name);
            Assert.AreEqual(GraphQLTypes.List, customers.Type.First());
            Assert.AreEqual("Customer", customers.Type.Last());

            var customerType = typeList.LookUpType(customers.Type.Last());
            Assert.IsNotNull(customerType);
        }

        [Test]
        public void Resolve()
        {
            var sut = new FieldBuilder();

            var typeList = TypeList.Create();
            sut.Create(typeof(Company), typeList);
            var result = typeList.LookUpType("Company") as GraphQLObjectType;

            var company = new Company
            {
                Id = 42,
                Name = "foo",
                IsTrue = true,
                Number = 5.434F
            };

            AssertResolve(result, 0, company, 42);
            AssertResolve(result, 1, company, "foo");
            AssertResolve(result, 2, company, true);
            AssertResolve(result, 3, company, 5.434F);
        }


        public void AggregateRoot()
        {
            var context = new GraphQLDemoDataContext();

            var sut = new FieldBuilder();

            var typeList = new TypeList();
            sut.Create(typeof(Company), typeList);
            var result = typeList.LookUpType("Company") as GraphQLObjectType;
        }

        private static void AssertResolve<TInput, TOutput>(GraphQLObjectType result, int elementAt, TInput input, TOutput expected)
        {
            var resolve = ((GraphQLScalarField<TInput, TOutput>)result.Fields.ElementAt(elementAt)).Resolve;

            var resolveFieldContext = new ResolveFieldContext<TInput>
            {
                Source = input
            };

            var actual = resolve(resolveFieldContext);

            Assert.AreEqual(expected, actual);
        }
    }
}