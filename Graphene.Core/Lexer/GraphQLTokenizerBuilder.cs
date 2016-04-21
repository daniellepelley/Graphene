using System.Collections.Generic;

namespace Graphene.Core.Lexer
{
    public class GraphQLTokenizerBuilder
    {
        private List<IGraphQLTokenizer> _tokenizers;

        public List<IGraphQLTokenizer> Build()
        {
            _tokenizers = new List<IGraphQLTokenizer>();

            _tokenizers.Add(new IgnoreGraphQLTokenizer
            {
                Characters = " " + (char)13
            });
            AddToken("{", GraphQLTokenType.BraceL);
            AddToken("}", GraphQLTokenType.BraceR);
            AddToken("(", GraphQLTokenType.ParenL);
            AddToken(")", GraphQLTokenType.ParenR);
            AddToken("," + (char)10 + (char)13, GraphQLTokenType.Seperator);
            AddMultipleToken(@"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_""", GraphQLTokenType.Name);
            AddToken("!", GraphQLTokenType.Bang);
            AddToken("$", GraphQLTokenType.Dollar);
            AddToken("...", GraphQLTokenType.Spread);
            AddToken(":", GraphQLTokenType.Colon);
            AddToken("=", GraphQLTokenType.Eq);
            AddToken("@", GraphQLTokenType.At);
            AddToken("[", GraphQLTokenType.BracketL);
            AddToken("]", GraphQLTokenType.BracketR);;
            AddToken("|", GraphQLTokenType.Pipe);
            AddToken("Int", GraphQLTokenType.Int);
            AddToken("Float", GraphQLTokenType.Float);
            AddToken("String", GraphQLTokenType.String);
            return _tokenizers;
        }

        private void AddToken(string characters, string tokenType)
        {
            _tokenizers.Add(new SingleGraphQLTokenizer
            {
                Characters = characters,
                TokenType = tokenType
            });
        }

        private void AddMultipleToken(string characters, string tokenType)
        {
            _tokenizers.Add(new MultipleGraphQLTokenizer
            {
                Characters = characters,
                TokenType = tokenType
            });
        }
    }
}