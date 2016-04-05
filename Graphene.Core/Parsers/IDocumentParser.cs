using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public interface IDocumentParser
    {
        Document Parse(string query);
    }
}