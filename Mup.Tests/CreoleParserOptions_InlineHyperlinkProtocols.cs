using System;
using System.Linq;
using Xunit;

namespace Mup.Tests
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
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ThrowsExceptionWhenCollectionContainsNullEmptyOrWhiteSpace(string value)
        {
            Assert.Throws<ArgumentException>(() => new CreoleParserOptions { InlineHyperlinkProtocols = new[] { value } });
        }

        [Trait("Class", nameof(CreoleParserOptions))]
        [Theory(DisplayName = (_property + nameof(SetsProtocols)))]
        [InlineData("http")]
        [InlineData("https")]
        [InlineData("http,https")]
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