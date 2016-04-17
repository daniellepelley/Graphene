using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ExecutionBranchBuilder
    {
        public ExecutionBranch Build(IToExecutionBranch graphQLType, Field field)
        {
            var generator = graphQLType.ToExecutionBranch(field);
            return generator;
        }
    }
}