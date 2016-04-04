using System.Linq;
using System.Text;
using Graphene.Core;
using NUnit.Framework;

namespace Graphene.Test
{
    public class SelectionParserTest
    {
        [Test]
        public void Parse1()
        {
            var sut = new SelectionsParser();
            var result = sut.Parse(new ParserFeed(@"{name}"));
            Assert.AreEqual("name", result.First().Field.Name);
        }

        [Test]
        public void Parse2()
        {
            var sut = new SelectionsParser();
            var result = sut.Parse(new ParserFeed(@"{name,age}"));
            Assert.AreEqual("name", result.ElementAt(0).Field.Name);
            Assert.AreEqual("age", result.ElementAt(1).Field.Name);
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
            var result = sut.Parse(new ParserFeed(query));
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