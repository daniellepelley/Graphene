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
        public void FullIn()
        {
            #region Query

            var query = @"
query IntrospectionQuery {
  __schema {
    queryType { name }
    mutationType { name }
    subscriptionType { name }
    types {
      kind
      name
      description
      fields(includeDeprecated: true) {
        name
        description
        args {
          name
          description
          type {
            kind
            name
            ofType {
              kind
              name
              ofType {
                kind
                name
                ofType {
                  kind
                  name
                }
              }
            }
          }
          defaultValue
        }
        type {
          kind
          name
          ofType {
            kind
            name
            ofType {
              kind
              name
              ofType {
                kind
                name
              }
            }
          }
        }
        isDeprecated
        deprecationReason
      }
      inputFields {
        name
        description
        type {
          kind
          name
          ofType {
            kind
            name
            ofType {
              kind
              name
              ofType {
                kind
                name
              }
            }
          }
        }
        defaultValue
      }
      interfaces {
        kind
        name
        ofType {
          kind
          name
          ofType {
            kind
            name
            ofType {
              kind
              name
            }
          }
        }
      }
      enumValues(includeDeprecated: true) {
        name
        description
        isDeprecated
        deprecationReason
      }
      possibleTypes {
        kind
        name
        ofType {
          kind
          name
          ofType {
            kind
            name
            ofType {
              kind
              name
            }
          }
        }
      }
    }
    directives {
      name
      description
      args {
        name
          description
          type { kind
          name
          ofType {
            kind
            name
            ofType {
              kind
              name
              ofType {
                kind
                name
              }
            }
          }
        }
        defaultValue
      }
      onOperation
      onFragment
      onField
    }
  }
}";

            #endregion

            var document = new DocumentParser().Parse(query);


        }



        [Test]
        public void SimpleTest()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var query = @"{user(id:""1"") {...Test}}fragment Test on __Type {id,name}";
            var document = new DocumentParser().Parse(query); 

            var selections = new List<Selection>();

            foreach (var selection in document.Operations.First().Selections)
            {
                if (selection.Field.Name.StartsWith("..."))
                {
                    var fragment = document.Fragments.FirstOrDefault(x => x.Name == "Test");

                    if (fragment == null)
                    {
                        throw new Exception();
                    }
                    selections.AddRange(fragment.Selections);
                }
                else
                {
                    selections.Add(selection);
                }
            }

            document.Operations.First().Selections = selections.ToArray();

            var expected =
                @"{""data"":{""id"":1,""name"":""Dan_Smith""}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AfterTest()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var query = @"{user(id:""1"") {...Test,name}}fragment Test on __Type {id}";
            var document = new DocumentParser().Parse(query);

            var selections = new List<Selection>();

            foreach (var selection in document.Operations.First().Selections)
            {
                if (selection.Field.Name.StartsWith("..."))
                {
                    var fragment = document.Fragments.FirstOrDefault(x => x.Name == "Test");

                    if (fragment == null)
                    {
                        throw new Exception();
                    }
                    selections.AddRange(fragment.Selections);
                }
                else
                {
                    selections.Add(selection);
                }
            }

            document.Operations.First().Selections = selections.ToArray();

            var expected =
                @"{""data"":{""id"":1,""name"":""Dan_Smith""}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void BeforeTest()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var query = @"{user(id:""1"") {id ...Test}}fragment Test on __Type {name}";
            var document = new DocumentParser().Parse(query);

            var selections = new List<Selection>();

            foreach (var selection in document.Operations.First().Selections)
            {
                if (selection.Field.Name.StartsWith("..."))
                {
                    var fragment = document.Fragments.FirstOrDefault(x => x.Name == "Test");

                    if (fragment == null)
                    {
                        throw new Exception();
                    }
                    selections.AddRange(fragment.Selections);
                }
                else
                {
                    selections.Add(selection);
                }
            }

            document.Operations.First().Selections = selections.ToArray();

            var expected =
                @"{""data"":{""id"":1,""name"":""Dan_Smith""}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }


        private static object Execute(ExecutionEngine sut, GraphQLSchema schema, Document document)
        {
            return JsonConvert.SerializeObject(sut.Execute(schema, document));
        }
    }
}