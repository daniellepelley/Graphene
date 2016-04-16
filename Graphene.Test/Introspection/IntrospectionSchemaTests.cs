using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Graphene.Schema;
using Graphene.Test.Execution;
using Newtonsoft.Json;
using NUnit.Framework;
using GraphQLSchema = Graphene.Core.Types.GraphQLSchema;

namespace Graphene.Test.Introspection
{

    public class GraphiqlTest
    {
        private string query = @"{""query"":""
                query IntrospectionQuery {
    __schema {
      queryType { name }
      mutationType { name }
      subscriptionType { name }
      types {
        ...FullType
      }
      directives {
        name
        description
        args {
          ...InputValue
        }
        onOperation
        onFragment
        onField
      }
    }
  }

  fragment FullType on __Type {
    kind
    name
    description
    fields(includeDeprecated: true) {
      name
      description
      args {
        ...InputValue
      }
      type {
        ...TypeRef
      }
      isDeprecated
      deprecationReason
    }
    inputFields {
      ...InputValue
    }
    interfaces {
      ...TypeRef
    }
    enumValues(includeDeprecated: true) {
      name
      description
      isDeprecated
      deprecationReason
    }
    possibleTypes {
      ...TypeRef
    }
  }

  fragment InputValue on __InputValue {
    name
    description
    type { ...TypeRef }
    defaultValue
  }

  fragment TypeRef on __Type {
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
  }"",""variables"":null}";


        [Test]
        public void StringDescription()
        {
            var queryObject = JsonConvert.DeserializeObject<QueryObject>(query);

            var document = new DocumentParser().Parse(queryObject.Query);

            //var result = new ExecutionEngine().Execute(new __Schema(), );
            
        }


    }

    public class QueryObject
    {
        public string Query { get; set; }
        public string Variables { get; set; }
    }
}