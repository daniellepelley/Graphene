Feature: 2.2 Query Document

OperationDefinition
OperationTypeNameoptVariableDefinitionsoptDirectivesoptSelectionSet
SelectionSet
OperationType
query	mutation
There are two types of operations that GraphQL models:

query – a read‐only fetch.
mutation – a write followed by a fetch.
Each operation is represented by an optional operation name and a selection set.

For example, this mutation operation might “like” a story and then retrieve the new number of likes:

mutation {
  likeStory(storyID: 12345) {
    story {
      likeCount
    }
  }
}
Query shorthand

If a document contains only one query operation, and that query defines no variables and contains no directives, that operation may be represented in a short‐hand form which omits the query keyword and query name.

For example, this unnamed query operation is written via query shorthand.

{
  field
}
many examples below will use the query short‐hand syntax.

