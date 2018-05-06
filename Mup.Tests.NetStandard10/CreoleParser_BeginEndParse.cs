using System;
using System.IO;
using System.Threading;
using Xunit;

namespace Mup.Tests.NetStandard10
{
    public class CreoleParser_BeginEndParse
    {
        private static readonly CreoleParser _parser = new CreoleParser();

        private const string _textMethod = (nameof(CreoleParser) + ".BeginParse(string): ");
        private const string _textReaderMethod = (nameof(CreoleParser) + ".BeginParse(TextReader): ");
        private const string _textReaderWithBufferMethod = (nameof(CreoleParser) + ".BeginParse(TextReader, int): ");
        private const string _endMethod = (nameof(CreoleParser) + ".EndParse(IAsyncResult): ");

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingNullTextThrowsException)))]
        public void ParsingNullTextThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _parser.BeginParse(text: null));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textReaderMethod + nameof(ParsingNullTextReaderThrowsException)))]
        public void ParsingNullTextReaderThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _parser.BeginParse(reader: null));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textReaderWithBufferMethod + nameof(ParsingNegativeBufferSizeThrowsException)))]
        public void ParsingNegativeBufferSizeThrowsException()
        {
            using (var stringReader = new StringReader(string.Empty))
                Assert.Throws<ArgumentException>(() => _parser.BeginParse(stringReader, -1));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textReaderWithBufferMethod + nameof(ParsingZeroBufferSizeThrowsException)))]
        public void ParsingZeroBufferSizeThrowsException()
        {
            using (var stringReader = new StringReader(string.Empty))
                Assert.Throws<ArgumentException>(() => _parser.BeginParse(stringReader, 0));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textReaderWithBufferMethod + nameof(ParsingNullToEndParseThrowsException)))]
        public void ParsingNullToEndParseThrowsException()
        {
            using (var stringReader = new StringReader(string.Empty))
                Assert.Throws<ArgumentNullException>(() => _parser.EndParse(null));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textReaderWithBufferMethod + nameof(ParsingAsyncResultFromDifferentParserInstanceToEndParseThrowsException)))]
        public void ParsingAsyncResultFromDifferentParserInstanceToEndParseThrowsException()
        {
            var otherParser = new CreoleParser();

            var result = otherParser.BeginParse(string.Empty);
            Assert.Throws<ArgumentException>(() => _parser.EndParse(result));

            otherParser.EndParse(result);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingTextWithStateSetsItInAsyncResult)))]
        public void ParsingTextWithStateSetsItInAsyncResult()
        {
            var state = new object();
            var asyncResult = _parser.BeginParse(string.Empty, state);
            _parser.EndParse(asyncResult);

            Assert.Equal(state, asyncResult.AsyncState);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingTextWithAsyncCallbackCallsIt)))]
        public void ParsingTextWithAsyncCallbackCallsIt()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            {
                int invokeCount = 0;

                _parser.BeginParse(
                    string.Empty,
                    asyncResult =>
                    {
                        _parser.EndParse(asyncResult);
                        Interlocked.Increment(ref invokeCount);
                        completeEvent.Set();
                    });
                completeEvent.WaitOne();

                Assert.Equal(1, invokeCount);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingTextWithStateMakesItAvailableInAsyncCallback)))]
        public void ParsingTextWithStateMakesItAvailableInAsyncCallback()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            {
                var expectedState = new object();
                object actualState = null;

                _parser.BeginParse(
                    string.Empty,
                    asyncResult =>
                    {
                        _parser.EndParse(asyncResult);
                        actualState = asyncResult.AsyncState;
                        completeEvent.Set();
                    },
                    expectedState);
                completeEvent.WaitOne();

                Assert.Equal(expectedState, actualState);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingFromTextReaderWithStateSetsItInAsyncResult)))]
        public void ParsingFromTextReaderWithStateSetsItInAsyncResult()
        {
            using (var stringReader = new StringReader(string.Empty))
            {
                var state = new object();

                var asyncResult = _parser.BeginParse(stringReader, state);
                _parser.EndParse(asyncResult);

                Assert.Equal(state, asyncResult.AsyncState);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingFromTextReaderTextWithAsyncCallbackCallsIt)))]
        public void ParsingFromTextReaderTextWithAsyncCallbackCallsIt()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            using (var stringReader = new StringReader(string.Empty))
            {
                int invokeCount = 0;

                _parser.BeginParse(
                    stringReader,
                    asyncResult =>
                    {
                        _parser.EndParse(asyncResult);
                        Interlocked.Increment(ref invokeCount);
                        completeEvent.Set();
                    });
                completeEvent.WaitOne();

                Assert.Equal(1, invokeCount);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingFromTextReaderWithStateMakesItAvailableInAsyncCallback)))]
        public void ParsingFromTextReaderWithStateMakesItAvailableInAsyncCallback()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            using (var stringReader = new StringReader(string.Empty))
            {
                var expectedState = new object();
                object actualState = null;

                _parser.BeginParse(
                    stringReader,
                    asyncResult =>
                    {
                        _parser.EndParse(asyncResult);
                        actualState = asyncResult.AsyncState;
                        completeEvent.Set();
                    },
                    expectedState);
                completeEvent.WaitOne();

                Assert.Equal(expectedState, actualState);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingFromTextReaderWithBufferWithStateSetsItInAsyncResult)))]
        public void ParsingFromTextReaderWithBufferWithStateSetsItInAsyncResult()
        {
            using (var stringReader = new StringReader(string.Empty))
            {
                var state = new object();

                var asyncResult = _parser.BeginParse(stringReader, 1, state);
                _parser.EndParse(asyncResult);

                Assert.Equal(state, asyncResult.AsyncState);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingFromTextReaderWithBufferTextWithAsyncCallbackCallsIt)))]
        public void ParsingFromTextReaderWithBufferTextWithAsyncCallbackCallsIt()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            using (var stringReader = new StringReader(string.Empty))
            {
                int invokeCount = 0;

                _parser.BeginParse(
                    stringReader,
                    1,
                    asyncResult =>
                    {
                        _parser.EndParse(asyncResult);
                        Interlocked.Increment(ref invokeCount);
                        completeEvent.Set();
                    });
                completeEvent.WaitOne();

                Assert.Equal(1, invokeCount);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_textMethod + nameof(ParsingFromTextReaderWithBufferWithStateMakesItAvailableInAsyncCallback)))]
        public void ParsingFromTextReaderWithBufferWithStateMakesItAvailableInAsyncCallback()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            using (var stringReader = new StringReader(string.Empty))
            {
                var expectedState = new object();
                object actualState = null;

                _parser.BeginParse(
                    stringReader,
                    1,
                    asyncResult =>
                    {
                        _parser.EndParse(asyncResult);
                        actualState = asyncResult.AsyncState;
                        completeEvent.Set();
                    },
                    expectedState);
                completeEvent.WaitOne();

                Assert.Equal(expectedState, actualState);
            }
        }
    }
}