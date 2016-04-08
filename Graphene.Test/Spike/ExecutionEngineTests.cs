using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Schema;
using Graphene.Spike;
using NUnit.Framework;
using NUnit.Framework.Compatibility;
using GraphQLFieldType = Graphene.Core.Types.GraphQLFieldType;
using GraphQLSchema = Graphene.Core.Types.GraphQLSchema;

namespace Graphene.Test.Spike
{
    public class ExecutionSpikeTests
    {
        [Test]
        public void QueryUserNames()
        {
            var query = "{user{Name}}";
             
            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Name"":""Dan_Smith""},{""Name"":""Lee_Smith""},{""Name"":""Nick_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void QueryUserNamesAndIds()
        {
            var query = "{user {Id, Name}}";

            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Id"":1,""Name"":""Dan_Smith""},{""Id"":2,""Name"":""Lee_Smith""},{""Id"":3,""Name"":""Nick_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        //[Test]
        //public void QueryUserNamesAndIdsFiltedById()
        //{
        //    var query = "{user(Id: 1){Id,Name}}";

        //    var document = new DocumentParser().Parse(query);
        //    var json = ExecuteQuery(document);
        //    var expected = @"[{""Id"":1,""Name"":""Dan_Smith""}]";

        //    Assert.AreEqual(expected, json);
        //}

        //[Test]
        //public void QueryUserNamesAndIdsFiltedByNameAndId()
        //{
        //    var query = @"{user(Id: 1, Name: Dan_Smith){Id,Name}}";

        //    var document = new DocumentParser().Parse(query);
        //    var json = ExecuteQuery(document);
        //    var expected = @"[{""Id"":1,""Name"":""Dan_Smith""}]";

        //    Assert.AreEqual(expected, json);
        //}

        //[Test]
        //public void QueryUserNamesAndIdsFiltedByNameAndIdReturnsNone()
        //{
        //    var query = @"{user(Id: 2, Name: Dan_Smith){Id,Name}}";

        //    var document = new DocumentParser().Parse(query);
        //    var json = ExecuteQuery(document);
        //    var expected = @"[]";

        //    Assert.AreEqual(expected, json);
        //}

        [Test]
        public void QueryUserNamesAndIdsFiltedByName()
        {
            var query = @"{user(Name: Dan_Smith){Id,Name}}";

            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Id"":1,""Name"":""Dan_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        private string ExecuteQuery(GraphQLSchema schema, Document document)
        {
            var executionEngine = new ExecutionEngine();
            return executionEngine.Execute(schema, document);
        }

        private string ExecuteQuery(Document document)
        {
            var executionEngine = new SpikeExecutionEngine<TestUser>(Data.GetData());
            return executionEngine.Execute(null, document);
        }

        [Test]
        [Ignore("Speed Test")]
        public void IsFast()
        {
            var query = "{ user(id: 1) {Id, Name, birthday}}";

            var document = new DocumentParser().Parse(query);
            ExecuteQuery(document);

            var documentStopWatch = new Stopwatch();
            documentStopWatch.Start();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                new DocumentParser().Parse(query);
            }
            documentStopWatch.Stop();

            var executeStopWatch = new Stopwatch();
            executeStopWatch.Start();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                ExecuteQuery(document);
            }
            executeStopWatch.Stop();

            Assert.Less(executeStopWatch.ElapsedMilliseconds, 1000);
            Assert.Less(documentStopWatch.ElapsedMilliseconds, 50);
        }
    }
}