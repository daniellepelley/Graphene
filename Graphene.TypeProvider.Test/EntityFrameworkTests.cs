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

            var typeList = new TypeList();

            sut.Create(typeof(Company), typeList);

            var result = typeList.LookUpType("Company") as GraphQLObjectType;

            Assert.AreEqual("Id", result.Fields.ElementAt(0).Name);
            Assert.AreEqual("Name", result.Fields.ElementAt(1).Name);
            Assert.AreEqual("CatchPhrase", result.Fields.ElementAt(2).Name);
            Assert.AreEqual("Address", result.Fields.ElementAt(3).Name);
        }

        [Test]
        public void Resolve()
        {
            var sut = new FieldBuilder();

            var typeList = new TypeList();
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