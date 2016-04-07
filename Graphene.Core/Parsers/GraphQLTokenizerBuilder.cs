using System.Collections.Generic;

namespace Graphene.Core.Parsers
{
    public class GraphQLTokenizerBuilder
    {
        private List<IGraphQLTokenizer> _tokenizers;

        public List<IGraphQLTokenizer> Build()
        {
            _tokenizers = new List<IGraphQLTokenizer>();

            AddToken("!", GraphQLTokenType.Bang);
            AddToken("$", GraphQLTokenType.Dollar);
            AddToken("(", GraphQLTokenType.ParenL);
            AddToken(")", GraphQLTokenType.ParenR);
            AddToken("...", GraphQLTokenType.Spread);
            AddToken(":", GraphQLTokenType.Colon);
            AddToken("=", GraphQLTokenType.Eq);
            AddToken("@", GraphQLTokenType.At);
            AddToken("[", GraphQLTokenType.BracketL);
            AddToken("]", GraphQLTokenType.BracketR);;
            AddToken("{", GraphQLTokenType.BraceL);
            AddToken("|", GraphQLTokenType.Pipe);
            AddToken("}", GraphQLTokenType.BraceR);
            AddMultipleToken("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_", GraphQLTokenType.Name);
            AddToken("Int", GraphQLTokenType.Int);
            AddToken("Float", GraphQLTokenType.Float);
            AddToken("String", GraphQLTokenType.String);
            AddToken("," + (char)10 + (char)13, GraphQLTokenType.Seperator);
            return _tokenizers;
        }

        public void AddToken(string characters, string tokenType)
        {
            _tokenizers.Add(new SingleGraphQLTokenizer
            {
                Characters = characters,
                TokenType = tokenType
            });
        }

        public void AddMultipleToken(string characters, string tokenType)
        {
            _tokenizers.Add(new MultipleGraphQLTokenizer
            {
                Characters = characters,
                TokenType = tokenType
            });
        }
    }
}