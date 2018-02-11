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
    public class Scanner_Scan
    {
        private const string _textMethod = (nameof(CharacterRepeatScanner<int>) + ".Scan(string): ");
        private const string _readerMethod = (nameof(CharacterRepeatScanner<int>) + ".Scan(TextReader): ");
        private const string _readerWithBufferMethod = (nameof(CharacterRepeatScanner<int>) + ".Scan(TextReader, int): ");

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(CannotScanFromNullText)))]
        public void CannotScanFromNullText()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            Assert.Throws<ArgumentNullException>(() => scanner.Scan(text: null));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_readerMethod + nameof(CannotScanFromNullTextReader)))]
        public void CannotScanFromNullTextReader()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            Assert.Throws<ArgumentNullException>(() => scanner.Scan(reader: null));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_readerMethod + nameof(CannotScanWithNegativeOrZeroBufferSize)))]
        [MemberData(nameof(InvalidBufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public void CannotScanWithNegativeOrZeroBufferSize(int bufferSize)
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());

            using (var reader = new StringReader(string.Empty))
                Assert.Throws<ArgumentException>(() => scanner.Scan(reader, bufferSize));
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_textMethod + nameof(ScansFromString)))]
        [MemberData(nameof(BufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public void ScansFromString(int sequenceLegth)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', sequenceLegth) + new string('b', sequenceLegth));
            scanner.Scan(text);
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
        [Theory(DisplayName = (_readerWithBufferMethod + nameof(ScansFromTextReader)))]
        [MemberData(nameof(BufferSizeTestData), MemberType = typeof(ScannerTestData))]
        public void ScansFromTextReader(int bufferSize)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c == 'a' },
                    { 1, c => c == 'b' }
                });

            var text = (new string('a', bufferSize) + new string('b', bufferSize));
            using (var stringReader = new StringReader(text))
                scanner.Scan(stringReader, bufferSize);
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
        [Theory(DisplayName = (_textMethod + nameof(ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatchesWhenScanningFromString)))]
        [MemberData(nameof(UnrecognizedCharacterTestData), MemberType = typeof(ScannerTestData))]
        public void ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatchesWhenScanningFromString(string text, char breakCharacter, int expectedLine, int expectedColumn)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c != breakCharacter }
                });

            var exception = Assert.Throws<InvalidOperationException>(() => scanner.Scan(text));
            Assert.Equal(
                $"Unexpected character '{breakCharacter}' ({breakCharacter:X2}) at line {expectedLine}, column {expectedColumn}.",
                exception.Message);
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Theory(DisplayName = (_readerMethod + nameof(ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatchesWhenScanningFromTextReader)))]
        [MemberData(nameof(UnrecognizedCharacterTestData), MemberType = typeof(ScannerTestData))]
        public void ThrowsExceptionWithLineAndColumnInformationWhenNoPredicateMatchesWhenScanningFromTextReader(string text, char breakCharacter, int expectedLine, int expectedColumn)
        {
            var scanner = new CharacterRepeatScannerMock<int>(
                new Dictionary<int, Predicate<char>>
                {
                    { 0, c => c != breakCharacter }
                });

            using (var stringReader = new StringReader(text))
            {
                var exception = Assert.Throws<InvalidOperationException>(() => scanner.Scan(stringReader));
                Assert.Equal(
                    $"Unexpected character '{breakCharacter}' ({breakCharacter:X2}) at line {expectedLine}, column {expectedColumn}.",
                    exception.Message);
            }
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_textMethod + nameof(EmptyStringResultsInNotTokensFromText)))]
        public void EmptyStringResultsInNotTokensFromText()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());
            scanner.Scan(string.Empty);
            var result = scanner.Result;
            Assert.False(result.Tokens.Any());
        }

        [Trait("Class", nameof(CharacterRepeatScanner<int>))]
        [Fact(DisplayName = (_readerMethod + nameof(EmptyStringResultsInNotTokensFromTextReader)))]
        public void EmptyStringResultsInNotTokensFromTextReader()
        {
            var scanner = new CharacterRepeatScannerMock<int>(new Dictionary<int, Predicate<char>>());
            using (var stringReader = new StringReader(string.Empty))
                scanner.Scan(stringReader);
            var result = scanner.Result;
            Assert.False(result.Tokens.Any());
        }
    }
}