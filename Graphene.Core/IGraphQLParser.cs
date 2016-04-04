using System;
using System.Collections;
using System.Linq;

namespace Graphene.Core
{
    public interface IGraphQLParser
    {
        object Parse(string query);
    }
}