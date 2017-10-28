using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mup.Creole;
using Mup.Creole.ElementFactories;
using Mup.Creole.Elements;
using Mup.Creole.Scanner;

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
            private readonly IEnumerable<CreoleToken> _tokens;
            private readonly IEnumerable<CreoleElementFactory> _blockElementFactories;

            internal CreoleMarkupParser(CreoleScanResult scanResult, IEnumerable<string> inlineHyperlinkProtocols)
            {
                var context = new CreoleParserContext(scanResult.Text, inlineHyperlinkProtocols);

                _tokens = scanResult.Tokens;
                _blockElementFactories = new CreoleElementFactory[]
                {
                    new CreoleHeadingElementFactory(context),
                    new CreolePluginElementFactory(context),
                    new CreolePreformattedBlockElementFactory(context),
                    new CreoleHorizontalRuleElementFactory(context),
                    new CreoleTableElementFactory(context),
                    new CreoleListElementFactory(context),
                    new CreoleParagraphElementFactory(context),
                    new CreoleBlankElementFactory(context)
                };
            }

            internal IParseTree Parse()
            {
                var currentToken = _tokens.FirstOrDefault();
                if (currentToken == null)
                    return new CreoleParseTree(Enumerable.Empty<CreoleElement>());

                var blockElements = new List<CreoleElement>();
                do
                {
                    var factoryResult = _CreateBlockElementFrom(currentToken);
                    blockElements.Add(factoryResult.Element);
                    currentToken = factoryResult.End.Next;
                } while (currentToken != null);
                return new CreoleParseTree(blockElements);
            }

            internal async Task<IParseTree> ParseAsync(CancellationToken cancellationToken)
            {
                var currentToken = _tokens.FirstOrDefault();
                if (currentToken == null)
                    return new CreoleParseTree(Enumerable.Empty<CreoleElement>());

                var blockElements = new List<CreoleElement>();
                do
                {
                    var factoryResult = _CreateBlockElementFrom(currentToken);
                    blockElements.Add(factoryResult.Element);
                    currentToken = factoryResult.End.Next;

                    await Task.Yield();
                    cancellationToken.ThrowIfCancellationRequested();
                } while (currentToken != null);
                return new CreoleParseTree(blockElements);
            }

            private CreoleFactoryResult _CreateBlockElementFrom(CreoleToken token)
            {
                CreoleFactoryResult factoryResult = null;
                using (var blockElementFactory = _blockElementFactories.GetEnumerator())
                    do
                    {
                        blockElementFactory.MoveNext();
                        factoryResult = blockElementFactory.Current.TryCreateFrom(token);
                    } while (factoryResult == null);
                return factoryResult;
            }
        }
    }
}