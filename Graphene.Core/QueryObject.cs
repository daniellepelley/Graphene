using System.Collections.Generic;

namespace Graphene.Core
{
    public class QueryObject
    {
        public string Name { get; set; }
        public List<QueryField> Fields { get; set; }
        public List<QueryFieldArgs> Args { get; set; }        
    }

    public class QueryField
    {
        public string Name { get; set; }
    }

    public class QueryFieldArgs
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }
}