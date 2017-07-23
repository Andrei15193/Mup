using Xunit;

namespace Mup.Tests
{
    public class Token_Ctor
    {
        private const string _method = (nameof(Token<int>) + "(string, int, int, int): ");

        [Trait("Class", nameof(Token<int>))]
        [Theory(DisplayName = (_method + nameof(TokenIsInitializedCorrectly)))]
        [InlineData(1, 2, 3)]
        public void TokenIsInitializedCorrectly(int code, int start, int length)
        {
            var token = new Token<int>(code, start, length);

            Assert.Equal(
                new
                {
                    Code = code,
                    Start = start,
                    Length = length,
                    End = (start + length)
                },
                new
                {
                    Code = token.Code,
                    Start = token.Start,
                    Length = token.Length,
                    End = token.End
                });
        }
    }
}