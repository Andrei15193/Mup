using System.Threading.Tasks;
using Xunit;

using static Mup.CreoleElementToken;

namespace Mup.Tests
{
    public class CreoleParser_Parse
    {
        private const string _method = (nameof(CreoleParser) + "." + nameof(CreoleParser.ParseAsync) + "(string): ");

        private readonly CreoleParser _parser = new CreoleParser();

        [Trait("Class", nameof(CreoleParser))]
        [Theory(DisplayName = (_method + nameof(ParsesTokens)))]
        [InlineData("test", new[] { ParagraphStart, Text, ParagraphEnd })]
        [InlineData("**test**", new[] { ParagraphStart, StrongStart, Text, StrongEnd, ParagraphEnd })]
        //[InlineData("test", new[] { ParagraphStart, Text, ParagraphEnd })]
        public async Task ParsesTokens(string text, CreoleElementToken[] tokens)
        {
            var result = await _parser.ParseAsync(text);

            Assert.Equal(tokens, result.Tokens);
        }
    }
}