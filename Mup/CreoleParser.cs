using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Mup.CreoleToken;
using static Mup.ElementBlock;
using static Mup.ElementMarkCode;
using static Mup.RichTextBlock;

namespace Mup
{
    public class CreoleParser : IMarkupParser
    {
        public IParseResult Parse(string text)
        {
            var scanner = _GetScanner();
            var scanResult = scanner.Scan(text);
            var parseResult = _Parse(scanResult);
            return parseResult;
        }

        public Task<IParseResult> ParseAsync(string text)
            => ParseAsync(text, CancellationToken.None);

        public async Task<IParseResult> ParseAsync(string text, CancellationToken cancellationToken)
        {
            var scanner = _GetScanner();
            var scanResult = await scanner.ScanAsync(text, cancellationToken);
            var parseResult = _Parse(scanResult);
            return parseResult;
        }

        public Task<IParseResult> ParseAsync(TextReader reader)
            => ParseAsync(reader);

        public async Task<IParseResult> ParseAsync(TextReader reader, CancellationToken cancellationToken)
        {
            var scanner = _GetScanner();
            var scanResult = await scanner.ScanAsync(reader, cancellationToken);
            var parseResult = _Parse(scanResult);
            return parseResult;
        }

        public Task<IParseResult> ParseAsync(TextReader reader, int bufferSize)
            => ParseAsync(reader, bufferSize, CancellationToken.None);

        public async Task<IParseResult> ParseAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken)
        {
            var scanner = _GetScanner();
            var scanResult = await scanner.ScanAsync(reader, bufferSize, cancellationToken);
            var parseResult = _Parse(scanResult);
            return parseResult;
        }

        private Scanner<CreoleToken> _GetScanner()
            => new CreoleScanner();

        private IParseResult _Parse(ScanResult<CreoleToken> scanResult)
        {
            var parser = new CreoleMarkupParser(scanResult);
            var parseResult = parser.Parse();
            return parseResult;
        }

        private class CreoleMarkupParser
        {
            private const char _LineFeed = '\n';

            private const int _MaximumHeadingTokenLength = 6;
            private const int _StrongCharacterRepeatCount = 2;
            private const int _EmphasisCharacterRepeatCount = 2;
            private const int _HyperlinkCharacterRepeatCount = 2;
            private const int _ImageCharacterRepeatCount = 2;

            private const char _ProtocolSeparator = ':';
            private const string _FileScheme = "file";
            private const string _FtpScheme = "ftp";
            private const string _GopherScheme = "gopher";
            private const string _HttpScheme = "http";
            private const string _HttpsScheme = "https";
            private const string _MailToScheme = "mailto";
            private const string _NntpScheme = "nntp";
            private static readonly IEnumerable<string> _protocols = new[]
            {
                _FileScheme,
                _FtpScheme,
                _GopherScheme,
                _HttpScheme,
                _HttpsScheme,
                _MailToScheme,
                _NntpScheme
            };

            private readonly string _text;
            private readonly IEnumerable<Token<CreoleToken>> _tokens;

            private ElementMark _strongStartMark = null;
            private ElementMark _emphasisStartMark = null;
            private ElementMark _hyperlinkStartMark = null;
            private ElementMark _imageStartMark = null;
            private readonly IList<ElementMark> _marks = new List<ElementMark>();
            private readonly Stack<ElementBlock> _blocks = new Stack<ElementBlock>();
            private readonly Stack<RichTextBlock> _richTextBlocks = new Stack<RichTextBlock>();

            internal CreoleMarkupParser(ScanResult<CreoleToken> scanResult)
            {
                _text = scanResult.Text;
                _tokens = scanResult.Tokens;
            }

