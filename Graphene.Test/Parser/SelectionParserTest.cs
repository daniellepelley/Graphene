using System.Linq;
using System.Text;
using Graphene.Core.Lexer;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class SelectionParserTest
    {
        [Test]
        public void OneSelection()
        {
            var sut = new SelectionsParser();
            var result = sut.Parse(new GraphQLLexerFeed(@"{name}"));
            Assert.AreEqual("name", result.First().Field.Name);
            Assert.AreEqual(0, result.First().Field.Arguments.Length);
        }

        [Test]
        public void TwoSelections()
        {
            var sut = new SelectionsParser();
            var result = sut.Parse(new GraphQLLexerFeed(@"{name,age}"));
            Assert.AreEqual("name", result.ElementAt(0).Field.Name);
            Assert.AreEqual("age", result.ElementAt(1).Field.Name);
            Assert.AreEqual(0, result.First().Field.Arguments.Length);
        }

        [Test]
        public void OneSelectionWithOneArgument()
        {
            var sut = new SelectionsParser();
            var result = sut.Parse(new GraphQLLexerFeed(@"{name(id: 1)}"));
            Assert.AreEqual("name", result.First().Field.Name);
            Assert.AreEqual(1, result.First().Field.Arguments.Length);
            Assert.AreEqual("id", result.First().Field.Arguments.First().Name);
            Assert.AreEqual(1, result.First().Field.Arguments.First().Value);
        }

        [Test]
        public void OneSelectionWithTwoArgument()
        {
            var sut = new SelectionsParser();
            var result = sut.Parse(new GraphQLLexerFeed(@"{name(  id : 1, id2:value)}"));
            Assert.AreEqual("name", result.First().Field.Name);
            Assert.AreEqual(2, result.First().Field.Arguments.Length);
            Assert.AreEqual("id", result.First().Field.Arguments.ElementAt(0).Name);
            Assert.AreEqual(1, result.First().Field.Arguments.ElementAt(0).Value);
            Assert.AreEqual("id2", result.First().Field.Arguments.ElementAt(1).Name);
            Assert.AreEqual("value", result.First().Field.Arguments.ElementAt(1).Value);
        }

        [Test]
        public void ParseNested()
        {
            var sb = new StringBuilder();

            sb.AppendLine("{");
            sb.AppendLine("  me {");
            sb.AppendLine("    id");
            sb.AppendLine("    firstName");
            sb.AppendLine("    lastName");
            sb.AppendLine("    birthday{");
            sb.AppendLine("      month");
            sb.AppendLine("      day");
            sb.AppendLine("    }");
            sb.AppendLine("    friends {");
            sb.AppendLine("      name");
            sb.AppendLine("    }");
            sb.AppendLine("  }");
            sb.AppendLine("}");

            var query = sb.ToString();

            var sut = new SelectionsParser();
            var result = sut.Parse(new GraphQLLexerFeed(query));
            Assert.AreEqual("me", result.First().Field.Name);
            Assert.AreEqual("id", result.First().Field.Selections.ElementAt(0).Field.Name);
            Assert.AreEqual("firstName", result.First().Field.Selections.ElementAt(1).Field.Name);
            Assert.AreEqual("lastName", result.First().Field.Selections.ElementAt(2).Field.Name);
            Assert.AreEqual("birthday", result.First().Field.Selections.ElementAt(3).Field.Name);
            Assert.AreEqual("month", result.First().Field.Selections.ElementAt(3).Field.Selections.ElementAt(0).Field.Name);
            Assert.AreEqual("day", result.First().Field.Selections.ElementAt(3).Field.Selections.ElementAt(1).Field.Name);
            Assert.AreEqual("friends", result.First().Field.Selections.ElementAt(4).Field.Name);
            Assert.AreEqual("name", result.First().Field.Selections.ElementAt(4).Field.Selections.ElementAt(0).Field.Name);
        }
    }
}