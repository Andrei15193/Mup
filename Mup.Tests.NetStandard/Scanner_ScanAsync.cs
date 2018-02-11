using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mup.Scanner;
using Xunit;
using static Mup.Tests.ScannerTestData;

namespace Mup.Tests.NetStandard
{
    public class Scanner_ScanAsync
    {
        private const string _textMethod = (nameof(CharacterRepeatScanner<int>) + ".ScanAsync(string): ");
        private const string _readerMethod = (nameof(CharacterRepeatScanner<int>) + ".ScanAsync(TextReader): ");

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(CannotScanFromNullText)))]
        public async Task CannotScanFromNullText()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => scanner.ScanAsync(text: null, cancellationToken: CancellationToken.None));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_readerMethod + nameof(CannotScanFromNullReader)))]
        public async Task CannotScanFromNullReader()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => scanner.ScanAsync(reader: null, cancellationToken: CancellationToken.None));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_readerMethod + nameof(CannotHaveZeroOrNegativeBufferSize)))]
        [MemberData(nameof(InvalidBufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public async Task CannotHaveZeroOrNegativeBufferSize(int bufferSize)
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            using (var stringReader = new StringReader(string.Empty))
                await Assert.ThrowsAsync<ArgumentException>(() => scanner.ScanAsync(stringReader, bufferSize, CancellationToken.None));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_readerMethod + nameof(ScansFromTextReader)))]
        [MemberData(nameof(BufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public async Task ScansFromTextReader(int bufferSize)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', bufferSize) + new string('b', bufferSize));
            using (var stringReader = new StringReader(text))
            {
                await scanner.ScanAsync(stringReader, bufferSize, CancellationToken.None);
                var result = scanner.Result;
                Assert.Equal(
                    new[]
                    {
                        new
                        {
                            Code = 0,
                            StartIndex = 0,
                            Length = bufferSize
                        },
                        new
                        {
                            Code = 1,
                            StartIndex = bufferSize,
                            Length = bufferSize
                        }
                    },
                    result.Tokens.Select(token =>
                        new
                        {
                            token.Code,
                            token.StartIndex,
                            token.Length
                        }));
            }
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_textMethod + nameof(ScansFromTextSynchronously)))]
        [MemberData(nameof(BufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public void ScansFromTextSynchronously(int bufferSize)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', bufferSize) + new string('b', bufferSize));
            scanner.Scan(text);
            var result = scanner.Result;
            Assert.Equal(
                new[]
                {
                    new
                    {
                        Code = 0,
                        StartIndex = 0,
                        Length = bufferSize
                    },
                    new
                    {
                        Code = 1,
                        StartIndex = bufferSize,
                        Length = bufferSize
                    }
                },
                result.Tokens.Select(token =>
                new
                {
                    token.Code,
                    token.StartIndex,
                    token.Length
                }));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_textMethod + nameof(ScansFromString)))]
        [MemberData(nameof(BufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public async Task ScansFromString(int sequenceLegth)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', sequenceLegth) + new string('b', sequenceLegth));
            await scanner.ScanAsync(text, CancellationToken.None);
            var result = scanner.Result;
            Assert.Equal(
                new[]
                {
                    new
                    {
                        Code = 0,
                        StartIndex = 0,
                        Length = sequenceLegth
                    },
                    new
                    {
                        Code = 1,
                        StartIndex = sequenceLegth,
                        Length = sequenceLegth
                    }
                },
                result.Tokens.Select(token =>
                new
                {
                    token.Code,
                    token.StartIndex,
                    token.Length
                }));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_textMethod + nameof(ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatches)))]
        [MemberData(nameof(UnrecognizedCharacterTestData), MemberType = typeof(ScannerTestData))]
        public async Task ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatches(string text, char breakCharacter, int expectedLine, int expectedColumn)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c != breakCharacter }
                });

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => scanner.ScanAsync(text, CancellationToken.None));
            Assert.Equal(
                $"Unexpected character '{breakCharacter}' ({breakCharacter:X2}) at line {expectedLine}, column {expectedColumn}.",
                exception.Message);
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(CancelingAScanFromTextLeavesTheTaskIsCanceledState)))]
        public async Task CancelingAScanFromTextLeavesTheTaskIsCanceledState()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                Task task = null;
                cancellationTokenSource.Cancel();
                await scanner.ScanAsync(string.Empty, cancellationTokenSource.Token).ContinueWith(resultTask => task = resultTask);
                Assert.True(task.IsCanceled);
            }
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_readerMethod + nameof(CancelingAScanFromReaderLeavesTheTaskIsCanceledState)))]
        public async Task CancelingAScanFromReaderLeavesTheTaskIsCanceledState()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                Task task = null;
                cancellationTokenSource.Cancel();
                using (var stringReader = new StringReader(string.Empty))
                    await scanner.ScanAsync(stringReader, cancellationTokenSource.Token).ContinueWith(resultTask => task = resultTask);
                Assert.True(task.IsCanceled);
            }
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(EmptyStringResultsInNotTokensFromText)))]
        public async Task EmptyStringResultsInNotTokensFromText()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());
            await scanner.ScanAsync(string.Empty, CancellationToken.None);
            var result = scanner.Result;
            Assert.False(result.Tokens.Any());
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_readerMethod + nameof(EmptyStringResultsInNotTokensFromReader)))]
        public async Task EmptyStringResultsInNotTokensFromReader()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());
            using (var stringReader = new StringReader(string.Empty))
            {
                await scanner.ScanAsync(stringReader, CancellationToken.None);
                var result = scanner.Result;
                Assert.False(result.Tokens.Any());
            }
        }
    }
}