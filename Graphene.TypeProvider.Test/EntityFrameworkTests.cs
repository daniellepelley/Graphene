using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Graphene.Core.FieldTypes;
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
            var context = new GraphQLDemoDataContext();

            var result = Create<Company>();

            Assert.AreEqual("Name", result.Fields.ElementAt(1).Name);

        }

        public GraphQLObjectType Create<T>()
        {
            var output = new GraphQLObjectType();

            var list = new List<IGraphQLFieldType>();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var field = new GraphQLScalarField<T, string>();
                field.Name = propertyInfo.Name;
                list.Add(field);
            }

            return new GraphQLObjectType
            {
                Fields = list
            };
        }


    }
}