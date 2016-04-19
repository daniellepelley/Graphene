using System;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Test.Data;
using NUnit.Framework;

namespace Graphene.Test.Execution_Tree
{
    public class ExecutionNodeTests
    {
        [Test]
        public void ExecutionNodeTest()
        {
            var scalar1 = new GraphQLScalarField<User, string>
            {
                Name = "field1",
                Resolve = x => x.Source.Name
            };

            var scalar2 = new GraphQLScalarField<User, int>
            {
                Name = "field2",
                Resolve = x => x.Source.Id
            };
            var getter = new Func<User>(() =>
                new User {
                    Id = 42,
                    Name = "Dan"
                });

            var executionNode1 = scalar1.ToExecutionNode("field1", getter);
            var executionNode2 = scalar2.ToExecutionNode("fieldB", getter);

            var actual1 = executionNode1.Execute();
            var actual2 = executionNode2.Execute();

            Assert.AreEqual("field1", actual1.Key);
            Assert.AreEqual("Dan", actual1.Value);

            Assert.AreEqual("fieldB", actual2.Key);
            Assert.AreEqual(42, actual2.Value);
        }
    }
}
