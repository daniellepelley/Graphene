using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types
{
    public abstract class FieldSelector<T> 
    {
        private IDictionary<string, Func<T, string>> _selectors;

        protected void SetUp(IDictionary<string, Func<T, string>> selectors)
        {
            _selectors = selectors;            
        }

        public IDictionary<string, string> Process(T graphQLType, string[] fields)
        {
            return _selectors.Where(selector => 
                fields.Contains(selector.Key))
                .ToDictionary(selector => selector.Key, selector => selector.Value(graphQLType));
        }
    }
}