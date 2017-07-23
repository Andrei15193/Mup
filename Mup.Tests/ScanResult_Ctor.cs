using System;
using System.Linq;
using Xunit;

namespace Mup.Tests
{
    public class ScanResult_Ctor
    {
        private const string _method = (nameof(ScanResult<int>) + "(string, IEnumerable<Token<int>>): ");

        [Trait("Class", nameof(ScanResult<int>))]
        [Fact(DisplayName = (_method + nameof(CannotHaveNullText)))]
        public void CannotHaveNullText()
        {
            Assert.Throws<ArgumentNullException>(() => new ScanResult<int>(null, Enumerable.Empty<Token<int>>()));
        }

        [Trait("Class", nameof(ScanResult<int>))]
        [Fact(DisplayName = (_method + nameof(CannotHaveNullForTokensCollection)))]
        public void CannotHaveNullForTokensCollection()
        {
            Assert.Throws<ArgumentNullException>(() => new ScanResult<int>("", null));
        }

        [Trait("Class", nameof(ScanResult<int>))]
        [Fact(DisplayName = (_method + nameof(CannotHaveNullTokens)))]
        public void CannotHaveNullTokens()
        {
            Assert.Throws<ArgumentException>(() => new ScanResult<int>("", new Token<int>[] { null }));
        }

        [Trait("Class", nameof(ScanResult<int>))]
        [Theory(DisplayName = (_method + nameof(IsInitializedCorrectly)))]
        [InlineData("test", 0)]
        [InlineData("test123", 1)]
        [InlineData("test", 2)]
        [InlineData("test123", 10)]
        public void IsInitializedCorrectly(string text, int tokensCount)
        {
            var tokens = Enumerable
                .Range(0, tokensCount)
                .Select(tokenCode => new Token<int>(tokenCode, 0, 0))
                .ToList()
                .AsEnumerable();
            var scanResult = new ScanResult<int>(text, tokens);

            Assert.Equal(
                new
                {
                    Text = text,
                    Tokens = tokens
                },
                new
                {
                    Text = scanResult.Text,
                    Tokens = scanResult.Tokens
                });
        }
    }
}