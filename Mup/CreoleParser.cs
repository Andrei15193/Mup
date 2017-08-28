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
            private readonly IEnumerable<IToken<CreoleTokenCode>> _tokens;
            private readonly IEnumerable<string> _inlineHyperlinkProtocols;

            private IToken<CreoleTokenCode> _currentToken;

            private ElementMark _strongStartMark = null;
            private ElementMark _emphasisStartMark = null;

            private ElementMark _hyperlinkStartMark = null;
            private ElementMark _hyperlinkDestinationMark = null;
            private ElementMark _hyperlinkTextSeparatorMark = null;

            private ElementMark _imageStartMark = null;
            private ElementMark _imageSourceMark = null;
            private ElementMark _imageTextSeparatorMark = null;

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
                IToken<CreoleTokenCode> firstToken = null;
                using (var token = _tokens.GetEnumerator())
                    if (token.MoveNext())
                        firstToken = token.Current;

                _currentToken = firstToken;
                while (_currentToken != null)
                {
                    _Process();
                    _currentToken = _currentToken.Next;
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

            private void _Process()
            {
                if (_blocks.Count == 0)
                    _BeginBlock();
                else
                    _ProcessBlock();
            }

            private void _BeginBlock()
            {
                var processed = false;
                if (_currentToken.Code == BraceOpen
                    && _currentToken.Length == _PreformattedCharacterRepeatCount
                    && (_currentToken.Previous == null || _EndsOnNewLine(_currentToken.Previous))
                    && _currentToken.Next?.Code == WhiteSpace
                    && _LineFeedCount(_currentToken.Next) > 0)
                    processed = _TryProcessPreformattedBlock();
                else if (_currentToken.Code == AngleOpen && _currentToken.Length >= _PluginCharacterRepeatCount && !_IsEscaped(_currentToken))
                    processed = _TryProcessPlugin();

                if (!processed)
                    switch (_currentToken.Code)
                    {
                        case WhiteSpace:
                            break;

                        case Asterisk when (_currentToken.Length == 1):
                            _BeginUnorderedList();
                            _Process();
                            break;

                        case Hash when (_currentToken.Length == 1):
                            _BeginOrderedList();
                            _Process();
                            break;

                        case Equal:
                            _BeginHeading();
                            break;

                        case Dash when (_currentToken.Length >= 4 && (_currentToken.Next == null || (_currentToken.Next.Code == WhiteSpace && _LineFeedCount(_currentToken.Next) > 0))):
                            _HorizontalRule();
                            break;

                        case Pipe:
                            _BeginTable();
                            break;

                        default:
                            _BeginParagraph();
                            break;
                    }
            }

            private bool _TryProcessPreformattedBlock()
            {
                var startToken = _currentToken;
                var endToken = _currentToken;
                while (endToken != null && !(endToken.Code == BraceClose && endToken.Length == _PreformattedCharacterRepeatCount && _IsAloneOnLine(endToken)))
                    endToken = endToken.Next;

                if (endToken != null)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PreformattedBlockStart,
                        Start = startToken.Start,
                        Length = _PreformattedCharacterRepeatCount
                    });

                    var plainTextStartIndex = (_FindLineFeeds(startToken.Next).First().Index + 1);
                    var plainTextEndIndex = (_FindLineFeeds(endToken.Previous).Last().Index);

                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = plainTextStartIndex,
                        Length = (plainTextEndIndex - plainTextStartIndex)
                    });

                    _marks.Add(new ElementMark
                    {
                        Code = PreformattedBlockEnd,
                        Start = endToken.Start,
                        Length = _PreformattedCharacterRepeatCount
                    });

                    _currentToken = endToken;
                    return true;
                }
                else
                    return false;
            }

            private bool _TryProcessPlugin()
            {
                var startToken = _currentToken;
                var endToken = _currentToken;
                while (endToken != null && !(endToken.Code == AngleClose && endToken.Length >= _PluginCharacterRepeatCount && (endToken.Next == null || _FindLineFeeds(endToken.Next).Any())))
                    endToken = endToken.Next;

                if (endToken != null)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PluginStart,
                        Start = startToken.Start,
                        Length = _PluginCharacterRepeatCount
                    });

                    var plainTextStartIndex = (startToken.Start + _PluginCharacterRepeatCount);
                    var plainTextTextEndIndex = (endToken.End - _PluginCharacterRepeatCount);

                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = plainTextStartIndex,
                        Length = (plainTextTextEndIndex - plainTextStartIndex)
                    });

                    _marks.Add(new ElementMark
                    {
                        Code = PluginEnd,
                        Start = (endToken.End - _PluginCharacterRepeatCount),
                        Length = _PluginCharacterRepeatCount
                    });

                    _currentToken = endToken;
                    return true;
                }
                else
                    return false;
            }

            private void _ProcessBlock()
            {
                var currentBlock = _blocks.Peek();
                switch (currentBlock)
                {
                    case Paragraph:
                        _ProcessParagraph();
                        break;

                    case Heading1:
                    case Heading2:
                    case Heading3:
                    case Heading4:
                    case Heading5:
                    case Heading6:
                        _ProcessHeading();
                        break;

                    case UnorderedList:
                    case OrderedList:
                        _ProcessList();
                        break;

                    case Table:
                        _ProcessTable();
                        break;

                    default:
                        throw new InvalidOperationException($"Unknown block: '{currentBlock}'.");
                }
            }

            private void _ProcessHeading()
            {
                if (_currentToken.Code == WhiteSpace && _LineFeedCount(_currentToken) > 0)
                    _EndHeading();
                else
                {
                    if (_marks[_marks.Count - 1].Code == PlainText || _currentToken.Code != WhiteSpace)
                        if (_currentToken.Code != Equal)
                            _AppendPlainText(_currentToken);
                        else if (_currentToken.Length > _MaximumHeadingTokenLength)
                            _AppendPlainText(_currentToken.Start, (_currentToken.Length - _MaximumHeadingTokenLength));
                    if (_currentToken.Next == null)
                        _EndHeading();
                }
            }

            private void _ProcessParagraph()
            {
                if (_currentToken.Code == WhiteSpace && (_LineFeedCount(_currentToken) >= 2 || (_LineFeedCount(_currentToken) >= 1 && _currentToken.Next?.Code == Dash && _currentToken.Next.Length >= 4)))
                    _EndParagraph();
                else
                    _ProcessRichText();

                if (_currentToken.Next == null && _blocks.Count > 0 && _blocks.Peek() == Paragraph)
                    _EndParagraph();
            }

            private void _ProcessRichText()
            {
                var processed = false;
                if (_currentToken.Code == BraceOpen && _currentToken.Length >= (_PreformattedCharacterRepeatCount + (_IsEscaped(_currentToken) ? 1 : 0)))
                    processed = _TryProcessPreformattedText();

                if (!processed)
                    if (_richTextBlocks.Count > 0 && (_richTextBlocks.Peek() == InlineHyperlink || _richTextBlocks.Peek() == EscapedInlineHyperlink))
                        _ProcessInlineHyperlink();
                    else if (_hyperlinkStartMark != null && _hyperlinkTextSeparatorMark == null)
                        _ProcessHyperlinkDestination();
                    else if (_imageStartMark != null)
                        _ProcessImage();
                    else
                        switch (_currentToken.Code)
                        {
                            case Tilde:
                                _ProcessTilde();
                                break;

                            case Asterisk:
                                _ProcessStrong();
                                break;

                            case Slash:
                                _ProcessEmphasis();
                                break;

                            case BackSlash when (_currentToken.Length >= _LineBreakRepeatCount):
                                _ProcessLineBreak();
                                break;

                            case BracketOpen when (_hyperlinkStartMark == null && !_IsEscaped(_currentToken) && _currentToken.Length >= _HyperlinkCharacterRepeatCount):
                            case BracketOpen when (_hyperlinkStartMark == null && _IsEscaped(_currentToken) && _currentToken.Length > _HyperlinkCharacterRepeatCount):
                                _BeginHyperlink();
                                break;

                            case BracketClose when (_currentToken.Length >= _HyperlinkCharacterRepeatCount):
                                _EndHyperlink();
                                break;

                            case BraceOpen when (_imageStartMark == null && !_IsEscaped(_currentToken) && _currentToken.Length == _ImageCharacterRepeatCount):
                            case BraceOpen when (_imageStartMark == null && _IsEscaped(_currentToken) && _currentToken.Length == (_ImageCharacterRepeatCount + 1)):
                                _BeginImage();
                                break;

                            case Text when (_IsProtocol(_currentToken) && _hyperlinkStartMark == null):
                                _BeginInlineHyperlink();
                                break;

                            case Dash:
                            case Pipe:
                            case AngleOpen:
                                _marks.Add(new ElementMark
                                {
                                    Code = PlainText,
                                    Start = _currentToken.Start,
                                    Length = _currentToken.Length
                                });
                                break;

                            default:
                                var startOffset = (_IsEscaped(_currentToken) ? 1 : 0);
                                _marks.Add(new ElementMark
                                {
                                    Code = PlainText,
                                    Start = (_currentToken.Start - startOffset),
                                    Length = (_currentToken.Length + startOffset)
                                });
                                break;
                        }
            }

            private void _ProcessTilde()
            {
                var tildeStart = _currentToken.Start;
                var tildeCount = (_currentToken.Length / 2);
                for (var tildeNumber = 0; tildeNumber < tildeCount; tildeNumber++, tildeStart += 2)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = tildeStart,
                        Length = 1
                    });
            }

            private void _ProcessStrong()
            {
                var isEscaped = _IsEscaped(_currentToken);
                var start = (_currentToken.Start + (isEscaped ? 1 : 0));
                var end = _currentToken.End;
                var length = _currentToken.Length;
                if (isEscaped)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = _currentToken.Start,
                        Length = 1
                    });
                    start++;
                    length--;
                }

                var strongMarkCount = (length / 2);
                for (int strongMarkStart = start, strongMarkNumber = 0; strongMarkNumber < strongMarkCount; strongMarkNumber++, strongMarkStart += _StrongCharacterRepeatCount)
                    _StartOrEndStrong(strongMarkStart);

                if (length % 2 == 1)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (_currentToken.End - 1),
                        Length = 1
                    });
            }

            private void _StartOrEndStrong(int strongMarkStart)
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

            private void _ProcessEmphasis()
            {
                var isEscaped = _IsEscaped(_currentToken);
                var start = (_currentToken.Start + (isEscaped ? 1 : 0));
                var end = _currentToken.End;
                var length = _currentToken.Length;
                if (isEscaped)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = _currentToken.Start,
                        Length = 1
                    });
                    start++;
                    length--;
                }

                var emphasisMarkCount = (length / 2);
                for (int emphasisMarkStart = start, emphasisMarkNumber = 0; emphasisMarkNumber < emphasisMarkCount; emphasisMarkNumber++, emphasisMarkStart += _EmphasisCharacterRepeatCount)
                    _StartOrEndEmphasis(emphasisMarkStart);

                if (length % 2 == 1)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (_currentToken.End - 1),
                        Length = 1
                    });
            }

            private void _StartOrEndEmphasis(int emphasisMarkStart)
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

            private void _BeginHeading()
            {
                var headingMark = _GetHeadingStartMark(_currentToken);
                var block = _GetHeadingBlockFor(headingMark);
                _blocks.Push(block);
                if (_currentToken.Length <= _MaximumHeadingTokenLength)
                    _marks.Add(new ElementMark
                    {
                        Code = headingMark,
                        Start = _currentToken.Start,
                        Length = _currentToken.Length
                    });
                else
                {
                    _marks.Add(new ElementMark
                    {
                        Code = headingMark,
                        Start = _currentToken.Start,
                        Length = _MaximumHeadingTokenLength
                    });
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (_currentToken.Start + _MaximumHeadingTokenLength),
                        Length = (_currentToken.Length - _MaximumHeadingTokenLength)
                    });
                }
            }

            private void _EndHeading()
            {
                var headingBlock = _blocks.Pop();
                if (_currentToken.Code == Equal && _currentToken.Length > _MaximumHeadingTokenLength)
                {
                    var textLength = (_currentToken.Length - _MaximumHeadingTokenLength);
                    _marks.Add(new ElementMark
                    {
                        Code = _GetHeadingEndMarkFor(headingBlock),
                        Start = (_currentToken.Previous.Start + textLength),
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
                        Start = _currentToken.Start,
                        Length = (_currentToken.Code == Equal ? _currentToken.Length : 0)
                    });
                }
            }

            private void _HorizontalRule()
            {
                _marks.Add(new ElementMark
                {
                    Code = HorizontalRule,
                    Start = _currentToken.Start,
                    Length = _currentToken.Length
                });
            }

            private void _BeginUnorderedList()
            {
                _blocks.Push(UnorderedList);
                _marks.Add(new ElementMark
                {
                    Code = UnorderedListStart,
                    Start = _currentToken.Start
                });
            }

            private void _BeginOrderedList()
            {
                _blocks.Push(OrderedList);
                _marks.Add(new ElementMark
                {
                    Code = OrderedListStart,
                    Start = _currentToken.Start
                });
            }

            private void _ProcessList()
            {
                if (_currentToken.Code == WhiteSpace && _LineFeedCount(_currentToken) > 1)
                    while (_blocks.Count > 0)
                        _EndList();
                else
                {
                    var listIndent = _blocks.Count;
                    switch (_currentToken.Code)
                    {
                        case Asterisk when (_currentToken.Length == listIndent && (_currentToken.Previous == null || (_currentToken.Previous.Code == WhiteSpace && _LineFeedCount(_currentToken.Previous) > 0))):
                            if (_blocks.Peek() != UnorderedList)
                            {
                                _EndList();
                                _BeginUnorderedList();
                            }
                            else if (_marks[_marks.Count - 1].Code != UnorderedListStart)
                                _EndListItem();

                            _BeginListItem();
                            break;

                        case Asterisk when (_currentToken.Length > listIndent && (_currentToken.Previous == null || (_currentToken.Previous.Code == WhiteSpace && _LineFeedCount(_currentToken.Previous) > 0))):
                            do
                            {
                                _BeginUnorderedList();
                                _BeginListItem();
                                listIndent = _blocks.Count;
                            } while (_currentToken.Length > listIndent);
                            break;

                        case Asterisk when (_currentToken.Length < listIndent && (_currentToken.Previous == null || (_currentToken.Previous.Code == WhiteSpace && _LineFeedCount(_currentToken.Previous) > 0))):
                            do
                            {
                                _EndList();
                                listIndent = _blocks.Count;
                            } while (_currentToken.Length < listIndent);
                            _EndListItem();
                            _BeginListItem();
                            break;

                        case Hash when (_currentToken.Length == listIndent && (_currentToken.Previous == null || (_currentToken.Previous.Code == WhiteSpace && _LineFeedCount(_currentToken.Previous) > 0))):
                            if (_blocks.Peek() != OrderedList)
                            {
                                _EndList();
                                _BeginOrderedList();
                            }
                            else if (_marks[_marks.Count - 1].Code != OrderedListStart)
                                _EndListItem();

                            _BeginListItem();
                            break;

                        case Hash when (_currentToken.Length > listIndent && (_currentToken.Previous == null || (_currentToken.Previous.Code == WhiteSpace && _LineFeedCount(_currentToken.Previous) > 0))):
                            do
                            {
                                _BeginOrderedList();
                                _BeginListItem();
                                listIndent = _blocks.Count;
                            } while (_currentToken.Length > listIndent);
                            break;

                        case Hash when (_currentToken.Length < listIndent && (_currentToken.Previous == null || (_currentToken.Previous.Code == WhiteSpace && _LineFeedCount(_currentToken.Previous) > 0))):
                            do
                            {
                                _EndList();
                                listIndent = _blocks.Count;
                            } while (_currentToken.Length < listIndent);
                            _EndListItem();
                            _BeginListItem();
                            break;

                        case WhiteSpace when (_marks[_marks.Count - 1].Code == ListItemStart || _marks[_marks.Count - 1].Code == ListItemEnd):
                        case WhiteSpace when (_LineFeedCount(_currentToken) > 0 && (_marks[_marks.Count - 1].Code == ListItemStart || _marks[_marks.Count - 1].Code == ListItemEnd || _currentToken.Next?.Code == Asterisk || _currentToken.Next?.Code == Hash)):
                            break;

                        default:
                            _ProcessRichText();
                            break;
                    }
                }

                if (_currentToken.Next == null)
                    while (_blocks.Count > 0)
                        _EndList();
            }

            private void _EndList()
            {
                _EndListItem();
                var listType = _blocks.Pop();
                _marks.Add(new ElementMark
                {
                    Code = (listType == UnorderedList ? UnorderedListEnd : OrderedListEnd),
                    Start = _currentToken.Start
                });
            }

            private void _BeginListItem()
            {
                _marks.Add(new ElementMark
                {
                    Code = ListItemStart,
                    Start = _currentToken.Start,
                    Length = _currentToken.Length
                });
            }

            private void _EndListItem()
            {
                _ClearRichText();
                _marks.Add(new ElementMark
                {
                    Code = ListItemEnd,
                    Start = _currentToken.Start
                });
            }

            private void _BeginInlineHyperlink()
            {
                if (_IsEscaped(_currentToken))
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = _currentToken.Start,
                        Length = _currentToken.Length
                    });
                    _richTextBlocks.Push(EscapedInlineHyperlink);
                }
                else
                {
                    _marks.Add(new ElementMark
                    {
                        Code = HyperlinkStart,
                        Start = _currentToken.Start
                    });
                    _marks.Add(new ElementMark
                    {
                        Code = HyperlinkDestination,
                        Start = _currentToken.Start,
                        Length = _currentToken.Length
                    });
                    _richTextBlocks.Push(InlineHyperlink);
                }
            }

            private void _ProcessInlineHyperlink()
            {
                switch (_currentToken.Code)
                {
                    case WhiteSpace:
                        if (_richTextBlocks.Peek() == InlineHyperlink)
                            _EndInlineHyperlink();
                        _marks.Add(new ElementMark
                        {
                            Code = PlainText,
                            Start = _currentToken.Start,
                            Length = _currentToken.Length
                        });
                        break;

                    default:
                        _marks[_marks.Count - 1].Length += _currentToken.Length;
                        break;
                }
            }

            private void _EndInlineHyperlink()
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
                    Start = _currentToken.Start
                });
                _richTextBlocks.Pop();
            }

            private void _BeginHyperlink()
            {
                if (_currentToken.Length > _HyperlinkCharacterRepeatCount)
                    _AppendPlainText(_currentToken.Start, _currentToken.Length - _HyperlinkCharacterRepeatCount);

                _hyperlinkStartMark = new ElementMark
                {
                    Code = HyperlinkStart,
                    Start = (_currentToken.End - _HyperlinkCharacterRepeatCount),
                    Length = _HyperlinkCharacterRepeatCount
                };
                _marks.Add(_hyperlinkStartMark);
                _hyperlinkDestinationMark = new ElementMark
                {
                    Code = HyperlinkDestination,
                    Start = _currentToken.End
                };
                _marks.Add(_hyperlinkDestinationMark);
                _richTextBlocks.Push(Hyperlink);
            }

            private void _ProcessHyperlinkDestination()
            {
                switch (_currentToken.Code)
                {
                    case BracketClose when (_currentToken.Length >= _HyperlinkCharacterRepeatCount):
                        _EndHyperlink();
                        break;

                    case Pipe:
                        _hyperlinkTextSeparatorMark = new ElementMark
                        {
                            Code = HyperlinkTextSeparator,
                            Start = _currentToken.Start,
                            Length = _HyperlinkTextSeparatorCharacterRepeatCount
                        };
                        _marks.Add(_hyperlinkTextSeparatorMark);
                        if (_currentToken.Length > _HyperlinkTextSeparatorCharacterRepeatCount)
                            _AppendPlainText(
                                (_currentToken.Start + _HyperlinkTextSeparatorCharacterRepeatCount),
                                (_currentToken.Length - _HyperlinkTextSeparatorCharacterRepeatCount));
                        break;

                    default:
                        _hyperlinkDestinationMark.Length += _currentToken.Length;
                        break;
                }
            }

            private void _EndHyperlink()
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
                    Start = _currentToken.Start,
                    Length = _HyperlinkCharacterRepeatCount
                });
                if (_currentToken.Length > _HyperlinkCharacterRepeatCount)
                    _AppendPlainText(
                        (_currentToken.Start + _HyperlinkCharacterRepeatCount),
                        (_currentToken.Length - _HyperlinkCharacterRepeatCount));

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

            private void _BeginImage()
            {
                if (_currentToken.Length > _ImageCharacterRepeatCount)
                    _AppendPlainText(_currentToken.Start, _currentToken.Length - _ImageCharacterRepeatCount);

                _imageStartMark = new ElementMark
                {
                    Code = ImageStart,
                    Start = (_currentToken.End - _ImageCharacterRepeatCount),
                    Length = _ImageCharacterRepeatCount
                };
                _marks.Add(_imageStartMark);
                _imageSourceMark = new ElementMark
                {
                    Code = ImageSource,
                    Start = _currentToken.End
                };
                _marks.Add(_imageSourceMark);
                _richTextBlocks.Push(Image);
            }

            private void _ProcessImage()
            {
                if (_imageTextSeparatorMark == null)
                    _ProcessImageSource();
                else
                    _ProcessImageAternativeText();
            }

            private void _ProcessImageSource()
            {
                switch (_currentToken.Code)
                {
                    case BraceClose when (_currentToken.Length >= _ImageCharacterRepeatCount):
                        _EndImage();
                        break;

                    case Pipe:
                        _imageTextSeparatorMark = new ElementMark
                        {
                            Code = HyperlinkTextSeparator,
                            Start = _currentToken.Start,
                            Length = _ImageTextSeparatorCharacterRepeatCount
                        };
                        _marks.Add(_imageTextSeparatorMark);
                        if (_currentToken.Length > _ImageTextSeparatorCharacterRepeatCount)
                            _AppendPlainText(
                                (_currentToken.Start + _ImageTextSeparatorCharacterRepeatCount),
                                (_currentToken.Length - _ImageTextSeparatorCharacterRepeatCount));
                        break;

                    default:
                        _imageSourceMark.Length += _currentToken.Length;
                        break;
                }
            }

            private void _ProcessImageAternativeText()
            {
                switch (_currentToken.Code)
                {
                    case BraceClose when (_currentToken.Length >= _ImageCharacterRepeatCount):
                        _EndImage();
                        break;

                    default:
                        var startOffset = (_IsEscaped(_currentToken) ? 1 : 0);
                        _marks.Add(new ElementMark
                        {
                            Code = PlainText,
                            Start = (_currentToken.Start - startOffset),
                            Length = (_currentToken.Length + startOffset)
                        });
                        break;
                }
            }

            private void _EndImage()
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
                    Start = _currentToken.Start,
                    Length = _ImageCharacterRepeatCount
                });
                if (_currentToken.Length > _ImageCharacterRepeatCount)
                    _AppendPlainText(
                        (_currentToken.Start + _ImageCharacterRepeatCount),
                        (_currentToken.Length - _ImageCharacterRepeatCount));

                _imageStartMark = null;
                _imageSourceMark = null;
                _imageTextSeparatorMark = null;

                _richTextBlocks.Pop();
            }

            private void _ProcessLineBreak()
            {
                var isEscaped = _IsEscaped(_currentToken);
                var start = (_currentToken.Start + (isEscaped ? 1 : 0));
                var end = _currentToken.End;
                var length = _currentToken.Length;
                if (isEscaped)
                {
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = _currentToken.Start,
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
                        Start = (_currentToken.End - 1),
                        Length = 1
                    });
            }

            private bool _TryProcessPreformattedText()
            {
                var startToken = _currentToken;
                var endToken = _currentToken;
                while (endToken != null && !(endToken.Code == BraceClose && endToken.Length >= _PreformattedCharacterRepeatCount))
                    if (endToken.Code == WhiteSpace && _FindLineFeeds(endToken).Skip(1).Any())
                        endToken = null;
                    else
                        endToken = endToken.Next;

                if (endToken != null)
                {
                    var startOffset = 0;
                    if (_IsEscaped(_currentToken))
                    {
                        startOffset = 1;
                        _AppendPlainText(startToken.Start, startOffset);
                    }

                    var plainTextStartIndex = (startToken.Start + _PreformattedCharacterRepeatCount + startOffset);
                    var plainTextEndIndex = (endToken.End - _PreformattedCharacterRepeatCount);

                    _marks.Add(new ElementMark
                    {
                        Code = PreformattedTextStart,
                        Start = (startToken.Start + startOffset),
                        Length = _PreformattedCharacterRepeatCount
                    });
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = plainTextStartIndex,
                        Length = (plainTextEndIndex - plainTextStartIndex)
                    });
                    _marks.Add(new ElementMark
                    {
                        Code = PreformattedTextEnd,
                        Start = plainTextEndIndex,
                        Length = _PreformattedCharacterRepeatCount
                    });

                    _currentToken = endToken;
                    return true;
                }
                else
                    return false;
            }

            private void _BeginParagraph()
            {
                _blocks.Push(Paragraph);
                _marks.Add(new ElementMark
                {
                    Code = ParagraphStart,
                    Start = _currentToken.Start
                });
                _Process();
            }

            private void _EndParagraph()
            {
                _ClearRichText();
                _marks.Add(new ElementMark
                {
                    Code = ParagraphEnd,
                    Start = _currentToken.End
                });
                _blocks.Pop();
            }

            private void _BeginTable()
            {
                _blocks.Push(Table);
                _tableStartMark = new ElementMark
                {
                    Code = TableStart,
                    Start = _currentToken.Start
                };
                _marks.Add(_tableStartMark);
                _Process();
            }

            private void _ProcessTable()
            {
                switch (_currentToken.Code)
                {
                    case Pipe when (_tableRowStartMark != null && (_currentToken.Next?.Code == WhiteSpace && _LineFeedCount(_currentToken.Next) > 0)):
                        break;

                    case Pipe when (_tableRowStartMark == null):
                        _BeginTableRow();
                        break;

                    case Pipe when (_tableCellStartMark != null && !_IsEscaped(_currentToken)):
                        _EndTableCell();
                        break;

                    case Equal when (_tableCellStartMark == null):
                        _BeginTableHeaderCell();
                        break;

                    case WhiteSpace when (_LineFeedCount(_currentToken) > 1):
                    case WhiteSpace when (_LineFeedCount(_currentToken) == 1 && _currentToken.Next?.Code != Pipe):
                        _EndTable();
                        break;

                    case WhiteSpace when (_LineFeedCount(_currentToken) > 0):
                        _EndTableRow();
                        break;

                    case WhiteSpace when (_tableRowStartMark == null || _tableCellStartMark == null || _marks[_marks.Count - 1] == _tableCellStartMark):
                        break;

                    default:
                        if (_tableCellStartMark == null)
                            _BeginTableCell();
                        _ProcessRichText();
                        break;
                }

                if (_currentToken.Next == null)
                    _EndTable();
            }

            private void _EndTable()
            {
                if (_tableRowStartMark != null)
                    _EndTableRow();
                _marks.Add(new ElementMark
                {
                    Code = TableEnd,
                    Start = _currentToken.Start
                });
                _tableStartMark = null;
                _blocks.Pop();
            }

            private void _BeginTableRow()
            {
                _tableRowStartMark = new ElementMark
                {
                    Code = TableRowStart,
                    Start = _currentToken.Start,
                    Length = _currentToken.Length
                };
                _marks.Add(_tableRowStartMark);
            }

            private void _EndTableRow()
            {
                if (_tableCellStartMark != null)
                    _EndTableCell();

                _marks.Add(new ElementMark
                {
                    Code = TableRowEnd,
                    Start = _currentToken.Start
                });
                _tableRowStartMark = null;
            }

            private void _BeginTableHeaderCell()
            {
                _tableCellStartMark = new ElementMark
                {
                    Code = TableHeaderCellStart,
                    Start = _currentToken.Start,
                    Length = _currentToken.Length
                };
                _marks.Add(_tableCellStartMark);
            }

            private void _BeginTableCell()
            {
                _tableCellStartMark = new ElementMark
                {
                    Code = TableCellStart,
                    Start = _currentToken.Start,
                    Length = _currentToken.Length
                };
                _marks.Add(_tableCellStartMark);
            }

            private void _EndTableCell()
            {
                _ClearRichText();

                var plainTextMark = _marks[_marks.Count - 1];
                if (plainTextMark.Code == PlainText)
                {
                    _TrimWhiteSpaceEnd(plainTextMark);
                    if (plainTextMark.Length == 0)
                        _marks.RemoveAt(_marks.Count - 1);
                }

                _marks.Add(new ElementMark
                {
                    Code = (_tableCellStartMark.Code == TableHeaderCellStart ? TableHeaderCellEnd : TableCellEnd),
                    Start = _currentToken.Start
                });
                _tableCellStartMark = null;
            }

            private void _ClearRichText()
            {
                if (_richTextBlocks.Count > 0 && _richTextBlocks.Peek() == InlineHyperlink)
                    _EndInlineHyperlink();

                _ClearStrong();
                _ClearEmphasis();
                _ClearHyperlink();
                _ClearImage();

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

            private void _TrimWhiteSpaceEnd(ElementMark mark)
            {
                while (mark.Length > 0 && char.IsWhiteSpace(_text, (mark.Length + mark.Start - 1)))
                    mark.Length--;
            }

            private void _AppendPlainText(IToken<CreoleTokenCode> token)
            {
                _AppendPlainText(_currentToken.Start, _currentToken.Length);
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

            private static ElementMarkCode _GetHeadingStartMark(IToken<CreoleTokenCode> currentToken)
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

            private static bool _IsEscaped(IToken<CreoleTokenCode> token)
                => (token.Previous != null && token.Previous.Code == Tilde && (token.Previous.Length % 2) == 1);

            private bool _IsProtocol(IToken<CreoleTokenCode> token)
            {
                var index = _currentToken.Start;
                var end = (_currentToken.End - 1);

                if (_text[end] != _ProtocolSchemeSeparator)
                    return false;

                var protocolLength = (_currentToken.Length - 1);
                var candidateProtocols = _inlineHyperlinkProtocols.Where(protocol => protocol.Length == protocolLength).ToList();
                while (candidateProtocols.Count > 0 && index < end)
                {
                    var candidateProtocolIndex = 0;
                    while (candidateProtocolIndex < candidateProtocols.Count)
                    {
                        var candidateProtocol = candidateProtocols[candidateProtocolIndex];
                        if (char.ToLowerInvariant(_text[index]) == candidateProtocol[(index - _currentToken.Start)])
                            candidateProtocolIndex++;
                        else
                            candidateProtocols.RemoveAt(candidateProtocolIndex);
                    }
                    index++;
                }

                return (candidateProtocols.Count > 0);
            }

            private int _LineFeedCount(IToken<CreoleTokenCode> token)
            {
                var count = 0;
                if (token.Code == WhiteSpace)
                    for (var index = token.Start; index < token.End; index++)
                        if (_text[index] == _LineFeed)
                            count++;
                return count;
            }

            private bool _EndsOnNewLine(IToken<CreoleTokenCode> token)
                => (token.Code == WhiteSpace && _text[token.End - 1] == _LineFeed);

            private bool _IsAloneOnLine(IToken<CreoleTokenCode> token)
                => ((token.Previous == null || _EndsOnNewLine(token.Previous)) && (token.Next == null || _LineFeedCount(token.Next) > 0));

            private IEnumerable<CharacterMatch> _FindLineFeeds(IToken<CreoleTokenCode> token)
            {
                if (token.Code == WhiteSpace)
                    for (var index = token.Start; index < token.End; index++)
                        if (_text[index] == _LineFeed)
                            yield return new CharacterMatch(_LineFeed, index);
            }

            private struct CharacterMatch
            {
                internal CharacterMatch(char character, int index)
                {
                    Character = character;
                    Index = index;
                }

                internal char Character { get; }

                internal int Index { get; }
            }
        }
    }
}