using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mup.Tests
{
    public class Scanner_ScanAsync
    {
        private const string _textMethod = (nameof(Scanner<int>) + ".ScanAsync(string): ");
        private const string _readerMethod = (nameof(Scanner<int>) + ".ScanAsync(TextReader): ");

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(CannotScanFromNullText)))]
        public async Task CannotScanFromNullText()
        {
            var scanner = new Scanner<int>(new Dictionary<int, Func<char, bool>>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => scanner.ScanAsync(text: null));
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_readerMethod + nameof(CannotScanFromNullReader)))]
        public async Task CannotScanFromNullReader()
        {
            var scanner = new Scanner<int>(new Dictionary<int, Func<char, bool>>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => scanner.ScanAsync(reader: null));
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Theory(DisplayName = (_readerMethod + nameof(CannotHaveZeroOrNegativeBufferSize)))]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(0)]
        public async Task CannotHaveZeroOrNegativeBufferSize(int bufferSize)
        {
            var scanner = new Scanner<int>(new Dictionary<int, Func<char, bool>>());

            using (var stringReader = new StringReader(string.Empty))
                await Assert.ThrowsAsync<ArgumentException>(() => scanner.ScanAsync(stringReader, bufferSize));
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Theory(DisplayName = (_readerMethod + nameof(ScansFromTextReader)))]
        [InlineData(10)]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(100)]
        public async Task ScansFromTextReader(int bufferSize)
        {
            var scanner = new Scanner<int>(
                new Dictionary<int, Func<char, bool>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', bufferSize) + new string('b', bufferSize));
            using (var stringReader = new StringReader(text))
            {
                var result = await scanner.ScanAsync(stringReader, bufferSize);
                Assert.Equal(
                    new[]
                    {
                        new
                        {
                            Code = 0,
                            Start = 0,
                            Length = bufferSize
                        },
                        new
                        {
                            Code = 1,
                            Start = bufferSize,
                            Length = bufferSize
                        }
                    },
                    result.Tokens.Select(token =>
                        new
                        {
                            token.Code,
                            token.Start,
                            token.Length
                        }));
            }
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Theory(DisplayName = (_readerMethod + nameof(ScansFromTextSynchronously)))]
        [InlineData(10)]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(100)]
        public void ScansFromTextSynchronously(int bufferSize)
        {
            var scanner = new Scanner<int>(
                new Dictionary<int, Func<char, bool>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', bufferSize) + new string('b', bufferSize));
            var result = scanner.Scan(text);
            Assert.Equal(
                new[]
                {
                    new
                    {
                        Code = 0,
                        Start = 0,
                        Length = bufferSize
                    },
                    new
                    {
                        Code = 1,
                        Start = bufferSize,
                        Length = bufferSize
                    }
                },
                result.Tokens.Select(token =>
                new
                {
                    token.Code,
                    token.Start,
                    token.Length
                }));
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Theory(DisplayName = (_textMethod + nameof(ScansFromString)))]
        [InlineData(10)]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(100)]
        public async Task ScansFromString(int sequenceLegth)
        {
            var scanner = new Scanner<int>(
                new Dictionary<int, Func<char, bool>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', sequenceLegth) + new string('b', sequenceLegth));
            var result = await scanner.ScanAsync(text);
            Assert.Equal(
                new[]
                {
                    new
                    {
                        Code = 0,
                        Start = 0,
                        Length = sequenceLegth
                    },
                    new
                    {
                        Code = 1,
                        Start = sequenceLegth,
                        Length = sequenceLegth
                    }
                },
                result.Tokens.Select(token =>
                new
                {
                    token.Code,
                    token.Start,
                    token.Length
                }));
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Theory(DisplayName = (_textMethod + nameof(ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatches)))]
        [InlineData("baaa\naaaa", 'b', 1, 1)]
        [InlineData("aaaa\nbaaa", 'b', 2, 1)]
        [InlineData("aaba\naaaa", 'b', 1, 3)]
        [InlineData("aaaa\naaba", 'b', 2, 3)]
        [InlineData("aaaa\r\nbaaa", 'b', 2, 1)]
        [InlineData("aaaa\r\naaba", 'b', 2, 3)]
        [InlineData("aaaa\n\rbaaa", 'b', 2, 2)]
        [InlineData("aaaa\n\raaba", 'b', 2, 4)]
        public async Task ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatches(string text, char breakCharacter, int expectedLine, int expectedColumn)
        {
            var scanner = new Scanner<int>(
                new Dictionary<int, Func<char, bool>>
                {
                    { 0, c => c != breakCharacter }
                });

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => scanner.ScanAsync(text));
            Assert.Equal(
                $"Unexpected character '{breakCharacter}' ({breakCharacter:X2}) at line {expectedLine}, column {expectedColumn}.",
                exception.Message);
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(CancelingAScanFromTextLeavesTheTaskIsCanceledState)))]
        public async Task CancelingAScanFromTextLeavesTheTaskIsCanceledState()
        {
            var scanner = new Scanner<int>(new Dictionary<int, Func<char, bool>>());

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                Task task = null;
                cancellationTokenSource.Cancel();
                await scanner.ScanAsync(string.Empty, cancellationTokenSource.Token).ContinueWith(resultTask => task = resultTask);
                Assert.True(task.IsCanceled);
            }
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(CancelingAScanFromReaderLeavesTheTaskIsCanceledState)))]
        public async Task CancelingAScanFromReaderLeavesTheTaskIsCanceledState()
        {
            var scanner = new Scanner<int>(new Dictionary<int, Func<char, bool>>());

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                Task task = null;
                cancellationTokenSource.Cancel();
                using (var stringReader = new StringReader(string.Empty))
                    await scanner.ScanAsync(stringReader, cancellationTokenSource.Token).ContinueWith(resultTask => task = resultTask);
                Assert.True(task.IsCanceled);
            }
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(EmptyStringResultsInNotTokensFromText)))]
        public async Task EmptyStringResultsInNotTokensFromText()
        {
            var scanner = new Scanner<int>(new Dictionary<int, Func<char, bool>>());
            var result = await scanner.ScanAsync(string.Empty);
            Assert.False(result.Tokens.Any());
        }

        [Trait("Class", nameof(Scanner<int>))]
        [Fact(DisplayName = (_readerMethod + nameof(EmptyStringResultsInNotTokensFromReader)))]
        public async Task EmptyStringResultsInNotTokensFromReader()
        {
            var scanner = new Scanner<int>(new Dictionary<int, Func<char, bool>>());
            using (var stringReader = new StringReader(string.Empty))
            {
                var result = await scanner.ScanAsync(stringReader);
                Assert.False(result.Tokens.Any());
            }
        }
    }
}