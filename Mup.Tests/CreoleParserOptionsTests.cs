using System;
using System.Linq;
using Xunit;
using static Mup.Tests.CreoleParseroptionsTestData;

namespace Mup.Tests
{
    public class CreoleParserOptionsTests
    {
        private const string _property = (nameof(CreoleParserOptions) + "." + nameof(CreoleParserOptions.InlineHyperlinkProtocols) + ": ");

        [Fact]
        public void ThrowsExceptionWhenSettingsToNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CreoleParserOptions { InlineHyperlinkProtocols = null });
        }

        [Theory, MemberData(nameof(InvalidInlineProtocolsTestCases), MemberType = typeof(CreoleParseroptionsTestData))]
        public void ThrowsExceptionWhenCollectionContainsNullEmptyOrWhiteSpace(string value)
        {
            Assert.Throws<ArgumentException>(() => new CreoleParserOptions { InlineHyperlinkProtocols = new[] { value } });
        }

        [Theory, MemberData(nameof(InlineProtocolsTestCases), MemberType = typeof(CreoleParseroptionsTestData))]
        public void SetsProtocols(string protocols)
        {
            var protocolsList = protocols.Split(',').Select(protocol => protocol.Trim()).ToList();

            var options = new CreoleParserOptions
            {
                InlineHyperlinkProtocols = protocolsList
            };

            Assert.Equal(protocolsList, options.InlineHyperlinkProtocols);
        }
    }
}