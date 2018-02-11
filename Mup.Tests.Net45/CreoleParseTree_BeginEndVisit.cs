using System;
using System.IO;
using System.Threading;
using Mup.Creole;
using Xunit;

namespace Mup.Tests.Net45
{
    public class CreoleParseTree_BeginEndVisit
    {
        private static readonly CreoleParser _parser = new CreoleParser();

        private const string _method = (nameof(CreoleParseTree) + ".BeginVisit(ParseTreeVisitor)/" + ".EndVisit(IAsyncResult)" + ": ");

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(AcceptingNullVisitorThrowsException)))]
        public void AcceptingNullVisitorThrowsException()
        {
            var parseTree = _parser.Parse(string.Empty);

            Assert.Throws<ArgumentNullException>(() => parseTree.BeginAccept(null));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(EndingVisitUsingNullAsyncResultThrowsException)))]
        public void EndingVisitUsingNullAsyncResultThrowsException()
        {
            var parseTree = _parser.Parse(string.Empty);
            Assert.Throws<ArgumentNullException>(() => parseTree.EndAccept(null));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(EndingVisitWithOutputUsingNullAsyncResultThrowsException)))]
        public void EndingVisitWithOutputUsingNullAsyncResultThrowsException()
        {
            var parseTree = _parser.Parse(string.Empty);
            Assert.Throws<ArgumentNullException>(() => parseTree.EndAccept<string>(null));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(EndingVisitUsingAsyncResultFromOtherParseTreeThrowsException)))]
        public void EndingVisitUsingAsyncResultFromOtherParseTreeThrowsException()
        {
            var parseTree = _parser.Parse(string.Empty);
            var otherParseTree = _parser.Parse(string.Empty);

            var asyncResult = parseTree.BeginAccept(new HtmlWriterVisitor());
            Assert.Throws<ArgumentException>(() => otherParseTree.EndAccept(asyncResult));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(EndingVisitWithOutputUsingAsyncResultFromOtherParseTreeThrowsException)))]
        public void EndingVisitWithOutputUsingAsyncResultFromOtherParseTreeThrowsException()
        {
            var parseTree = _parser.Parse(string.Empty);
            var otherParseTree = _parser.Parse(string.Empty);

            var asyncResult = parseTree.BeginAccept(new HtmlWriterVisitor());
            Assert.Throws<ArgumentException>(() => otherParseTree.EndAccept<string>(asyncResult));
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(AcceptingVisitorWithCallbackInvokesIt)))]
        public void AcceptingVisitorWithCallbackInvokesIt()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            {
                var invokeCount = 0;
                var parseTree = _parser.Parse(string.Empty);

                parseTree.BeginAccept(
                    new HtmlWriterVisitor(),
                    asyncResult =>
                    {
                        parseTree.EndAccept(asyncResult);
                        Interlocked.Increment(ref invokeCount);
                        completeEvent.Set();
                    });
                completeEvent.WaitOne();

                Assert.Equal(1, invokeCount);
            }
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(AcceptingVisitorWithStateSetsItToResult)))]
        public void AcceptingVisitorWithStateSetsItToResult()
        {
            var state = new object();
            var parseTree = _parser.Parse(string.Empty);

            var asyncResult = parseTree.BeginAccept(new HtmlWriterVisitor(), state);
            parseTree.EndAccept(asyncResult);
            Assert.Equal(state, asyncResult.AsyncState);
        }

        [Trait("Class", nameof(CreoleParser))]
        [Fact(DisplayName = (_method + nameof(AcceptingVisitorWithStateMakesItAvailableInCallback)))]
        public void AcceptingVisitorWithStateMakesItAvailableInCallback()
        {
            using (var completeEvent = new ManualResetEvent(initialState: false))
            {
                var expectedState = new object();
                object actualState = null;
                var parseTree = _parser.Parse(string.Empty);

                parseTree.BeginAccept(
                    new HtmlWriterVisitor(),
                    asyncResult =>
                    {
                        parseTree.EndAccept(asyncResult);
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