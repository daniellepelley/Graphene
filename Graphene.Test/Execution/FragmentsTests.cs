using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class FragmentsTests
    {
        [Test]
        public void SimpleTest()
        {
            var query = @"{user(id:""1"") {...Test}}fragment Test on __Type {id,name}";

            var expected = @"{""data"":{""id"":1,""name"":""Dan_Smith""}}";

            RunTest(query, expected);
        }

        private static void RunTest(string query, string expected)
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var document = new DocumentParser().Parse(query);

            ProcessFragments(document);

            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        private static void ProcessFragments(Document document)
        {
            new FragmentProcessor().Process(document);
        }

        [Test]
        public void AfterTest()
        {
            var query = @"{user(id:""1"") {...Test,name}}fragment Test on __Type {id}";
            var expected = @"{""data"":{""id"":1,""name"":""Dan_Smith""}}";
            RunTest(query, expected);
        }

        [Test]
        public void BeforeTest()
        {
            var query = @"{user(id:""1"") {id ...Test}}fragment Test on __Type {name}";
            var expected = @"{""data"":{""id"":1,""name"":""Dan_Smith""}}";
            RunTest(query, expected);
        }

        [Test]
        public void MiddleTest()
        {
            var query = @"{user(id:""1"") {id ...Test boss { ...Test }}}fragment Test on __Type {name}";

            //query = @"{user(id:""1"") {id name boss { name }}}";

            var expected = @"{""data"":{""id"":1,""name"":""Dan_Smith"",""boss"":{""name"":""Boss_Smith""}}}";
            RunTest(query, expected);
        }

        private static object Execute(ExecutionEngine sut, GraphQLSchema schema, Document document)
        {
            return JsonConvert.SerializeObject(sut.Execute(schema, document));
        }
    }
}