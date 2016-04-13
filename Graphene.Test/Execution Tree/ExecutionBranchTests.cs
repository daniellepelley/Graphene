using System;
using System.Collections.Generic;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Spike;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution_Tree
{
    public class ExecutionBranchTests
    {
        [Test]
        public void ExecutionNodeTest()
        {
            var getter = new Func<TestUser>(() =>
                new TestUser
                {
                    Id = 42,
                    Name = "Dan"
                });

            var sut = new ExecutionBranch<TestUser>("user", getter);
            
            var scalar1 = new GraphQLScalar<TestUser, string>
            {
                Name = "field1",
                Resolve = x => x.Name
            };

            var scalar2 = new GraphQLScalar<TestUser, int>
            {
                Name = "field2",
                Resolve = x => x.Id
            };

            var executionNode1 = scalar1.ToExecutionNode(sut.GetOutput);
            var executionNode2 = scalar2.ToExecutionNode(sut.GetOutput);

            sut.AddNode(executionNode1);
            sut.AddNode(executionNode2);

            var actual = sut.Execute().Value as IDictionary<string, object>;
             
            Assert.AreEqual("Dan", actual["field1"]);
            Assert.AreEqual(42, actual["field2"]);
        }

        [Test]
        public void ExecutionNodeTestNested()
        {
            var getter = new Func<TestUser>(() =>
                new TestUser
                {
                    Id = 42,
                    Name = "Dan",
                    Boss = new Boss
                    {
                        Name = "Boss Man",
                        Id = 55
                    }
                });

            var generator = new ExecutionBranch<TestUser>("user", getter);

            var scalar1 = new GraphQLScalar<TestUser, string>
            {
                Name = "field1",
                Resolve = x => x.Name
            };

            var scalar2 = new GraphQLScalar<TestUser, int>
            {
                Name = "field2",
                Resolve = x => x.Id
            };

            var scalar3 = new GraphQLScalar<Boss, string>
            {
                Name = "field3",
                Resolve = x => x.Name
            };

            var branch = new ExecutionBranch<TestUser, Boss>("boss", testUser => testUser.Boss, generator.GetOutput);

            var executionNode1 = scalar1.ToExecutionNode(generator.GetOutput);
            var executionNode2 = scalar2.ToExecutionNode(generator.GetOutput);
            var executionNode3 = scalar3.ToExecutionNode(branch.GetOutput);

            generator.AddNode(executionNode1);
            generator.AddNode(executionNode2);
            generator.AddNode(branch);

            branch.AddNode(executionNode3);
            
            var actual = generator.Execute();

            var result = new Dictionary<string, object> {{actual.Key, actual.Value}};
             
            var json = JsonConvert.SerializeObject(result);

            Assert.AreEqual(@"{""user"":{""field1"":""Dan"",""field2"":42,""boss"":{""field3"":""Boss Man""}}}", json);
        }

    }
}