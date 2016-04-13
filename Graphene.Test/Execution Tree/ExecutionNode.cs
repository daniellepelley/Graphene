using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Test.Execution_Tree
{
    public class ExecutionNodeTests
    {

        public void Test1()
        {
            var field = new Field
            {
                Name = "field1"
            };
            var scalar = new GraphQLScalar
            {

            };

            //Do something to get an execution node
            //Execute the node with an object to get a scalar value


        }
    }

    public class ExecutionNode
    {
        public object Execute(object source)
        {
            //Return field from source
            return null;
        }
    }
}
