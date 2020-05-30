using Mup.Creole;
using Mup.Creole.ElementProcessors;
using Mup.Creole.Elements;
using Mup.Scanner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Mup
{
    /// <summary>A markup parser implementation for Creole.</summary>
    public class CreoleParser : IMarkupParser
    {
        private const int _defaultBufferSize = 1024;

        private delegate CreoleElementProcessor CreoleElementProcessorFactory(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens);

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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is <c>null</c>.</exception>
        public IParseTree Parse(string text)
        {
            using (var stringReader = new StringReader(text))
                return Parse(stringReader);
        }

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is <c>null</c>.</exception>
        public IParseTree Parse(TextReader reader)
            => Parse(reader, _defaultBufferSize);

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="reader"/> is <c>null</c>.</exception>
        public IParseTree Parse(TextReader reader, int bufferSize)
        {
            var scanner = new CreoleScanner();
            scanner.Scan(reader, bufferSize);
            var scanResult = scanner.Result;
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        private IParseTree _Parse(IReadOnlyList<Token<CreoleTokenCode>> tokens)
        {
            var context = new CreoleParserContext(Options.InlineHyperlinkProtocols);
            var tokenRange = new TokenRange<CreoleTokenCode>(tokens);

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
    }
}