using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class FragmentProcessor
    {
        public void Process(Document document, bool deleteFragments = false)
        {
            foreach (var fragment in document.Fragments)
            {
                Process(fragment, document.Fragments);
            }

            foreach (var operation in document.Operations)
            {
                Process(operation, document.Fragments);
            }

            if (deleteFragments)
            {
                document.Fragments = new Fragment[0];
            }
        }

        private static void Process(IHasSelections hasSelections, Fragment[] fragments)
        {
            if (hasSelections.Selections == null)
            {
                return;
            }

            var selections = new List<Selection>();

            foreach (var selection in hasSelections.Selections)
            {
                Process(selection.Field, fragments);

                if (selection.Field.Name.StartsWith("..."))
                {
                    var fragment = fragments.FirstOrDefault(x => x.Name == selection.Field.Name.Replace("...", string.Empty));

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

            hasSelections.Selections = selections.ToArray();
        }
    }
}