using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Mup.Creole;
using Mup.Creole.ElementProcessors;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup
{
    /// <summary>A markup parser implementation for Creole.</summary>
    public class CreoleParser : IMarkupParser
    {
        private delegate CreoleElementProcessor CreoleElementProcessorFactory(CreoleParserContext context, CreoleTokenRange tokens);

        private static readonly ReadOnlyCollection<CreoleElementProcessorFactory> _creoleElementProcessorFactories =
            new ReadOnlyCollection<CreoleElementProcessorFactory>(
                new CreoleElementProcessorFactory[]
                {
                    (context, tokens) => new CreoleCodeBlockElementProcessor(context, tokens),
                    (context, tokens) => new CreoleHeadingElementProcessor(context, tokens),
                    (context, tokens) => new CreoleHorizontalRuleElementProcessor(context, tokens),
                    (context, tokens) => new CreolePluginElementProcessor(context, tokens),
                    (context, tokens) => new CreoleTableElementProcessor(context, tokens),
                    (context, tokens) => new CreoleListElementProcessor(context, tokens),
                    (context, tokens) => new CreoleParagraphElementProcessor(context, tokens)
                }
            );

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

            return TaskAsyncOperationHelper.BeginParse(this, text, asyncCallback, asyncState);
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

            return TaskAsyncOperationHelper.BeginParse(this, reader, asyncCallback, asyncState);
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

            return TaskAsyncOperationHelper.BeginParse(this, reader, bufferSize, asyncCallback, asyncState);
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

        private IParseTree _Parse(ReadOnlyCollection<CreoleToken> tokens)
        {
            var context = new CreoleParserContext(Options.InlineHyperlinkProtocols);
            var tokenRange = new CreoleTokenRange(tokens);

            var elementInfos = new LinkedList<CreoleElementInfo>();

            foreach (var creoleElementFactory in _creoleElementProcessorFactories)
            {
                var startIndex = 0;
                for (var baseElementNode = elementInfos.First; baseElementNode != null; baseElementNode = baseElementNode.Next)
                {
                    using (var processor = creoleElementFactory(context, tokenRange.SubRange(startIndex, (baseElementNode.Value.StartIndex - startIndex))))
                        while (processor.MoveNext())
                            elementInfos.AddBefore(
                                baseElementNode,
                                new CreoleElementInfo(
                                    (startIndex + processor.Current.StartIndex),
                                    (startIndex + processor.Current.EndIndex),
                                    processor.Current.Element
                                )
                            );
                    startIndex = baseElementNode.Value.EndIndex;
                }

                using (var processor = creoleElementFactory(context, tokenRange.SubRange(startIndex, (tokens.Count - startIndex))))
                    while (processor.MoveNext())
                        elementInfos.AddLast(
                            new CreoleElementInfo(
                                (startIndex + processor.Current.StartIndex),
                                (startIndex + processor.Current.EndIndex),
                                processor.Current.Element
                            )
                        );
            }

            var creoleElements = new List<CreoleElement>(elementInfos.Count);
            foreach (var elementInfo in elementInfos)
                creoleElements.Add(elementInfo.Element);
            return new CreoleParseTree(creoleElements);
        }

#if netstandard10
        private async Task<IParseTree> _ParseAsync(ReadOnlyCollection<CreoleToken> tokens, CancellationToken cancellationToken)
        {
            var context = new CreoleParserContext(Options.InlineHyperlinkProtocols);
            var tokenRange = new CreoleTokenRange(tokens);

            var elementInfos = new LinkedList<CreoleElementInfo>();

            foreach (var creoleElementFactory in _creoleElementProcessorFactories)
            {
                var startIndex = 0;
                for (var baseElementNode = elementInfos.First; baseElementNode != null; baseElementNode = baseElementNode.Next)
                {
                    using (var processor = creoleElementFactory(context, tokenRange.SubRange(startIndex, (baseElementNode.Value.StartIndex - startIndex))))
                        while (processor.MoveNext())
                        {
                            elementInfos.AddBefore(
                               baseElementNode,
                               new CreoleElementInfo(
                                   (startIndex + processor.Current.StartIndex),
                                   (startIndex + processor.Current.EndIndex),
                                   processor.Current.Element
                               )
                           );

                            await Task.Yield();
                            cancellationToken.ThrowIfCancellationRequested();
                        }
                    startIndex = baseElementNode.Value.EndIndex;
                }

                using (var processor = creoleElementFactory(context, tokenRange.SubRange(startIndex, (tokens.Count - startIndex))))
                    while (processor.MoveNext())
                    {
                        elementInfos.AddLast(
                           new CreoleElementInfo(
                               (startIndex + processor.Current.StartIndex),
                               (startIndex + processor.Current.EndIndex),
                               processor.Current.Element
                           )
                       );

                        await Task.Yield();
                        cancellationToken.ThrowIfCancellationRequested();
                    }
            }

            var creoleElements = new List<CreoleElement>(elementInfos.Count);
            foreach (var elementInfo in elementInfos)
                creoleElements.Add(elementInfo.Element);
            return new CreoleParseTree(creoleElements);
        }
#endif
    }
}