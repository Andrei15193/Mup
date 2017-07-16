using System;
using Xunit;

namespace Mup.Tests
{
    public class Token_Ctor
    {
        private const string _method = (nameof(Token<int>) + "(string, int, int, int): ");

        [Trait("Class", nameof(Token<int>))]
        [Fact(DisplayName = (_method + nameof(CannotHaveNullText)))]
        public void CannotHaveNullText()
        {
            Assert.Throws<ArgumentNullException>(() => new Token<int>(null, 0, 0, 0));
        }

        [Trait("Class", nameof(Token<int>))]
        [Theory(DisplayName = (_method + nameof(CannotHaveStartOutsideOfTextBounds)))]
        [InlineData("", -1)]
        [InlineData("", 1)]
        [InlineData("test", 4)]
        public void CannotHaveStartOutsideOfTextBounds(string text, int start)
        {
            Assert.Throws<ArgumentException>(() => new Token<int>(text, 0, start, 0));
        }

        [Trait("Class", nameof(Token<int>))]
        [Theory(DisplayName = (_method + nameof(CannotHaveLengthOutsideOfTextBounds)))]
        [InlineData("", -1)]
        [InlineData("", 1)]
        [InlineData("test", 5)]
        public void CannotHaveLengthOutsideOfTextBounds(string text, int length)
        {
            Assert.Throws<ArgumentException>(() => new Token<int>(text, 0, 0, length));
        }

        [Trait("Class", nameof(Token<int>))]
        [Theory(DisplayName = (_method + nameof(ValueIsSubstringOfText)))]
        [InlineData("test", 0, 2, "te")]
        [InlineData("test", 1, 2, "es")]
        [InlineData("test", 2, 2, "st")]
        [InlineData("test", 0, 4, "test")]
        public void ValueIsSubstringOfText(string text, int start, int length, string expectedValue)
        {
            var token = new Token<int>(text, 0, start, length);

            Assert.Equal(expectedValue, token.Value);
        }

        [Trait("Class", nameof(Token<int>))]
        [Theory(DisplayName = (_method + nameof(TokenIsInitializedCorrectly)))]
        [InlineData("test123", 1, 2, 3)]
        public void TokenIsInitializedCorrectly(string text, int code, int start, int length)
        {
            var token = new Token<int>(text, code, start, length);

            Assert.Equal(
                new
                {
                    Code = code,
                    Start = start,
                    Length = length
                },
                new
                {
                    Code = token.Code,
                    Start = token.Start,
                    Length = token.Length
                });
        }
    }
}