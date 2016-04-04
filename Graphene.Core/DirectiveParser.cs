﻿using System.Linq;
using System.Text;

namespace Graphene.Core
{
    public class DirectiveParser
    {
        private Directive _directive;
        //private StringBuilder _stringBuilder;
        private ParsedPart _current;

        public Directive Parse(ParserFeed parserFeed)
        {
            _directive = new Directive();

            while (!parserFeed.IsComplete())
            {
                _current = parserFeed.Next();

                if (_current.ParseType == ParseType.Name)
                {
                    _directive.Name = _current.Value;
                }
                else if (_current.ParseType == ParseType.Open)
                {
                    if (_current.Value == "(")
                    {
                        _directive.Arguments = new ArgumentsParser().GetArguments(parserFeed);
                    }
                    else if (!string.IsNullOrEmpty(_directive.Name))
                    {
                        return _directive;
                    }
                }
            }
            return _directive;
        }
    }
}