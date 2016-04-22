Feature: 2.2 Query Document

Document
Definitionlist
Definition
OperationDefinition
FragmentDefinition
A GraphQL query document describes a complete file or request string received by a GraphQL service. A document contains multiple definitions of Operations and Fragments. GraphQL query documents are only executable by a server if they contain an operation. However documents which do not contain operations may still be parsed and validated to allow client to represent a single request across many documents.

If a document contains only one operation, that operation may be unnamed or represented in the shorthand form, which omits both the query keyword and operation name. Otherwise, if a GraphQL query document contains multiple operations, each operation must be named. When submitting a query document with multiple operations to a GraphQL service, the name of the desired operation to be executed must also be provided.
