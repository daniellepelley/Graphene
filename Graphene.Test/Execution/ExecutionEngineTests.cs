using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Execution;
using Graphene.Test.Spike;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Compatibility;

namespace Graphene.Test.Execution
{
    public class ExecutionEngineTests
    {
        [Test]
        public void RunsExecute()
        {
            var sut = new ExecutionEngine();

            var query = "{user {Id, Name}}";
            var document = new DocumentParser().Parse(query); ;

            var result = sut.Execute(document);
            Assert.AreEqual("json", result);
        }
    }
}