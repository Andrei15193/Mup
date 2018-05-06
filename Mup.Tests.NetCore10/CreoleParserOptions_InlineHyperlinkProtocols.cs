using System;
using System.Linq;
using Xunit;
using static Mup.Tests.CreoleParseroptionsTestData;

namespace Mup.Tests.NetCore10
{
    public class CreoleParserOptions_InlineHyperlinkProtocols
    {
        private const string _property = (nameof(CreoleParserOptions) + "." + nameof(CreoleParserOptions.InlineHyperlinkProtocols) + ": ");

        [Trait("Class", nameof(CreoleParserOptions))]
        [Fact(DisplayName = (_property + nameof(ThrowsExceptionWhenSettingsToNull)))]
        public void ThrowsExceptionWhenSettingsToNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CreoleParserOptions { InlineHyperlinkProtocols = null });
        }

        [Trait("Class", nameof(CreoleParserOptions))]
        [Theory(DisplayName = (_property + nameof(ThrowsExceptionWhenCollectionContainsNullEmptyOrWhiteSpace)))]
        [MemberData(nameof(InvalidInlineProtocolsTestCases), MemberType = typeof(CreoleParseroptionsTestData))]
        public void ThrowsExceptionWhenCollectionContainsNullEmptyOrWhiteSpace(string value)
        {
            Assert.Throws<ArgumentException>(() => new CreoleParserOptions { InlineHyperlinkProtocols = new[] { value } });
        }

        [Trait("Class", nameof(CreoleParserOptions))]
        [Theory(DisplayName = (_property + nameof(SetsProtocols)))]
        [MemberData(nameof(InlineProtocolsTestCases), MemberType = typeof(CreoleParseroptionsTestData))]
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