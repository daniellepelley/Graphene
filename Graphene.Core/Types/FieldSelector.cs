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
            return fields.Where(field =>
                _selectors.ContainsKey(field))
                .ToDictionary(field => field, field =>
                    _selectors[field](graphQLType));
        }
    }
}