﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Mup.Creole;
using Mup.Creole.ElementParsers;
using Mup.Creole.Elements;
using Mup.Creole.Scanner;
using static Mup.Creole.CreoleTokenCode;
#if netstandard10
using System.Threading.Tasks;
#endif

namespace Mup
{
    /// <summary>A markup parser implementation for Creole.</summary>
    public class CreoleParser : IMarkupParser
    {
        /// <summary>Initializes a new instance of the <see cref="CreoleParser"/> class.</summary>
        /// <param name="options">The options to use when parsing a block of text.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="options"/> are null.</exception>
        public CreoleParser(CreoleParserOptions options)
        {
            Options = (options ?? throw new ArgumentNullException(nameof(options)));
        }

        /// <summary>Initializes a new instance of the <see cref="CreoleParser"/> class.</summary>
        public CreoleParser()
            : this(new CreoleParserOptions())
        {
        }

        /// <summary>The options used by the parser.</summary>
        public CreoleParserOptions Options { get; }

        /// <summary>Parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public IParseTree Parse(string text)
        {
            var scanner = new CreoleScanner();
            scanner.Scan(text);
            var scanResult = scanner.Result;
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public IParseTree Parse(TextReader reader)
        {
            var scanner = new CreoleScanner();
            scanner.Scan(reader);
            var scanResult = scanner.Result;
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public IParseTree Parse(TextReader reader, int bufferSize)
        {
            var scanner = new CreoleScanner();
            scanner.Scan(reader, bufferSize);
            var scanResult = scanner.Result;
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public IAsyncResult BeginParse(string text)
            => BeginParse(text, null, null);


        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public IAsyncResult BeginParse(string text, object asyncState)
            => BeginParse(text, null, asyncState);

        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public IAsyncResult BeginParse(string text, AsyncCallback asyncCallback)
            => BeginParse(text, asyncCallback, null);

        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public IAsyncResult BeginParse(string text, AsyncCallback asyncCallback, object asyncState)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

#if net20
            return TaskAsyncOperationHelper.Run<IParseTree>(this, () => Parse(text), asyncCallback, asyncState);
#else
            return TaskAsyncOperationHelper.Run<IParseTree>(this, () => ParseAsync(text), asyncCallback, asyncState);
#endif
        }

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public IAsyncResult BeginParse(TextReader reader)
            => BeginParse(reader, null, null);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public IAsyncResult BeginParse(TextReader reader, object asyncState)
            => BeginParse(reader, null, asyncState);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public IAsyncResult BeginParse(TextReader reader, AsyncCallback asyncCallback)
            => BeginParse(reader, asyncCallback, null);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public IAsyncResult BeginParse(TextReader reader, AsyncCallback asyncCallback, object asyncState)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

#if net20
            return TaskAsyncOperationHelper.Run<IParseTree>(this, () => Parse(reader), asyncCallback, asyncState);
#else
            return TaskAsyncOperationHelper.Run<IParseTree>(this, () => ParseAsync(reader), asyncCallback, asyncState);
#endif
        }

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public IAsyncResult BeginParse(TextReader reader, int bufferSize)
            => BeginParse(reader, bufferSize, null, null);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public IAsyncResult BeginParse(TextReader reader, int bufferSize, object asyncState)
            => BeginParse(reader, bufferSize, null, asyncState);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public IAsyncResult BeginParse(TextReader reader, int bufferSize, AsyncCallback asyncCallback)
            => BeginParse(reader, bufferSize, asyncCallback, null);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public IAsyncResult BeginParse(TextReader reader, int bufferSize, AsyncCallback asyncCallback, object asyncState)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (bufferSize <= 0)
                throw new ArgumentException("The buffer size must be greater than zero.", nameof(bufferSize));

#if net20
            return TaskAsyncOperationHelper.Run<IParseTree>(this, () => Parse(reader, bufferSize), asyncCallback, asyncState);
#else
            return TaskAsyncOperationHelper.Run<IParseTree>(this, () => ParseAsync(reader, bufferSize), asyncCallback, asyncState);
#endif
        }