            internal ParseResult Parse()
            {
                Token<CreoleToken> previous = null, current = null, next = null;

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
                return new ParseResult(_text, marks);
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

            private void _Process(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
            {
                if (_blocks.Count == 0)
                    _BeginBlock(previousToken, currentToken, nextToken);
                else
                    _ProcessBlock(previousToken, currentToken, nextToken);
            }

            private void _BeginBlock(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
            {
                switch (currentToken.Code)
                {
                    case Asterisk when (currentToken.Length == 1):
                        _blocks.Push(BulletList);
                        _marks.Add(new ElementMark
                        {
                            Code = BulletListStart,
                            Start = currentToken.Start
                        });
                        _marks.Add(new ElementMark
                        {
                            Code = BulletListItemStart,
                            Start = currentToken.Start,
                            Length = currentToken.Length
                        });
                        break;

                    case Hash when (currentToken.Length == 1):
                        _blocks.Push(OrderedList);
                        _marks.Add(new ElementMark
                        {
                            Code = OrderedListStart,
                            Start = currentToken.Start
                        });
                        _marks.Add(new ElementMark
                        {
                            Code = OrderedListItemStart,
                            Start = currentToken.Start,
                            Length = currentToken.Length
                        });
                        break;

                    case AngleOpen when (currentToken.Length == 2):
                        _blocks.Push(Plugin);
                        _marks.Add(new ElementMark
                        {
                            Code = PluginStart,
                            Start = currentToken.Start,
                            Length = currentToken.Length
                        });
                        break;

                    case Equal:
                        _BeginHeading(previousToken, currentToken, nextToken);
                        break;

                    case Dash when (currentToken.Length >= 4 && (nextToken == null || (nextToken.Code == NewLine && _LineFeedCount(nextToken) > 0))):
                        _marks.Add(new ElementMark
                        {
                            Code = HorizontalLine,
                            Start = currentToken.Start,
                            Length = currentToken.Length
                        });
                        break;

                    case Pipe:
                        _blocks.Push(Table);
                        _marks.Add(new ElementMark
                        {
                            Code = TableStart,
                            Start = currentToken.Start,
                            Length = currentToken.Length
                        });
                        _marks.Add(new ElementMark
                        {
                            Code = TableRowStart,
                            Start = currentToken.End
                        });
                        break;

                    default:
                        _BeginParagraph(previousToken, currentToken, nextToken);
                        break;
                }
            }

            private void _ProcessBlock(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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

                    case BulletList:
                        break;

                    case OrderedList:
                        break;

                    case Table:
                        break;

                    case NoWiki:
                        break;

                    case Plugin:
                        break;

                    default:
                        break;
                }
            }

            private void _ProcessParagraph(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
            {
                switch (currentToken.Code)
                {
                    case NewLine when (_LineFeedCount(currentToken) >= 2):
                        _EndParagraph(previousToken, currentToken, nextToken);
                        break;

                    case Tilde:
                        _ProcessTilde(previousToken, currentToken, nextToken);
                        break;

                    case Asterisk:
                        _ProcessStrong(previousToken, currentToken, nextToken);
                        break;

                    case Slash:
                        _ProcessEmphasis(previousToken, currentToken, nextToken);
                        break;

                    case CreoleToken.BackSlash:
                        break;
                    case CreoleToken.BracketOpen:
                        break;
                    case CreoleToken.BracketClose:
                        break;
                    case CreoleToken.BraceOpen:
                        break;
                    case CreoleToken.BraceClose:
                        break;
                    case CreoleToken.AngleOpen:
                        break;
                    case CreoleToken.AngleClose:
                        break;
                    case CreoleToken.Equal:
                        break;
                    case CreoleToken.Dash:
                        break;
                    case CreoleToken.Hash:
                        break;
                    case CreoleToken.Pipe:
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

                if (nextToken == null && _blocks.Count > 0 && _blocks.Peek() == Paragraph)
                    _EndParagraph(previousToken, currentToken, nextToken);
            }

            private void _ProcessTilde(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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

            private void _ProcessStrong(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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

            private void _StartOrEndStrong(int strongMarkStart, Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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

            private void _ProcessEmphasis(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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
                for (int emphasisMarkStart = start, emphasisMarkNumber = 0; emphasisMarkNumber < emphasisMarkCount; emphasisMarkNumber++, emphasisMarkStart += _StrongCharacterRepeatCount)
                    _StartOrEndEmphasis(emphasisMarkStart, previousToken, currentToken, nextToken);

                if (length % 2 == 1)
                    _marks.Add(new ElementMark
                    {
                        Code = PlainText,
                        Start = (currentToken.End - 1),
                        Length = 1
                    });
            }

            private void _StartOrEndEmphasis(int emphasisMarkStart, Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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

            private void _BeginParagraph(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
            {
                _blocks.Push(Paragraph);
                _marks.Add(new ElementMark
                {
                    Code = ParagraphStart,
                    Start = currentToken.Start
                });
                _Process(previousToken, currentToken, nextToken);
            }

            private void _EndParagraph(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
            {
                _marks.Add(new ElementMark
                {
                    Code = ParagraphEnd,
                    Start = currentToken.End
                });
                _ClearRichText(previousToken, currentToken, nextToken);
                _blocks.Pop();
            }

            private void _ClearRichText(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
            {
                if (_strongStartMark != null)
                {
                    _strongStartMark.Code = PlainText;
                    _strongStartMark = null;
                }
                if (_emphasisStartMark != null)
                {
                    _emphasisStartMark.Code = PlainText;
                    _emphasisStartMark = null;
                }

                if (_richTextBlocks.Count > 0 && (_richTextBlocks.Peek() == InlineHyperlink || _richTextBlocks.Peek() == EscapedInlineHyperlink))
                {
                    _marks.Add(new ElementMark
                    {
                        Code = HyperlinkEnd,
                        Start = currentToken.Start
                    });
                }

                _richTextBlocks.Clear();
            }

            private void _ProcessHeading(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
            {
                if (currentToken.Code == NewLine && _LineFeedCount(currentToken) > 0)
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

            private void _BeginHeading(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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

            private void _EndHeading(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
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

            private void _TrimWhiteSpaceEnd(ElementMark mark)
            {
                while (mark.Length > 0 && char.IsWhiteSpace(_text, (mark.Length + mark.Start - 1)))
                    mark.Length--;
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

            private void _AppendPlainText(Token<CreoleToken> token)
            {
                _AppendPlainText(token.Start, token.Length);
            }

            private static ElementMarkCode _GetHeadingStartMark(Token<CreoleToken> currentToken)
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

            private int _LineFeedCount(Token<CreoleToken> token)
            {
                var count = 0;

                var end = (token.Start + token.Length);
                for (var index = token.Start; index < end; index++)
                    if (_text[index] == '\n')
                        count++;

                return count;
            }

            private static bool _IsEscaped(Token<CreoleToken> previousToken, Token<CreoleToken> currentToken, Token<CreoleToken> nextToken)
                => (previousToken != null && previousToken.Code == Tilde && (previousToken.Length % 2 == 1));

            private int _TryMatchProtocol(Token<CreoleToken> token)
            {
                Match match = null;
                var potentialMatches = _protocols.Select(protocol => new Match(protocol)).ToList();
                var index = token.Start;
                var end = token.End;
                while (index < end && match == null)
                {
                    var character = _text[index];
                    foreach (var potentialMatch in potentialMatches)
                        if (potentialMatch.ValueIndex < potentialMatch.Value.Length && potentialMatch.Value[potentialMatch.ValueIndex] == character)
                            potentialMatch.ValueIndex++;
                        else
                        {
                            potentialMatch.StartIndex = index;
                            potentialMatch.ValueIndex = 0;
                        }

                    if (character == _ProtocolSeparator)
                        match = potentialMatches.SingleOrDefault(potentialMatch => (potentialMatch.ValueIndex == potentialMatch.Value.Length));
                }

                return (match?.StartIndex ?? -1);
            }

            private class Match
            {
                internal Match(string value)
                    => Value = value;

                internal string Value { get; }

                internal int ValueIndex { get; set; }

                internal int StartIndex { get; set; }
            }
        }
    }
}