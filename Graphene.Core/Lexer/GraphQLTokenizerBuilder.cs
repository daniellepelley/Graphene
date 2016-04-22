using System.Collections.Generic;
using System.Text;

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
                Characters = Encoding.ASCII.GetBytes(" " + (char)13 + (char)10)
            });

            AddToken("{", GraphQLTokenType.BraceL);
            AddToken("}", GraphQLTokenType.BraceR);
            AddToken("(", GraphQLTokenType.ParenL);
            AddToken(")", GraphQLTokenType.ParenR);
            AddMultipleToken(@"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_""", GraphQLTokenType.Name);
            AddToken("...", GraphQLTokenType.Spread);
            AddToken(":", GraphQLTokenType.Colon);
            AddToken("Int", GraphQLTokenType.Int);
            AddToken("Float", GraphQLTokenType.Float);
            AddToken("String", GraphQLTokenType.String);

            AddToken("!", GraphQLTokenType.Bang);
            AddToken("$", GraphQLTokenType.Dollar);
            AddToken("=", GraphQLTokenType.Eq);
            AddToken("@", GraphQLTokenType.At);
            AddToken("[", GraphQLTokenType.BracketL);
            AddToken("]", GraphQLTokenType.BracketR);;
            AddToken("|", GraphQLTokenType.Pipe);
            _tokenizers.Add(new CommentGraphQLTokenizer());

            return _tokenizers;
        }

        private void AddToken(string characters, string tokenType)
        {
            _tokenizers.Add(new SingleGraphQLTokenizer
            {
                Characters = Encoding.ASCII.GetBytes(characters),
                TokenType = tokenType
            });
        }

        private void AddMultipleToken(string characters, string tokenType)
        {
            _tokenizers.Add(new MultipleGraphQLTokenizer
            {
                Characters = Encoding.ASCII.GetBytes(characters),
                TokenType = tokenType
            });
        }
    }
}