        /// <summary>Waits for the pending asynchronous parse operation to complete.</summary>
        /// <param name="asyncResult">The pending asynchronous operation to wait for.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="asyncResult"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="asyncResult"/> was not returned by one of the Begin methods of the current instance.</exception>
        public IParseTree EndParse(IAsyncResult asyncResult)
        {
            if (asyncResult == null)
                throw new ArgumentNullException(nameof(asyncResult));

            return TaskAsyncOperationHelper.GetResult<IParseTree>(this, asyncResult);
        }

#if netstandard10
        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public Task<IParseTree> ParseAsync(string text)
            => ParseAsync(text, CancellationToken.None);

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public Task<IParseTree> ParseAsync(string text, CancellationToken cancellationToken)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            return Parse();

            async Task<IParseTree> Parse()
            {
                var scanner = new CreoleScanner();
                await scanner.ScanAsync(text, cancellationToken).ConfigureAwait(false);
                var scanResult = scanner.Result;
                var parseTree = await _ParseAsync(scanResult, cancellationToken).ConfigureAwait(false);
                return parseTree;
            }
        }

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public Task<IParseTree> ParseAsync(TextReader reader)
            => ParseAsync(reader, CancellationToken.None);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public Task<IParseTree> ParseAsync(TextReader reader, CancellationToken cancellationToken)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            return Parse();

            async Task<IParseTree> Parse()
            {
                var scanner = new CreoleScanner();
                await scanner.ScanAsync(reader, cancellationToken).ConfigureAwait(false);
                var scanResult = scanner.Result;
                var parseTree = await _ParseAsync(scanResult, cancellationToken).ConfigureAwait(false);
                return parseTree;
            };
        }

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public Task<IParseTree> ParseAsync(TextReader reader, int bufferSize)
            => ParseAsync(reader, bufferSize, CancellationToken.None);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public Task<IParseTree> ParseAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (bufferSize <= 0)
                throw new ArgumentException("The buffer size must be greater than zero.", nameof(bufferSize));
            return Parse();

            async Task<IParseTree> Parse()
            {
                var scanner = new CreoleScanner();
                await scanner.ScanAsync(reader, bufferSize, cancellationToken).ConfigureAwait(false);
                var scanResult = scanner.Result;
                var parseTree = await _ParseAsync(scanResult, cancellationToken).ConfigureAwait(false);
                return parseTree;
            }
        }
#endif

        private IParseTree _Parse(CreoleScanResult scanResult)
        {
            var parser = new CreoleMarkupParser(scanResult, Options.InlineHyperlinkProtocols);
            var parseTree = parser.Parse();
            return parseTree;
        }

#if netstandard10
        private async Task<IParseTree> _ParseAsync(CreoleScanResult scanResult, CancellationToken cancellationToken)
        {
            var parser = new CreoleMarkupParser(scanResult, Options.InlineHyperlinkProtocols);
            var parseTree = await parser.ParseAsync(cancellationToken).ConfigureAwait(false);
            return parseTree;
        }
