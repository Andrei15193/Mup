using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mup.Creole;
using Mup.Creole.ElementFactories;
using Mup.Creole.Elements;
using Mup.Creole.Scanner;
using static Mup.Creole.CreoleTokenCode;

namespace Mup
{
    /// <summary>A markup parser implementation for Creole.</summary>
    public class CreoleParser
        : IMarkupParser
    {
        private const string _FileScheme = "file";
        private const string _FtpScheme = "ftp";
        private const string _GopherScheme = "gopher";
        private const string _HttpScheme = "http";
        private const string _HttpsScheme = "https";
        private const string _MailToScheme = "mailto";
        private const string _NntpScheme = "nntp";

        /// <summary>Initializes a new instance of the <see cref="CreoleParser"/> class.</summary>
        public CreoleParser()
        {
        }

        /// <summary>Parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public IParseTree Parse(string text)
        {
            var scanner = new CreoleScanner();
            scanner.Scan(text);
            var scanResult = scanner.Result;
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public Task<IParseTree> ParseAsync(string text)
            => ParseAsync(text, CancellationToken.None);

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        public async Task<IParseTree> ParseAsync(string text, CancellationToken cancellationToken)
        {
            var scanner = new CreoleScanner();
            await scanner.ScanAsync(text, cancellationToken).ConfigureAwait(false);
            var scanResult = scanner.Result;
            var parseTree = await _ParseAsync(scanResult, cancellationToken).ConfigureAwait(false);
            return parseTree;
        }

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public Task<IParseTree> ParseAsync(TextReader reader)
            => ParseAsync(reader, CancellationToken.None);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        public async Task<IParseTree> ParseAsync(TextReader reader, CancellationToken cancellationToken)
        {
            var scanner = new CreoleScanner();
            await scanner.ScanAsync(reader, cancellationToken).ConfigureAwait(false);
            var scanResult = scanner.Result;
            var parseTree = await _ParseAsync(scanResult, cancellationToken).ConfigureAwait(false);
            return parseTree;
        }

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public Task<IParseTree> ParseAsync(TextReader reader, int bufferSize)
            => ParseAsync(reader, bufferSize, CancellationToken.None);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        public async Task<IParseTree> ParseAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
        {
            var scanner = new CreoleScanner();
            await scanner.ScanAsync(reader, bufferSize, cancellationToken).ConfigureAwait(false);
            var scanResult = scanner.Result;
            var parseTree = await _ParseAsync(scanResult, cancellationToken).ConfigureAwait(false);
            return parseTree;
        }

        /// <summary>Gets the protocols for which inline hyperlinks are generated.</summary>
        protected virtual IEnumerable<string> InlineHyperlinkProtocols
        {
            get
            {
                yield return _FileScheme;
                yield return _FtpScheme;
                yield return _GopherScheme;
                yield return _HttpScheme;
                yield return _HttpsScheme;
                yield return _MailToScheme;
                yield return _NntpScheme;
            }
        }

        private IParseTree _Parse(CreoleScanResult scanResult)
        {
            var parser = new CreoleMarkupParser(scanResult, InlineHyperlinkProtocols);
            var parseTree = parser.Parse();
            return parseTree;
        }

        private async Task<IParseTree> _ParseAsync(CreoleScanResult scanResult, CancellationToken cancellationToken)
        {
            var parser = new CreoleMarkupParser(scanResult, InlineHyperlinkProtocols);
            var parseTree = await parser.ParseAsync(cancellationToken).ConfigureAwait(false);
            return parseTree;
        }

        private class CreoleMarkupParser
        {
            private readonly CreoleParserContext _context;
            private readonly IEnumerable<CreoleToken> _tokens;
            private readonly CreoleParagraphElementFactory _paragraphFactory;
            private readonly IEnumerable<CreoleElementFactory> _blockElementFactoriesExceptParagraph;

            internal CreoleMarkupParser(CreoleScanResult scanResult, IEnumerable<string> inlineHyperlinkProtocols)
            {
                _context = new CreoleParserContext(scanResult.Text, inlineHyperlinkProtocols);
                _tokens = scanResult.Tokens;
                _paragraphFactory = new CreoleParagraphElementFactory(_context);
                _blockElementFactoriesExceptParagraph = new CreoleElementFactory[]
                {
                    new CreoleHeadingElementFactory(_context),
                    new CreolePluginElementFactory(_context),
                    new CreolePreformattedBlockElementFactory(_context),
                    new CreoleHorizontalRuleElementFactory(_context),
                    new CreoleTableElementFactory(_context),
                    new CreoleListElementFactory(_context)
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
                        var paragraphFactoryResult = _paragraphFactory.TryCreateFrom(paragraphStart, token);
                        if (paragraphFactoryResult != null)
                        {
                            blockElements.Add(paragraphFactoryResult.Element);
                            token = paragraphFactoryResult.End;
                            paragraphStart = token.Next;
                        }
                    }
                    else if (token.Previous == null
                        || (token.Previous.Code == WhiteSpace && token.Previous.Previous == null)
                        || _IsNewLine(token.Previous))
                    {
                        var factoryResult = _TryCreateBlockElementExceptParagpraphFrom(token);
                        if (factoryResult != null)
                        {
                            if (factoryResult.Start.Previous != null)
                            {
                                var paragraphFactoryResult = _paragraphFactory.TryCreateFrom(paragraphStart, factoryResult.Start.Previous);
                                if (paragraphFactoryResult != null)
                                    blockElements.Add(paragraphFactoryResult.Element);
                            }
                            blockElements.Add(factoryResult.Element);
                            token = factoryResult.End;
                            paragraphStart = token.Next;
                        }
                    }

                    token = token.Next;
                }
                if (paragraphStart != null)
                {
                    var paragraphElement = _paragraphFactory.TryCreateFrom(paragraphStart, null);
                    if (paragraphElement != null)
                        blockElements.Add(paragraphElement.Element);
                }

                return new CreoleParseTree(blockElements);
            }

            internal async Task<IParseTree> ParseAsync(CancellationToken cancellationToken)
            {
                var token = _GetFirstNonWhiteSpaceToken();
                var paragraphStart = token;

                var blockElements = new List<CreoleElement>();
                while (token != null)
                {
                    if (_IsBlankLine(token.Next))
                    {
                        var paragraphFactoryResult = _paragraphFactory.TryCreateFrom(paragraphStart, token);
                        if (paragraphFactoryResult != null)
                        {
                            blockElements.Add(paragraphFactoryResult.Element);
                            token = paragraphFactoryResult.End;
                            paragraphStart = token.Next;

                            await Task.Yield();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }
                    else if (token.Previous == null
                        || (token.Previous.Code == WhiteSpace && token.Previous.Previous == null)
                        || _IsNewLine(token.Previous))
                    {
                        var factoryResult = _TryCreateBlockElementExceptParagpraphFrom(token);
                        if (factoryResult != null)
                        {
                            if (factoryResult.Start.Previous != null)
                            {
                                var paragraphFactoryResult = _paragraphFactory.TryCreateFrom(paragraphStart, factoryResult.Start.Previous);
                                if (paragraphFactoryResult != null)
                                    blockElements.Add(paragraphFactoryResult.Element);
                            }
                            blockElements.Add(factoryResult.Element);
                            token = factoryResult.End;
                            paragraphStart = token.Next;

                            await Task.Yield();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    }

                    token = token.Next;
                }
                if (paragraphStart != null)
                {
                    var paragraphElement = _paragraphFactory.TryCreateFrom(paragraphStart, null);
                    if (paragraphElement != null)
                        blockElements.Add(paragraphElement.Element);

                    await Task.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                }

                return new CreoleParseTree(blockElements);
            }

            private CreoleFactoryResult _TryCreateBlockElementExceptParagpraphFrom(CreoleToken token)
            {
                CreoleFactoryResult factoryResult = null;

                using (var blockElementFactory = _blockElementFactoriesExceptParagraph.GetEnumerator())
                    while (factoryResult == null && blockElementFactory.MoveNext())
                        factoryResult = blockElementFactory.Current.TryCreateFrom(token, null);

                return factoryResult;
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