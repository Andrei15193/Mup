using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Mup.CreoleTokenCode;
using static Mup.ElementBlock;
using static Mup.ElementMarkCode;
using static Mup.RichTextBlock;

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
        /// <returns>Returns an <see cref="IParseTree"/> that can be used to generate other formats.</returns>
        public IParseTree Parse(string text)
        {
            var scanner = _GetScanner();
            var scanResult = scanner.Scan(text);
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be used to generate other formats.</returns>
        public Task<IParseTree> ParseAsync(string text)
            => ParseAsync(text, CancellationToken.None);

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be used to generate other formats.</returns>
        public async Task<IParseTree> ParseAsync(string text, CancellationToken cancellationToken)
        {
            var scanner = _GetScanner();
            var scanResult = await scanner.ScanAsync(text, cancellationToken).ConfigureAwait(false);
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be used to generate other formats.</returns>
        public Task<IParseTree> ParseAsync(TextReader reader)
            => ParseAsync(reader);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be used to generate other formats.</returns>
        public async Task<IParseTree> ParseAsync(TextReader reader, CancellationToken cancellationToken)
        {
            var scanner = _GetScanner();
            var scanResult = await scanner.ScanAsync(reader, cancellationToken).ConfigureAwait(false);
            var parseTree = _Parse(scanResult);
            return parseTree;
        }

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be used to generate other formats.</returns>
        public Task<IParseTree> ParseAsync(TextReader reader, int bufferSize)
            => ParseAsync(reader, bufferSize, CancellationToken.None);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be used to generate other formats.</returns>
        public async Task<IParseTree> ParseAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
        {
            var scanner = _GetScanner();
            var scanResult = await scanner.ScanAsync(reader, bufferSize, cancellationToken).ConfigureAwait(false);
            var parseTree = _Parse(scanResult);
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

        private Scanner<CreoleTokenCode> _GetScanner()
            => new CreoleScanner();

        private IParseTree _Parse(ScanResult<CreoleTokenCode> scanResult)
        {
            var parser = new CreoleMarkupParser(scanResult, InlineHyperlinkProtocols);
            var parseTree = parser.Parse();
            return parseTree;
        }

        private class CreoleMarkupParser
        {
            private const char _LineFeed = '\n';
            private const char _ProtocolSchemeSeparator = ':';

            private const int _MaximumHeadingTokenLength = 6;

            private const int _StrongCharacterRepeatCount = 2;
            private const int _EmphasisCharacterRepeatCount = 2;

            private const int _HyperlinkCharacterRepeatCount = 2;
            private const int _HyperlinkTextSeparatorCharacterRepeatCount = 1;

            private const int _ImageCharacterRepeatCount = 2;
            private const int _ImageTextSeparatorCharacterRepeatCount = 1;

            private const int _LineBreakRepeatCount = 2;

            private const int _PluginCharacterRepeatCount = 2;

            private const int _PreformattedCharacterRepeatCount = 3;

            private readonly string _text;
            private readonly IEnumerable<Token<CreoleTokenCode>> _tokens;
            private readonly IEnumerable<string> _inlineHyperlinkProtocols;

            private ElementMark _strongStartMark = null;
            private ElementMark _emphasisStartMark = null;

            private ElementMark _hyperlinkStartMark = null;
            private ElementMark _hyperlinkDestinationMark = null;
            private ElementMark _hyperlinkTextSeparatorMark = null;

            private ElementMark _imageStartMark = null;
            private ElementMark _imageSourceMark = null;
            private ElementMark _imageTextSeparatorMark = null;

            private ElementMark _preformattedStartMark = null;

            private ElementMark _tableStartMark;
            private ElementMark _tableRowStartMark;
            private ElementMark _tableCellStartMark;

            private readonly IList<ElementMark> _marks = new List<ElementMark>();
            private readonly Stack<ElementBlock> _blocks = new Stack<ElementBlock>();
            private readonly Stack<RichTextBlock> _richTextBlocks = new Stack<RichTextBlock>();

            internal CreoleMarkupParser(ScanResult<CreoleTokenCode> scanResult, IEnumerable<string> inlineHyperlinkProtocols)
            {
                _text = scanResult.Text;
                _tokens = scanResult.Tokens;
                _inlineHyperlinkProtocols = inlineHyperlinkProtocols;
            }

            internal FlatParseTree Parse()
            {
                Token<CreoleTokenCode> previous = null, current = null, next = null;

                using (var token = _tokens.GetEnumerator())
                    if (token.MoveNext())
                    {
                        next = token.Current;
                        do
                        {
                            previous = current;
                            current = next;
                            if (token.MoveNext())
                                next = token.Current;
                            else
                                next = null;
                            _Process(previous, current, next);
                        } while (next != null);
                    }

                var marks = _BuildMarks();
                return new FlatParseTree(_text, marks);
            }

            private IEnumerable<ElementMark> _BuildMarks()
            {
                var marks = new List<ElementMark>();
                using (var mark = _marks.GetEnumerator())
                    if (mark.MoveNext())
                    {
                        var currentMark = mark.Current;
                        var markCode = currentMark.Code;
                        var markStart = currentMark.Start;
                        var markLength = currentMark.Length;
                        while (mark.MoveNext())
                        {
                            currentMark = mark.Current;
                            if (currentMark.Code == markCode && currentMark.Start == (markStart + markLength))
                                markLength += currentMark.Length;
                            else
                            {
                                marks.Add(new ElementMark
                                {
                                    Code = markCode,
                                    Start = markStart,
                                    Length = markLength
                                });
                                markCode = currentMark.Code;
                                markStart = currentMark.Start;
                                markLength = currentMark.Length;
                            }
                        }
                        marks.Add(new ElementMark
                        {
                            Code = markCode,
                            Start = markStart,
                            Length = markLength
                        });
                    }
                return marks;
            }

            private void _Process(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_blocks.Count == 0)
                    _BeginBlock(previousToken, currentToken, nextToken);
                else
                    _ProcessBlock(previousToken, currentToken, nextToken);
            }

            private void _BeginBlock(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                switch (currentToken.Code)
                {
                    case WhiteSpace:
                        break;

                    case Asterisk when (currentToken.Length == 1):
                        _BeginUnorderedList(previousToken, currentToken, nextToken);
                        _Process(previousToken, currentToken, nextToken);
                        break;

                    case Hash when (currentToken.Length == 1):
                        _BeginOrderedList(previousToken, currentToken, nextToken);
                        _Process(previousToken, currentToken, nextToken);
                        break;

                    case AngleOpen when (currentToken.Length >= _PluginCharacterRepeatCount):
                        _BeginPlugIn(previousToken, currentToken, nextToken);
                        break;

                    case BraceOpen when (currentToken.Length >= _PreformattedCharacterRepeatCount):
                        _BeginPreformattedBlock(previousToken, currentToken, nextToken);
                        break;

                    case Equal:
                        _BeginHeading(previousToken, currentToken, nextToken);
                        break;

                    case Dash when (currentToken.Length >= 4 && (nextToken == null || (nextToken.Code == WhiteSpace && _LineFeedCount(nextToken) > 0))):
                        _HorizontalRule(previousToken, currentToken, nextToken);
                        break;

                    case Pipe:
                        _BeginTable(previousToken, currentToken, nextToken);
                        break;

                    default:
                        _BeginParagraph(previousToken, currentToken, nextToken);
                        break;
                }
            }

            private void _ProcessBlock(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var currentBlock = _blocks.Peek();
                switch (currentBlock)
                {
                    case Paragraph:
                        _ProcessParagraph(previousToken, currentToken, nextToken);
                        break;

                    case Heading1:
                    case Heading2:
                    case Heading3:
                    case Heading4:
                    case Heading5:
                    case Heading6:
                        _ProcessHeading(previousToken, currentToken, nextToken);
                        break;

                    case UnorderedList:
                    case OrderedList:
                        _ProcessList(previousToken, currentToken, nextToken);
                        break;

                    case Table:
                        _ProcessTable(previousToken, currentToken, nextToken);
                        break;

                    case PreformattedBlock:
                        _ProcessPreformattedBlock(previousToken, currentToken, nextToken);
                        break;

                    case Plugin:
                        _ProcessPlugIn(previousToken, currentToken, nextToken);
                        break;

                    default:
                        throw new InvalidOperationException($"Unknown block: '{currentBlock}'.");
                }
            }

            private void _ProcessHeading(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Code == WhiteSpace && _LineFeedCount(currentToken) > 0)
                    _EndHeading(previousToken, currentToken, nextToken);
                else
                {
                    if (_marks[_marks.Count - 1].Code == PlainText || currentToken.Code != WhiteSpace)
                        if (currentToken.Code != Equal)
                            _AppendPlainText(currentToken);
                        else if (currentToken.Length > _MaximumHeadingTokenLength)
                            _AppendPlainText(currentToken.Start, (currentToken.Length - _MaximumHeadingTokenLength));
                    if (nextToken == null)
                        _EndHeading(previousToken, currentToken, nextToken);
                }
            }

            private void _ProcessParagraph(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Code == WhiteSpace && (_LineFeedCount(currentToken) >= 2 || (_LineFeedCount(currentToken) >= 1 && nextToken?.Code == Dash && nextToken.Length >= 4)))
                    _EndParagraph(previousToken, currentToken, nextToken);
                else
                    _ProcessRichText(previousToken, currentToken, nextToken);

                if (nextToken == null && _blocks.Count > 0 && _blocks.Peek() == Paragraph)
                    _EndParagraph(previousToken, currentToken, nextToken);
            }

            private void _ProcessRichText(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_richTextBlocks.Count > 0 && (_richTextBlocks.Peek() == InlineHyperlink || _richTextBlocks.Peek() == EscapedInlineHyperlink))
                    _ProcessInlineHyperlink(previousToken, currentToken, nextToken);
                else if (_hyperlinkStartMark != null && _hyperlinkTextSeparatorMark == null)
                    _ProcessHyperlinkDestination(previousToken, currentToken, nextToken);
                else if (_imageStartMark != null)
                    _ProcessImage(previousToken, currentToken, nextToken);
                else if (_preformattedStartMark != null)
                    _ProcessPreformatted(previousToken, currentToken, nextToken);
                else
                    switch (currentToken.Code)
                    {
                        case Tilde:
                            _ProcessTilde(previousToken, currentToken, nextToken);
                            break;

                        case Asterisk:
                            _ProcessStrong(previousToken, currentToken, nextToken);
                            break;

                        case Slash:
                            _ProcessEmphasis(previousToken, currentToken, nextToken);
                            break;

                        case BackSlash when (currentToken.Length >= _LineBreakRepeatCount):
                            _ProcessLineBreak(previousToken, currentToken, nextToken);
                            break;

                        case BracketOpen when (_hyperlinkStartMark == null && !_IsEscaped(previousToken, currentToken, nextToken) && currentToken.Length >= _HyperlinkCharacterRepeatCount):
                        case BracketOpen when (_hyperlinkStartMark == null && _IsEscaped(previousToken, currentToken, nextToken) && currentToken.Length > _HyperlinkCharacterRepeatCount):
                            _BeginHyperlink(previousToken, currentToken, nextToken);
                            break;

                        case BracketClose when (currentToken.Length >= _HyperlinkCharacterRepeatCount):
                            _EndHyperlink(previousToken, currentToken, nextToken);
                            break;

                        case BraceOpen when (_imageStartMark == null && !_IsEscaped(previousToken, currentToken, nextToken) && currentToken.Length == _ImageCharacterRepeatCount):
                        case BraceOpen when (_imageStartMark == null && _IsEscaped(previousToken, currentToken, nextToken) && currentToken.Length == (_ImageCharacterRepeatCount + 1)):
                            _BeginImage(previousToken, currentToken, nextToken);
                            break;

                        case BraceOpen when (_imageStartMark == null && !_IsEscaped(previousToken, currentToken, nextToken) && currentToken.Length >= _PreformattedCharacterRepeatCount):
                        case BraceOpen when (_imageStartMark == null && _IsEscaped(previousToken, currentToken, nextToken) && currentToken.Length > _PreformattedCharacterRepeatCount):
                            _BeginPreformatted(previousToken, currentToken, nextToken);
                            break;

                        case Text when (_IsProtocol(previousToken, currentToken, nextToken) && _hyperlinkStartMark == null):
                            _BeginInlineHyperlink(previousToken, currentToken, nextToken);
                            break;

                        case Dash:
                        case Pipe:
                            _marks.Add(new ElementMark
                            {
                                Code = PlainText,
                                Start = currentToken.Start,
                                Length = currentToken.Length
                            });
                            break;

                        default:
                            var startOffset = (_IsEscaped(previousToken, currentToken, nextToken) ? 1 : 0);
                            _marks.Add(new ElementMark
                            {
                                Code = PlainText,
                                Start = (currentToken.Start - startOffset),
                                Length = (currentToken.Length + startOffset)
                            });
                            break;
                    }
            }

            private void _ProcessTilde(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var tildeStart = currentToken.Start;
                var tildeCount = (currentToken.Length / 2);
                for (var tildeNumber = 0; tildeNumber < tildeCount; tildeNumber++, tildeStart += 2)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = tildeStart,
                        Length = 1
                    });
            }

            private void _ProcessStrong(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var isEscaped = _IsEscaped(previousToken, currentToken, nextToken);
                var start = (currentToken.Start + (isEscaped ? 1 : 0));
                var end = currentToken.End;
                var length = currentToken.Length;
                if (isEscaped)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = currentToken.Start,
                        Length = 1
                    });
                    start++;
                    length--;
                }

                var strongMarkCount = (length / 2);
                for (int strongMarkStart = start, strongMarkNumber = 0; strongMarkNumber < strongMarkCount; strongMarkNumber++, strongMarkStart += _StrongCharacterRepeatCount)
                    _StartOrEndStrong(strongMarkStart, previousToken, currentToken, nextToken);

                if (length % 2 == 1)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (currentToken.End - 1),
                        Length = 1
                    });
            }

            private void _StartOrEndStrong(int strongMarkStart, Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_strongStartMark == null)
                {
                    _strongStartMark = new ElementMark
                    {
                        Code = StrongStart,
                        Start = strongMarkStart,
                        Length = _StrongCharacterRepeatCount
                    };
                    _marks.Add(_strongStartMark);
                    _richTextBlocks.Push(Strong);
                }
                else if (_richTextBlocks.Pop() == Strong)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = StrongEnd,
                        Start = strongMarkStart,
                        Length = _StrongCharacterRepeatCount
                    });
                    _strongStartMark = null;
                }
                else
                {
                    _strongStartMark.Code = PlainText;
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = strongMarkStart,
                        Length = _StrongCharacterRepeatCount
                    });
                    _strongStartMark = null;
                }
            }

            private void _ProcessEmphasis(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var isEscaped = _IsEscaped(previousToken, currentToken, nextToken);
                var start = (currentToken.Start + (isEscaped ? 1 : 0));
                var end = currentToken.End;
                var length = currentToken.Length;
                if (isEscaped)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = currentToken.Start,
                        Length = 1
                    });
                    start++;
                    length--;
                }

                var emphasisMarkCount = (length / 2);
                for (int emphasisMarkStart = start, emphasisMarkNumber = 0; emphasisMarkNumber < emphasisMarkCount; emphasisMarkNumber++, emphasisMarkStart += _EmphasisCharacterRepeatCount)
                    _StartOrEndEmphasis(emphasisMarkStart, previousToken, currentToken, nextToken);

                if (length % 2 == 1)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (currentToken.End - 1),
                        Length = 1
                    });
            }

            private void _StartOrEndEmphasis(int emphasisMarkStart, Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_emphasisStartMark == null)
                {
                    _emphasisStartMark = new ElementMark
                    {
                        Code = EmphasisStart,
                        Start = emphasisMarkStart,
                        Length = _EmphasisCharacterRepeatCount
                    };
                    _marks.Add(_emphasisStartMark);
                    _richTextBlocks.Push(Emphasis);
                }
                else if (_richTextBlocks.Pop() == Emphasis)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = EmphasisEnd,
                        Start = emphasisMarkStart,
                        Length = _EmphasisCharacterRepeatCount
                    });
                    _emphasisStartMark = null;
                }
                else
                {
                    _emphasisStartMark.Code = PlainText;
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = emphasisMarkStart,
                        Length = _EmphasisCharacterRepeatCount
                    });
                    _emphasisStartMark = null;
                }
            }

            private void _BeginHeading(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var headingMark = _GetHeadingStartMark(currentToken);
                var block = _GetHeadingBlockFor(headingMark);
                _blocks.Push(block);
                if (currentToken.Length <= _MaximumHeadingTokenLength)
                    _marks.Add(new ElementMark
                    {
                        Code = headingMark,
                        Start = currentToken.Start,
                        Length = currentToken.Length
                    });
                else
                {
                    _marks.Add(new ElementMark
                    {
                        Code = headingMark,
                        Start = currentToken.Start,
                        Length = _MaximumHeadingTokenLength
                    });
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (currentToken.Start + _MaximumHeadingTokenLength),
                        Length = (currentToken.Length - _MaximumHeadingTokenLength)
                    });
                }
            }

            private void _EndHeading(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var headingBlock = _blocks.Pop();
                if (currentToken.Code == Equal && currentToken.Length > _MaximumHeadingTokenLength)
                {
                    var textLength = (currentToken.Length - _MaximumHeadingTokenLength);
                    _marks.Add(new ElementMark
                    {
                        Code = _GetHeadingEndMarkFor(headingBlock),
                        Start = (previousToken.Start + textLength),
                        Length = _MaximumHeadingTokenLength
                    });
                }
                else
                {
                    var plainTextMark = _marks[_marks.Count - 1];
                    _TrimWhiteSpaceEnd(plainTextMark);
                    if (plainTextMark.Length == 0)
                        _marks.RemoveAt(_marks.Count - 1);

                    _marks.Add(new ElementMark
                    {
                        Code = _GetHeadingEndMarkFor(headingBlock),
                        Start = currentToken.Start,
                        Length = (currentToken.Code == Equal ? currentToken.Length : 0)
                    });
                }
            }

            private void _HorizontalRule(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _marks.Add(new ElementMark
                {
                    Code = HorizontalRule,
                    Start = currentToken.Start,
                    Length = currentToken.Length
                });
            }

            private void _BeginUnorderedList(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _blocks.Push(UnorderedList);
                _marks.Add(new ElementMark
                {
                    Code = UnorderedListStart,
                    Start = currentToken.Start
                });
            }

            private void _BeginOrderedList(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _blocks.Push(OrderedList);
                _marks.Add(new ElementMark
                {
                    Code = OrderedListStart,
                    Start = currentToken.Start
                });
            }

            private void _ProcessList(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Code == WhiteSpace && _LineFeedCount(currentToken) > 1)
                    while (_blocks.Count > 0)
                        _EndList(previousToken, currentToken, nextToken);
                else
                {
                    var listIndent = _blocks.Count;
                    switch (currentToken.Code)
                    {
                        case Asterisk when (currentToken.Length == listIndent && (previousToken == null || (previousToken.Code == WhiteSpace && _LineFeedCount(previousToken) > 0))):
                            if (_blocks.Peek() != UnorderedList)
                            {
                                _EndList(previousToken, currentToken, nextToken);
                                _BeginUnorderedList(previousToken, currentToken, nextToken);
                            }
                            else if (_marks[_marks.Count - 1].Code != UnorderedListStart)
                                _EndListItem(previousToken, currentToken, nextToken);

                            _BeginListItem(previousToken, currentToken, nextToken);
                            break;

                        case Asterisk when (currentToken.Length > listIndent && (previousToken == null || (previousToken.Code == WhiteSpace && _LineFeedCount(previousToken) > 0))):
                            do
                            {
                                _BeginUnorderedList(previousToken, currentToken, nextToken);
                                _BeginListItem(previousToken, currentToken, nextToken);
                                listIndent = _blocks.Count;
                            } while (currentToken.Length > listIndent);
                            break;

                        case Asterisk when (currentToken.Length < listIndent && (previousToken == null || (previousToken.Code == WhiteSpace && _LineFeedCount(previousToken) > 0))):
                            do
                            {
                                _EndList(previousToken, currentToken, nextToken);
                                listIndent = _blocks.Count;
                            } while (currentToken.Length < listIndent);
                            _EndListItem(previousToken, currentToken, nextToken);
                            _BeginListItem(previousToken, currentToken, nextToken);
                            break;

                        case Hash when (currentToken.Length == listIndent && (previousToken == null || (previousToken.Code == WhiteSpace && _LineFeedCount(previousToken) > 0))):
                            if (_blocks.Peek() != OrderedList)
                            {
                                _EndList(previousToken, currentToken, nextToken);
                                _BeginOrderedList(previousToken, currentToken, nextToken);
                            }
                            else if (_marks[_marks.Count - 1].Code != OrderedListStart)
                                _EndListItem(previousToken, currentToken, nextToken);

                            _BeginListItem(previousToken, currentToken, nextToken);
                            break;

                        case Hash when (currentToken.Length > listIndent && (previousToken == null || (previousToken.Code == WhiteSpace && _LineFeedCount(previousToken) > 0))):
                            do
                            {
                                _BeginOrderedList(previousToken, currentToken, nextToken);
                                _BeginListItem(previousToken, currentToken, nextToken);
                                listIndent = _blocks.Count;
                            } while (currentToken.Length > listIndent);
                            break;

                        case Hash when (currentToken.Length < listIndent && (previousToken == null || (previousToken.Code == WhiteSpace && _LineFeedCount(previousToken) > 0))):
                            do
                            {
                                _EndList(previousToken, currentToken, nextToken);
                                listIndent = _blocks.Count;
                            } while (currentToken.Length < listIndent);
                            _EndListItem(previousToken, currentToken, nextToken);
                            _BeginListItem(previousToken, currentToken, nextToken);
                            break;

                        case WhiteSpace when (_marks[_marks.Count - 1].Code == ListItemStart || _marks[_marks.Count - 1].Code == ListItemEnd):
                        case WhiteSpace when (_LineFeedCount(currentToken) > 0 && (_marks[_marks.Count - 1].Code == ListItemStart || _marks[_marks.Count - 1].Code == ListItemEnd || nextToken?.Code == Asterisk || nextToken?.Code == Hash)):
                            break;

                        default:
                            _ProcessRichText(previousToken, currentToken, nextToken);
                            break;
                    }
                }

                if (nextToken == null)
                    while (_blocks.Count > 0)
                        _EndList(previousToken, currentToken, nextToken);
            }

            private void _EndList(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _EndListItem(previousToken, currentToken, nextToken);
                var listType = _blocks.Pop();
                _marks.Add(new ElementMark
                {
                    Code = (listType == UnorderedList ? UnorderedListEnd : OrderedListEnd),
                    Start = currentToken.Start
                });
            }

            private void _BeginListItem(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _marks.Add(new ElementMark
                {
                    Code = ListItemStart,
                    Start = currentToken.Start,
                    Length = currentToken.Length
                });
            }

            private void _EndListItem(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _ClearRichText(previousToken, currentToken, nextToken);
                _marks.Add(new ElementMark
                {
                    Code = ListItemEnd,
                    Start = currentToken.Start
                });
            }

            private void _BeginInlineHyperlink(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_IsEscaped(previousToken, currentToken, nextToken))
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = currentToken.Start,
                        Length = currentToken.Length
                    });
                    _richTextBlocks.Push(EscapedInlineHyperlink);
                }
                else
                {
                    _marks.Add(new ElementMark
                    {
                        Code = HyperlinkStart,
                        Start = currentToken.Start
                    });
                    _marks.Add(new ElementMark
                    {
                        Code = HyperlinkDestination,
                        Start = currentToken.Start,
                        Length = currentToken.Length
                    });
                    _richTextBlocks.Push(InlineHyperlink);
                }
            }

            private void _ProcessInlineHyperlink(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                switch (currentToken.Code)
                {
                    case WhiteSpace:
                        if (_richTextBlocks.Peek() == InlineHyperlink)
                            _EndInlineHyperlink(previousToken, currentToken, nextToken);
                        _marks.Add(new ElementMark
                        {
                            Code = PlainText,
                            Start = currentToken.Start,
                            Length = currentToken.Length
                        });
                        break;

                    default:
                        _marks[_marks.Count - 1].Length += currentToken.Length;
                        break;
                }
            }

            private void _EndInlineHyperlink(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var hyperlinkDestination = _marks[_marks.Count - 1];
                if (hyperlinkDestination.Code == HyperlinkDestination)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = hyperlinkDestination.Start,
                        Length = hyperlinkDestination.Length
                    });
                _marks.Add(new ElementMark
                {
                    Code = HyperlinkEnd,
                    Start = currentToken.Start
                });
                _richTextBlocks.Pop();
            }

            private void _BeginHyperlink(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Length > _HyperlinkCharacterRepeatCount)
                    _AppendPlainText(currentToken.Start, currentToken.Length - _HyperlinkCharacterRepeatCount);

                _hyperlinkStartMark = new ElementMark
                {
                    Code = HyperlinkStart,
                    Start = (currentToken.End - _HyperlinkCharacterRepeatCount),
                    Length = _HyperlinkCharacterRepeatCount
                };
                _marks.Add(_hyperlinkStartMark);
                _hyperlinkDestinationMark = new ElementMark
                {
                    Code = HyperlinkDestination,
                    Start = currentToken.End
                };
                _marks.Add(_hyperlinkDestinationMark);
                _richTextBlocks.Push(Hyperlink);
            }

            private void _ProcessHyperlinkDestination(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                switch (currentToken.Code)
                {
                    case BracketClose when (currentToken.Length >= _HyperlinkCharacterRepeatCount):
                        _EndHyperlink(previousToken, currentToken, nextToken);
                        break;

                    case Pipe:
                        _hyperlinkTextSeparatorMark = new ElementMark
                        {
                            Code = HyperlinkTextSeparator,
                            Start = currentToken.Start,
                            Length = _HyperlinkTextSeparatorCharacterRepeatCount
                        };
                        _marks.Add(_hyperlinkTextSeparatorMark);
                        if (currentToken.Length > _HyperlinkTextSeparatorCharacterRepeatCount)
                            _AppendPlainText(
                                (currentToken.Start + _HyperlinkTextSeparatorCharacterRepeatCount),
                                (currentToken.Length - _HyperlinkTextSeparatorCharacterRepeatCount));
                        break;

                    default:
                        _hyperlinkDestinationMark.Length += currentToken.Length;
                        break;
                }
            }

            private void _EndHyperlink(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var hyperlinkDestination = _marks[_marks.Count - 1];
                if (hyperlinkDestination.Code == HyperlinkDestination)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = hyperlinkDestination.Start,
                        Length = hyperlinkDestination.Length
                    });

                _marks.Add(new ElementMark
                {
                    Code = HyperlinkEnd,
                    Start = currentToken.Start,
                    Length = _HyperlinkCharacterRepeatCount
                });
                if (currentToken.Length > _HyperlinkCharacterRepeatCount)
                    _AppendPlainText(
                        (currentToken.Start + _HyperlinkCharacterRepeatCount),
                        (currentToken.Length - _HyperlinkCharacterRepeatCount));

                _ClearHyperlinkRichText();

                _hyperlinkStartMark = null;
                _hyperlinkDestinationMark = null;
                _hyperlinkTextSeparatorMark = null;
            }

            private void _ClearHyperlinkRichText()
            {
                var richTextBlock = _richTextBlocks.Pop();
                while (richTextBlock != Hyperlink)
                {
                    switch (richTextBlock)
                    {
                        case Strong:
                            _ClearStrong();
                            break;
                        case Emphasis:
                            _ClearEmphasis();
                            break;

                        case Image:
                            _ClearImage();
                            break;
                    }
                    richTextBlock = _richTextBlocks.Pop();
                }
            }

            private void _BeginImage(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Length > _ImageCharacterRepeatCount)
                    _AppendPlainText(currentToken.Start, currentToken.Length - _ImageCharacterRepeatCount);

                _imageStartMark = new ElementMark
                {
                    Code = ImageStart,
                    Start = (currentToken.End - _ImageCharacterRepeatCount),
                    Length = _ImageCharacterRepeatCount
                };
                _marks.Add(_imageStartMark);
                _imageSourceMark = new ElementMark
                {
                    Code = ImageSource,
                    Start = currentToken.End
                };
                _marks.Add(_imageSourceMark);
                _richTextBlocks.Push(Image);
            }

            private void _ProcessImage(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_imageTextSeparatorMark == null)
                    _ProcessImageSource(previousToken, currentToken, nextToken);
                else
                    _ProcessImageAternativeText(previousToken, currentToken, nextToken);
            }

            private void _ProcessImageSource(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                switch (currentToken.Code)
                {
                    case BraceClose when (currentToken.Length >= _ImageCharacterRepeatCount):
                        _EndImage(previousToken, currentToken, nextToken);
                        break;

                    case Pipe:
                        _imageTextSeparatorMark = new ElementMark
                        {
                            Code = HyperlinkTextSeparator,
                            Start = currentToken.Start,
                            Length = _ImageTextSeparatorCharacterRepeatCount
                        };
                        _marks.Add(_imageTextSeparatorMark);
                        if (currentToken.Length > _ImageTextSeparatorCharacterRepeatCount)
                            _AppendPlainText(
                                (currentToken.Start + _ImageTextSeparatorCharacterRepeatCount),
                                (currentToken.Length - _ImageTextSeparatorCharacterRepeatCount));
                        break;

                    default:
                        _imageSourceMark.Length += currentToken.Length;
                        break;
                }
            }

            private void _ProcessImageAternativeText(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                switch (currentToken.Code)
                {
                    case BraceClose when (currentToken.Length >= _ImageCharacterRepeatCount):
                        _EndImage(previousToken, currentToken, nextToken);
                        break;

                    default:
                        var startOffset = (_IsEscaped(previousToken, currentToken, nextToken) ? 1 : 0);
                        _marks.Add(new ElementMark
                        {
                            Code = PlainText,
                            Start = (currentToken.Start - startOffset),
                            Length = (currentToken.Length + startOffset)
                        });
                        break;
                }
            }

            private void _EndImage(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var imageSource = _marks[_marks.Count - 1];
                if (imageSource.Code == ImageSource)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (imageSource.Start + imageSource.Length)
                    });

                _marks.Add(new ElementMark
                {
                    Code = ImageEnd,
                    Start = currentToken.Start,
                    Length = _ImageCharacterRepeatCount
                });
                if (currentToken.Length > _ImageCharacterRepeatCount)
                    _AppendPlainText(
                        (currentToken.Start + _ImageCharacterRepeatCount),
                        (currentToken.Length - _ImageCharacterRepeatCount));

                _imageStartMark = null;
                _imageSourceMark = null;
                _imageTextSeparatorMark = null;

                _richTextBlocks.Pop();
            }

            private void _ProcessLineBreak(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var isEscaped = _IsEscaped(previousToken, currentToken, nextToken);
                var start = (currentToken.Start + (isEscaped ? 1 : 0));
                var end = currentToken.End;
                var length = currentToken.Length;
                if (isEscaped)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = currentToken.Start,
                        Length = 1
                    });
                    start++;
                    length--;
                }

                var lineBreakMarkCount = (length / 2);
                for (int lineBreakMarkStart = start, emphasisMarkNumber = 0; emphasisMarkNumber < lineBreakMarkCount; emphasisMarkNumber++, lineBreakMarkStart += _LineBreakRepeatCount)
                    _marks.Add(new ElementMark
                    {
                        Code = LineBreak,
                        Start = lineBreakMarkStart,
                        Length = _LineBreakRepeatCount
                    });

                if (length % 2 == 1)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (currentToken.End - 1),
                        Length = 1
                    });
            }

            private void _BeginPreformatted(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Length > _PreformattedCharacterRepeatCount)
                    _AppendPlainText(currentToken.Start, currentToken.Length - _PreformattedCharacterRepeatCount);

                _preformattedStartMark = new ElementMark
                {
                    Code = PreformattedTextStart,
                    Start = (currentToken.End - _PreformattedCharacterRepeatCount),
                    Length = _PreformattedCharacterRepeatCount
                };
                _marks.Add(_preformattedStartMark);
                _richTextBlocks.Push(Preformatted);
            }

            private void _ProcessPreformatted(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                switch (currentToken.Code)
                {
                    case BraceClose when (currentToken.Length >= _PreformattedCharacterRepeatCount):
                        _EndPreformatted(previousToken, currentToken, nextToken);
                        break;

                    default:
                        _marks.Add(new ElementMark
                        {
                            Code = PlainText,
                            Start = currentToken.Start,
                            Length = currentToken.Length
                        });
                        break;
                }
            }

            private void _EndPreformatted(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Length > _PreformattedCharacterRepeatCount)
                    _AppendPlainText(
                        currentToken.Start,
                        (currentToken.Length - _PreformattedCharacterRepeatCount));
                _marks.Add(new ElementMark
                {
                    Code = PreformattedTextEnd,
                    Start = currentToken.Start + _PreformattedCharacterRepeatCount,
                    Length = _PreformattedCharacterRepeatCount
                });

                _preformattedStartMark = null;

                _richTextBlocks.Pop();
            }

            private void _BeginParagraph(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _blocks.Push(Paragraph);
                _marks.Add(new ElementMark
                {
                    Code = ParagraphStart,
                    Start = currentToken.Start
                });
                _Process(previousToken, currentToken, nextToken);
            }

            private void _EndParagraph(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _ClearRichText(previousToken, currentToken, nextToken);
                _marks.Add(new ElementMark
                {
                    Code = ParagraphEnd,
                    Start = currentToken.End
                });
                _blocks.Pop();
            }

            private void _BeginPreformattedBlock(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _blocks.Push(PreformattedBlock);
                _marks.Add(new ElementMark
                {
                    Code = PreformattedBlockStart,
                    Start = currentToken.Start,
                    Length = _PreformattedCharacterRepeatCount
                });

                if (currentToken.Length > _PreformattedCharacterRepeatCount)
                    _AppendPlainText(
                        (currentToken.Start + _PreformattedCharacterRepeatCount),
                        (currentToken.Length - _PreformattedCharacterRepeatCount));
            }

            private void _ProcessPreformattedBlock(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Code == BraceClose && currentToken.Length >= _PreformattedCharacterRepeatCount
                    && (nextToken == null || (nextToken.Code == WhiteSpace && _LineFeedCount(nextToken) > 0)))
                    _EndPreformattedBlock(previousToken, currentToken, nextToken);
                else
                    _AppendPlainText(currentToken);
            }

            private void _EndPreformattedBlock(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Code == BraceClose)
                {
                    if (currentToken.Length > _PreformattedCharacterRepeatCount)
                        _AppendPlainText(
                            currentToken.Start,
                            (currentToken.Length - _PreformattedCharacterRepeatCount));
                    _marks.Add(new ElementMark
                    {
                        Code = PreformattedBlockEnd,
                        Start = (currentToken.End - _PreformattedCharacterRepeatCount),
                        Length = _PreformattedCharacterRepeatCount
                    });
                }
                else
                    _marks.Add(new ElementMark
                    {
                        Code = PreformattedBlockEnd,
                        Start = currentToken.End
                    });
                _blocks.Pop();
            }

            private void _BeginPlugIn(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _blocks.Push(Plugin);
                _marks.Add(new ElementMark
                {
                    Code = PluginStart,
                    Start = currentToken.Start,
                    Length = _PluginCharacterRepeatCount
                });

                if (currentToken.Length > _PluginCharacterRepeatCount)
                    _AppendPlainText(
                        (currentToken.Start + _PluginCharacterRepeatCount),
                        (currentToken.Length - _PluginCharacterRepeatCount));
            }

            private void _ProcessPlugIn(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Code == AngleClose && currentToken.Length >= _PluginCharacterRepeatCount
                    && (nextToken == null || (nextToken.Code == WhiteSpace && _LineFeedCount(nextToken) > 0)))
                    _EndPlugIn(previousToken, currentToken, nextToken);
                else
                    _AppendPlainText(currentToken);
            }

            private void _EndPlugIn(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (currentToken.Code == AngleClose)
                {
                    if (currentToken.Length > _PluginCharacterRepeatCount)
                        _AppendPlainText(
                            currentToken.Start,
                            (currentToken.Length - _PluginCharacterRepeatCount));
                    _marks.Add(new ElementMark
                    {
                        Code = PluginEnd,
                        Start = (currentToken.End - _PluginCharacterRepeatCount),
                        Length = _PluginCharacterRepeatCount
                    });
                }
                else
                    _marks.Add(new ElementMark
                    {
                        Code = PluginEnd,
                        Start = currentToken.End
                    });
                _blocks.Pop();
            }

            private void _BeginTable(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _blocks.Push(Table);
                _tableStartMark = new ElementMark
                {
                    Code = TableStart,
                    Start = currentToken.Start
                };
                _marks.Add(_tableStartMark);
                _Process(previousToken, currentToken, nextToken);
            }

            private void _ProcessTable(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                switch (currentToken.Code)
                {
                    case Pipe when (_tableRowStartMark != null && (nextToken?.Code == WhiteSpace && _LineFeedCount(nextToken) > 0)):
                        break;

                    case Pipe when (_tableRowStartMark == null):
                        _BeginTableRow(previousToken, currentToken, nextToken);
                        break;

                    case Pipe when (_tableCellStartMark != null && !_IsEscaped(previousToken, currentToken, nextToken)):
                        _EndTableCell(previousToken, currentToken, nextToken);
                        break;

                    case Equal when (_tableCellStartMark == null):
                        _BeginTableHeaderCell(previousToken, currentToken, nextToken);
                        break;

                    case WhiteSpace when (_LineFeedCount(currentToken) > 1):
                    case WhiteSpace when (_LineFeedCount(currentToken) == 1 && nextToken?.Code != Pipe):
                        _EndTable(previousToken, currentToken, nextToken);
                        break;

                    case WhiteSpace when (_LineFeedCount(currentToken) > 0):
                        _EndTableRow(previousToken, currentToken, nextToken);
                        break;

                    case WhiteSpace when (_tableRowStartMark == null || _tableCellStartMark == null):
                        break;

                    default:
                        if (_tableCellStartMark == null)
                            _BeginTableCell(previousToken, currentToken, nextToken);
                        _ProcessRichText(previousToken, currentToken, nextToken);
                        break;
                }

                if (nextToken == null)
                    _EndTable(previousToken, currentToken, nextToken);
            }

            private void _EndTable(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_tableRowStartMark != null)
                    _EndTableRow(previousToken, currentToken, nextToken);
                _marks.Add(new ElementMark
                {
                    Code = TableEnd,
                    Start = currentToken.Start
                });
                _tableStartMark = null;
                _blocks.Pop();
            }

            private void _BeginTableRow(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _tableRowStartMark = new ElementMark
                {
                    Code = TableRowStart,
                    Start = currentToken.Start,
                    Length = currentToken.Length
                };
                _marks.Add(_tableRowStartMark);
            }

            private void _EndTableRow(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_tableCellStartMark != null)
                    _EndTableCell(previousToken, currentToken, nextToken);

                _marks.Add(new ElementMark
                {
                    Code = TableRowEnd,
                    Start = currentToken.Start
                });
                _tableRowStartMark = null;
            }

            private void _BeginTableHeaderCell(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _tableCellStartMark = new ElementMark
                {
                    Code = TableHeaderCellStart,
                    Start = currentToken.Start,
                    Length = currentToken.Length
                };
                _marks.Add(_tableCellStartMark);
            }

            private void _BeginTableCell(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _tableCellStartMark = new ElementMark
                {
                    Code = TableCellStart,
                    Start = currentToken.Start,
                    Length = currentToken.Length
                };
                _marks.Add(_tableCellStartMark);
            }

            private void _EndTableCell(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                _ClearRichText(previousToken, currentToken, nextToken);
                _marks.Add(new ElementMark
                {
                    Code = (_tableCellStartMark.Code == TableHeaderCellStart ? TableHeaderCellEnd : TableCellEnd),
                    Start = currentToken.Start
                });
                _tableCellStartMark = null;
            }

            private void _ClearRichText(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                if (_richTextBlocks.Count > 0 && _richTextBlocks.Peek() == InlineHyperlink)
                    _EndInlineHyperlink(previousToken, currentToken, nextToken);

                _ClearStrong();
                _ClearEmphasis();
                _ClearHyperlink();
                _ClearImage();
                _ClearPreformatted();

                _richTextBlocks.Clear();
            }

            private void _ClearStrong()
            {
                if (_strongStartMark != null)
                {
                    _strongStartMark.Code = PlainText;
                    _strongStartMark = null;
                }
            }

            private void _ClearEmphasis()
            {
                if (_emphasisStartMark != null)
                {
                    _emphasisStartMark.Code = PlainText;
                    _emphasisStartMark = null;
                }
            }

            private void _ClearHyperlink()
            {
                if (_hyperlinkStartMark != null)
                {
                    _hyperlinkStartMark.Code = PlainText;
                    _hyperlinkStartMark = null;
                }
                if (_hyperlinkDestinationMark != null)
                {
                    _hyperlinkDestinationMark.Code = PlainText;
                    _hyperlinkDestinationMark = null;
                }
                if (_hyperlinkTextSeparatorMark != null)
                {
                    _hyperlinkTextSeparatorMark.Code = PlainText;
                    _hyperlinkTextSeparatorMark = null;
                }
            }

            private void _ClearImage()
            {
                if (_imageStartMark != null)
                {
                    _imageStartMark.Code = PlainText;
                    _imageStartMark = null;
                }
                if (_imageSourceMark != null)
                {
                    _imageSourceMark.Code = PlainText;
                    _imageSourceMark = null;
                }
                if (_imageTextSeparatorMark != null)
                {
                    _imageTextSeparatorMark.Code = PlainText;
                    _imageTextSeparatorMark = null;
                }
            }

            private void _ClearPreformatted()
            {
                if (_preformattedStartMark != null)
                {
                    _preformattedStartMark.Code = PlainText;
                    _preformattedStartMark = null;
                }
            }

            private void _TrimWhiteSpaceEnd(ElementMark mark)
            {
                while (mark.Length > 0 && char.IsWhiteSpace(_text, (mark.Length + mark.Start - 1)))
                    mark.Length--;
            }

            private void _AppendPlainText(Token<CreoleTokenCode> token)
            {
                _AppendPlainText(token.Start, token.Length);
            }

            private void _AppendPlainText(int start, int length)
            {
                var mark = (_marks.Count == 0 ? null : _marks[_marks.Count - 1]);
                if (mark == null || mark.Code != PlainText || (mark.Start + mark.Length) < start)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = start,
                        Length = length
                    });
                else
                    mark.Length += length;
            }

            private static ElementMarkCode _GetHeadingStartMark(Token<CreoleTokenCode> currentToken)
            {
                switch (currentToken.Length)
                {
                    case 1:
                        return Heading1Start;

                    case 2:
                        return Heading2Start;

                    case 3:
                        return Heading3Start;

                    case 4:
                        return Heading4Start;

                    case 5:
                        return Heading5Start;

                    default:
                        return Heading6Start;
                }
            }

            private static ElementBlock _GetHeadingBlockFor(ElementMarkCode mark)
            {
                switch (mark)
                {
                    case Heading1Start:
                        return Heading1;

                    case Heading2Start:
                        return Heading2;

                    case Heading3Start:
                        return Heading3;

                    case Heading4Start:
                        return Heading4;

                    case Heading5Start:
                        return Heading5;

                    case Heading6Start:
                        return Heading6;

                    default:
                        throw new InvalidOperationException($"The {mark} mark is not a heading one.");
                }
            }

            private static ElementMarkCode _GetHeadingEndMarkFor(ElementBlock block)
            {
                switch (block)
                {
                    case Heading1:
                        return Heading1End;

                    case Heading2:
                        return Heading2End;

                    case Heading3:
                        return Heading3End;

                    case Heading4:
                        return Heading4End;

                    case Heading5:
                        return Heading5End;

                    case Heading6:
                        return Heading6End;

                    default:
                        throw new InvalidOperationException($"The {block} block is not a heading one.");
                }
            }

            private static bool _IsEscaped(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
                => (previousToken != null && previousToken.Code == Tilde && (previousToken.Length % 2 == 1));

            private bool _IsProtocol(Token<CreoleTokenCode> previousToken, Token<CreoleTokenCode> currentToken, Token<CreoleTokenCode> nextToken)
            {
                var index = currentToken.Start;
                var end = (currentToken.End - 1);

                if (_text[end] != _ProtocolSchemeSeparator)
                    return false;

                var protocolLength = (currentToken.Length - 1);
                var candidateProtocols = _inlineHyperlinkProtocols.Where(protocol => protocol.Length == protocolLength).ToList();
                while (candidateProtocols.Count > 0 && index < end)
                {
                    var candidateProtocolIndex = 0;
                    while (candidateProtocolIndex < candidateProtocols.Count)
                    {
                        var candidateProtocol = candidateProtocols[candidateProtocolIndex];
                        if (char.ToLowerInvariant(_text[index]) == candidateProtocol[(index - currentToken.Start)])
                            candidateProtocolIndex++;
                        else
                            candidateProtocols.RemoveAt(candidateProtocolIndex);
                    }
                    index++;
                }

                return (candidateProtocols.Count > 0);
            }

            private int _LineFeedCount(Token<CreoleTokenCode> token)
            {
                var count = 0;

                var end = (token.Start + token.Length);
                for (var index = token.Start; index < end; index++)
                    if (_text[index] == _LineFeed)
                        count++;

                return count;
            }
        }
    }
}