#endif

        private class CreoleMarkupParser
        {
            private readonly CreoleParserContext _context;
            private readonly IEnumerable<CreoleToken> _tokens;
            private readonly CreoleParagraphElementParser _paragraphParser;
            private readonly IEnumerable<CreoleElementParser> _blockElementParsersExceptParagraph;

            internal CreoleMarkupParser(CreoleScanResult scanResult, IEnumerable<string> inlineHyperlinkProtocols)
            {
                _context = new CreoleParserContext(scanResult.Text, inlineHyperlinkProtocols);
                _tokens = scanResult.Tokens;
                _paragraphParser = new CreoleParagraphElementParser(_context);
                _blockElementParsersExceptParagraph = new CreoleElementParser[]
                {
                    new CreoleHeadingElementParser(_context),
                    new CreolePluginElementParser(_context),
                    new CreolePreformattedBlockElementParser(_context),
                    new CreoleHorizontalRuleElementParser(_context),
                    new CreoleTableElementParser(_context),
                    new CreoleListElementParser(_context)
                };
            }

            internal IParseTree Parse()
            {
                var token = _GetFirstNonWhiteSpaceToken();
                var paragraphStart = token;

                var blockElements = new List<CreoleElement>();
                while (token != null)
                {
                    if (_IsBlankLine(token.Next))
                    {
                        var paragraphParseResult = _paragraphParser.TryCreateFrom(paragraphStart, token);
                        if (paragraphParseResult != null)
                        {
                            blockElements.Add(paragraphParseResult.Element);
                            token = paragraphParseResult.End;
                            paragraphStart = token.Next;
                        }
                    }
                    else if (token.Previous == null
                        || (token.Previous.Code == WhiteSpace && token.Previous.Previous == null)
                        || _IsNewLine(token.Previous))
                    {
                        var elementParseResult = _TryCreateBlockElementExceptParagpraphFrom(token);
                        if (elementParseResult != null)
                        {
                            if (elementParseResult.Start.Previous != null)
                            {
                                var paragraphParseResult = _paragraphParser.TryCreateFrom(paragraphStart, elementParseResult.Start.Previous);
                                if (paragraphParseResult != null)
                                    blockElements.Add(paragraphParseResult.Element);
                            }
                            blockElements.Add(elementParseResult.Element);
                            token = elementParseResult.End;
                            paragraphStart = token.Next;
                        }
                    }

                    token = token.Next;
                }
                if (paragraphStart != null)
                {
                    var paragraphElement = _paragraphParser.TryCreateFrom(paragraphStart, null);
                    if (paragraphElement != null)
                        blockElements.Add(paragraphElement.Element);
                }

                return new CreoleParseTree(blockElements);
            }

#if netstandard10
            internal async Task<IParseTree> ParseAsync(CancellationToken cancellationToken)
            {
                var token = _GetFirstNonWhiteSpaceToken();
                var paragraphStart = token;

                var blockElements = new List<CreoleElement>();
                while (token != null)
                {
                    if (_IsBlankLine(token.Next))
                    {
                        var paragraphParseResult = _paragraphParser.TryCreateFrom(paragraphStart, token);
                        if (paragraphParseResult != null)
                        {
                            blockElements.Add(paragraphParseResult.Element);
                            token = paragraphParseResult.End;
                            paragraphStart = token.Next;

                            await Task.Yield();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }
                    else if (token.Previous == null
                        || (token.Previous.Code == WhiteSpace && token.Previous.Previous == null)
                        || _IsNewLine(token.Previous))
                    {
                        var elementParseResult = _TryCreateBlockElementExceptParagpraphFrom(token);
                        if (elementParseResult != null)
                        {
                            if (elementParseResult.Start.Previous != null)
                            {
                                var paragraphParseResult = _paragraphParser.TryCreateFrom(paragraphStart, elementParseResult.Start.Previous);
                                if (paragraphParseResult != null)
                                    blockElements.Add(paragraphParseResult.Element);
                            }
                            blockElements.Add(elementParseResult.Element);
                            token = elementParseResult.End;
                            paragraphStart = token.Next;

                            await Task.Yield();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }

                    token = token.Next;
                }
                if (paragraphStart != null)
                {
                    var paragraphElement = _paragraphParser.TryCreateFrom(paragraphStart, null);
                    if (paragraphElement != null)
                        blockElements.Add(paragraphElement.Element);

                    await Task.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }

                return new CreoleParseTree(blockElements);
            }
#endif

            private CreoleElementParserResult _TryCreateBlockElementExceptParagpraphFrom(CreoleToken token)
            {
                CreoleElementParserResult elementParseResult = null;

                using (var blockElementParser = _blockElementParsersExceptParagraph.GetEnumerator())
                    while (elementParseResult == null && blockElementParser.MoveNext())
                        elementParseResult = blockElementParser.Current.TryCreateFrom(token, null);

                return elementParseResult;
            }

            private CreoleToken _GetFirstNonWhiteSpaceToken()
            {
                CreoleToken firstNonWhiteSpaceToken = null;
                using (var token = _tokens.GetEnumerator())
                    if (token.MoveNext())
                    {
                        firstNonWhiteSpaceToken = token.Current;
                        while (token.MoveNext() && firstNonWhiteSpaceToken.Code == WhiteSpace)
                            firstNonWhiteSpaceToken = token.Current;
                    }
                return firstNonWhiteSpaceToken;
            }

            private bool _IsNewLine(CreoleToken token)
                => _ContainsLineFeeds(token, 1);

            private bool _IsBlankLine(CreoleToken token)
                => _ContainsLineFeeds(token, 2);

            private bool _ContainsLineFeeds(CreoleToken token, int minCount)
            {
                var containsBlankLine = false;

                if (token?.Code == WhiteSpace)
                {
                    var index = token.StartIndex;
                    var lineFeedCount = 0;
                    while (index < token.EndIndex && lineFeedCount < minCount)
                    {
                        if (_context.Text[index] == '\n')
                            lineFeedCount++;
                        index++;
                    }
                    containsBlankLine = (lineFeedCount >= minCount);
                }

                return containsBlankLine;
            }
        }
    }
}