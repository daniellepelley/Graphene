﻿using System;
using Graphene.Core.Types;
using Graphene.Test.Spike;
using NUnit.Framework;

namespace Graphene.Test.Execution_Tree
{
    public class ExecutionNodeTests
    {
        [Test]
        public void ExecutionNodeTest()
        {
            var scalar1 = new GraphQLScalar<TestUser, string>
            {
                Name = "field1",
                Resolve = x => x.Source.Name
            };

            var scalar2 = new GraphQLScalar<TestUser, int>
            {
                Name = "field2",
                Resolve = x => x.Source.Id
            };
            var getter = new Func<TestUser>(() =>
                new TestUser {
                    Id = 42,
                    Name = "Dan"
                });

            var executionNode1 = scalar1.ToExecutionNode(getter);
            var executionNode2 = scalar2.ToExecutionNode(getter);

            var actual1 = executionNode1.Execute();
            var actual2 = executionNode2.Execute();

            Assert.AreEqual("field1", actual1.Key);
            Assert.AreEqual("Dan", actual1.Value);

            Assert.AreEqual("field2", actual2.Key);
            Assert.AreEqual(42, actual2.Value);
        }
    }
}