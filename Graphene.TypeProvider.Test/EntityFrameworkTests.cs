using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Graphene.Core;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Example.Data.EntityFramework;
using NUnit.Framework;

namespace Graphene.TypeProvider.Test
{
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
            Assert.AreEqual("Int", id.Type.Name);

            var name = result.Fields.ElementAt(1);
            Assert.AreEqual("Name", name.Name);
            Assert.AreEqual("String", name.Type.Name);

            var catchPhrase = result.Fields.ElementAt(2);
            Assert.AreEqual("CatchPhrase", catchPhrase.Name);
            Assert.AreEqual("String", catchPhrase.Type.Name);

            var address = result.Fields.ElementAt(3);
            Assert.AreEqual("Address", address.Name);
            Assert.AreEqual("Address", address.Type.Name);

            var addressId = address.Type.GetFields().ElementAt(0);
            Assert.AreEqual("Id", addressId.Name);
            Assert.AreEqual("Int", addressId.Type.Name);

            var addressStreetAddress = address.Type.GetFields().ElementAt(1);
            Assert.AreEqual("StreetAddress", addressStreetAddress.Name);
            Assert.AreEqual("String", addressStreetAddress.Type.Name);

            var customers = result.Fields.ElementAt(4);
            Assert.AreEqual("Customers", customers.Name);
            Assert.AreEqual("List", customers.Type.Name);
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
                CatchPhrase = "foo forever"
            };

            AssertResolve(result, 0, company, 42);
            AssertResolve(result, 1, company, "foo");
            AssertResolve(result, 2, company, "foo forever");
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