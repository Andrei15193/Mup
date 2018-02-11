﻿using System;
using System.Collections.Generic;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;
using static Mup.Creole.CreoleTokenHelper;
using static Mup.RichTextElementType;

namespace Mup.Creole.ElementParsers
{
    internal abstract class CreoleRichTextBlockElementParser : CreoleElementParser
    {
        internal CreoleRichTextBlockElementParser(CreoleParserContext context)
            : base(context)
        {
        }

        protected IEnumerable<CreoleElement> CreateRichTextElementsFrom(CreoleToken start, CreoleToken end)
        {
            var hyperlinkElementInfos = new List<ElementInfo>(_GetHyperlinkElementInfos(start, end));
            var codeElementInfos = _GetCodeElementInfos(start, end);
            var imageElementInfos = _GetImageElementInfos(start, end);
            var pluginElementInfos = _GetPluginElementInfos(start, end);

            var rootElementInfos = new LinkedList<ElementInfo>(_LeftPrecedenceMerge(hyperlinkElementInfos, codeElementInfos, imageElementInfos, pluginElementInfos));

            _FillGaps(_GetInlineHyperlinkElementInfos, start, end, rootElementInfos);
            _FillGaps(_GetLineBreakElementInfos, start, end, rootElementInfos);

            var fontStyleElementInfos = _GetFontStyleElementInfos(start, end, rootElementInfos);
            _MergeToBase(rootElementInfos, fontStyleElementInfos);

            _ProcessHyperlinkElementInfos(hyperlinkElementInfos);

            return _CreateElements(start, rootElementInfos, end);
        }

        private void _ProcessHyperlinkElementInfos(IEnumerable<ElementInfo> hyperlinkElementInfos)
        {
            foreach (var hyperlinkElementInfo in hyperlinkElementInfos)
                _ProcessHyperlinkElementInfo(hyperlinkElementInfo);
        }

        private void _ProcessHyperlinkElementInfo(ElementInfo hyperlinkElementInfo)
        {
            if (hyperlinkElementInfo.ContentStart != null && hyperlinkElementInfo.ContentEnd != null)
            {
                _FillGaps(_GetLineBreakElementInfos, hyperlinkElementInfo.ContentStart, hyperlinkElementInfo.ContentEnd, hyperlinkElementInfo.Children);

                var canContainEmphasis = !_HasParentType(hyperlinkElementInfo, Emphasis);
                var canContainStrong = !_HasParentType(hyperlinkElementInfo, Strong);

                IEnumerable<ElementInfo> fontStyleElementInfos = null;
                if (canContainEmphasis && canContainStrong)
                    fontStyleElementInfos = _GetFontStyleElementInfos(hyperlinkElementInfo.ContentStart, hyperlinkElementInfo.ContentEnd, hyperlinkElementInfo.Children);
                else
                {
                    var gapRanges = _GetGapRanges(hyperlinkElementInfo.ContentStart, hyperlinkElementInfo.ContentEnd, hyperlinkElementInfo.Children);
                    if (canContainEmphasis)
                        fontStyleElementInfos = _GetCrossRangeElementInfos(_CreateEmphasisElementInfo, Slash, gapRanges);
                    else if (canContainStrong)
                        fontStyleElementInfos = _GetCrossRangeElementInfos(_CreateStrongElementInfo, Asterisk, gapRanges);
                }

                if (fontStyleElementInfos != null)
                    _MergeToBase(hyperlinkElementInfo.Children, fontStyleElementInfos);
            }
        }

        private void _FillGaps(ElementInfosFactory elementInfosFactory, CreoleToken start, CreoleToken end, LinkedList<ElementInfo> elementInfos)
        {
            var gapStart = start;

            var elementInfoNode = elementInfos.First;
            while (elementInfoNode != null)
            {
                var elementInfo = elementInfoNode.Value;
                foreach (var generateElementInfo in elementInfosFactory(gapStart, elementInfo.Start))
                    elementInfos.AddBefore(elementInfoNode, generateElementInfo);

                gapStart = elementInfo.End.Next;
                elementInfoNode = elementInfoNode.Next;
            }

            foreach (var generateElementInfo in elementInfosFactory(gapStart, end.Next))
                elementInfos.AddLast(generateElementInfo);
        }

        private static void _MergeToBase(LinkedList<ElementInfo> baseElementInfos, IEnumerable<ElementInfo> elementInfos)
        {
            _MergeElements(baseElementInfos, elementInfos);
            _CreateHierarchy(baseElementInfos);
        }

        private static void _MergeElements(LinkedList<ElementInfo> baseElementInfos, IEnumerable<ElementInfo> elementInfos)
        {
            var baseElementInfo = baseElementInfos.First;

            if (baseElementInfo == null)
                foreach (var elementInfo in elementInfos)
                    baseElementInfos.AddLast(elementInfo);
            else
                foreach (var elementInfo in elementInfos)
                    if (baseElementInfo == null)
                        baseElementInfos.AddLast(elementInfo);
                    else
                    {
                        while (baseElementInfo != null && baseElementInfo.Value.Start.StartIndex < elementInfo.Start.StartIndex)
                            baseElementInfo = baseElementInfo.Next;
                        if (baseElementInfo != null)
                            baseElementInfos.AddBefore(baseElementInfo, elementInfo);
                        else
                            baseElementInfos.AddLast(elementInfo);
                    }
        }

        private static void _CreateHierarchy(LinkedList<ElementInfo> elementInfos)
        {
            var parentElementInfoNode = elementInfos.Last?.Previous;

            while (parentElementInfoNode != null)
            {
                var parentElementInfo = parentElementInfoNode.Value;
                while (parentElementInfoNode.Next != null && parentElementInfo.Contains(parentElementInfoNode.Next.Value))
                {
                    var childElementInfoNode = parentElementInfoNode.Next;
                    var childElementInfo = childElementInfoNode.Value;
                    parentElementInfo.Children.AddFirst(childElementInfo);
                    elementInfos.Remove(childElementInfoNode);
                }
                parentElementInfoNode = parentElementInfoNode.Previous;
            }
        }

        private IEnumerable<CreoleTokenRange> _GetGapRanges(CreoleToken start, CreoleToken end, IEnumerable<ElementInfo> elementInfos)
        {
            var gapStart = start;
            foreach (var elementInfo in elementInfos)
            {
                var token = gapStart;
                if (token != elementInfo.Start)
                    yield return new CreoleTokenRange(gapStart, elementInfo.Start);

                gapStart = elementInfo.End.Next;
            }
            if (gapStart != end.Next)
                yield return new CreoleTokenRange(gapStart, end.Next);
        }

        private IEnumerable<ElementInfo> _GetCrossRangeElementInfos(ElementInfoFactory elementInfoFactory, CreoleTokenCode tokenCode, IEnumerable<CreoleTokenRange> creoleTokenRanges)
        {
            CreoleToken elementInfoStart = null;
            foreach (var range in creoleTokenRanges)
            {
                var token = range.Start;
                while (token != range.End)
                {
                    if (elementInfoStart != null)
                        if (token.Code == tokenCode && token.Previous.Code == tokenCode)
                        {
                            yield return elementInfoFactory(elementInfoStart, token);
                            elementInfoStart = null;
                            token = token.Next;
                        }
                        else
                            token = token.Next;
                    else if (token.Next != range.End && token.Code == tokenCode && token.Next?.Code == tokenCode && token.Next.Next?.Code != tokenCode)
                    {
                        elementInfoStart = token;
                        token = token.Next.Next;
                    }
                    else
                        token = token.Next;
                }
            }
        }

        private static IEnumerable<ElementInfo> _GetHyperlinkElementInfos(CreoleToken start, CreoleToken end)
        {
            ElementInfo hyperlinkElementInfo = null;
            for (var token = start; token != end.Next; token = token.Next)
                if (hyperlinkElementInfo != null)
                {
                    if (hyperlinkElementInfo.ContentStart == null && token.Code == Pipe)
                        hyperlinkElementInfo.ContentStart = token.Next;

                    if (token.Previous.Code == BracketClose && token.Code == BracketClose)
                    {
                        if (hyperlinkElementInfo.ContentStart != null)
                            hyperlinkElementInfo.ContentEnd = token.Previous.Previous;
                        hyperlinkElementInfo.End = token;

                        yield return hyperlinkElementInfo;
                        hyperlinkElementInfo = null;
                    }
                }
                else if (token.Code == BracketOpen && token.Next?.Code == BracketOpen && token.Next?.Next?.Code != BracketOpen)
                    hyperlinkElementInfo = new ElementInfo(Hyperlink)
                    {
                        Start = token
                    };
        }

        private static IEnumerable<ElementInfo> _GetCodeElementInfos(CreoleToken start, CreoleToken end)
        {
            var previousStart = start;
            ElementInfo codeElementInfo = null;
            for (var token = start; token != end.Next; token = token.Next)
                if (codeElementInfo != null)
                {
                    if (token.Previous?.Previous?.Code == BraceClose && token.Previous?.Code == BraceClose && token.Code == BraceClose && token.Next?.Code != BraceClose)
                    {
                        codeElementInfo.ContentEnd = token.Previous.Previous.Previous;
                        codeElementInfo.End = token;

                        yield return codeElementInfo;
                        codeElementInfo = null;
                    }
                }
                else if (token.Previous?.Code != BraceOpen && token.Code == BraceOpen && token.Next?.Code == BraceOpen && token.Next?.Next?.Code == BraceOpen)
                    codeElementInfo = new ElementInfo(Code)
                    {
                        Start = token,
                        ContentStart = token.Next.Next.Next
                    };
        }

        private static IEnumerable<ElementInfo> _GetImageElementInfos(CreoleToken start, CreoleToken end)
        {
            ElementInfo imageElementInfo = null;
            for (var token = start; token != end.Next; token = token.Next)
                if (imageElementInfo != null)
                {
                    if (imageElementInfo.ContentStart == null && token.Code == Pipe)
                        imageElementInfo.ContentStart = token.Next;

                    if (token.Previous.Code == BraceClose && token.Code == BraceClose)
                    {
                        if (imageElementInfo.ContentStart != null)
                            imageElementInfo.ContentEnd = token.Previous.Previous;
                        imageElementInfo.End = token;

                        yield return imageElementInfo;
                        imageElementInfo = null;
                    }
                }
                else if (token.Code == BraceOpen && token.Next?.Code == BraceOpen && token.Next?.Next?.Code != BraceOpen && token.Previous?.Code != BraceOpen)
                    imageElementInfo = new ElementInfo(Image)
                    {
                        Start = token
                    };
        }

        private static IEnumerable<ElementInfo> _GetPluginElementInfos(CreoleToken start, CreoleToken end)
        {
            ElementInfo pluginElementInfo = null;
            for (var token = start; token != end.Next; token = token.Next)
                if (pluginElementInfo != null)
                {
                    if (token.Previous.Code == AngleClose && token.Code == AngleClose)
                    {
                        pluginElementInfo.End = token;
                        pluginElementInfo.ContentEnd = token.Previous.Previous;

                        yield return pluginElementInfo;
                        pluginElementInfo = null;
                    }
                }
                else if (token.Code == AngleOpen && token.Next?.Code == AngleOpen && token.Next?.Next?.Code != AngleOpen)
                    pluginElementInfo = new ElementInfo(Plugin)
                    {
                        Start = token,
                        ContentStart = token.Next.Next
                    };
        }

        private static IEnumerable<ElementInfo> _GetLineBreakElementInfos(CreoleToken start, CreoleToken end)
        {
            var token = start;
            while (token != end)
                if (token.Code == BackSlash && token.Next?.Code == BackSlash)
                {
                    var lineBreakElementInfo = new ElementInfo(LineBreak)
                    {
                        Start = token,
                        End = token.Next
                    };
                    yield return lineBreakElementInfo;
                    token = token.Next.Next;
                }
                else
                    token = token.Next;
        }

        private IEnumerable<ElementInfo> _GetFontStyleElementInfos(CreoleToken start, CreoleToken end, LinkedList<ElementInfo> elementInfos)
        {
            var gapRanges = _GetGapRanges(start, end, elementInfos);

            var emphasisElementInfos = _GetCrossRangeElementInfos(_CreateEmphasisElementInfo, Slash, gapRanges);
            var strongElementInfos = _GetCrossRangeElementInfos(_CreateStrongElementInfo, Asterisk, gapRanges);

            return new List<ElementInfo>(_OmitOverlapMerge(emphasisElementInfos, strongElementInfos));
        }

        private ElementInfo _CreateEmphasisElementInfo(CreoleToken start, CreoleToken end)
            => new ElementInfo(Emphasis)
            {
                Start = start,
                ContentStart = start.Next.Next,
                End = end,
                ContentEnd = end.Previous.Previous
            };

        private ElementInfo _CreateStrongElementInfo(CreoleToken start, CreoleToken end)
            => new ElementInfo(Strong)
            {
                Start = start,
                ContentStart = start.Next.Next,
                End = end,
                ContentEnd = end.Previous.Previous
            };

        private bool _IsInlineHyperlinkProtocol(CreoleToken token)
        {
            using (var inlineHyperlinkProtocol = Context.InlineHyperlinkProtocols.GetEnumerator())
            {
                var foundMatch = false;

                while (!foundMatch && inlineHyperlinkProtocol.MoveNext())
                {
                    var protocol = inlineHyperlinkProtocol.Current;
                    if (protocol.Length == token.Length)
                    {
                        var protocolIndex = 0;
                        while (protocolIndex < protocol.Length && char.ToLowerInvariant(Context.Text[token.StartIndex + protocolIndex]) == char.ToLowerInvariant(protocol[protocolIndex]))
                            protocolIndex++;
                        foundMatch = (protocolIndex == protocol.Length);
                    }
                }

                return foundMatch;
            }
        }

        private IEnumerable<ElementInfo> _GetInlineHyperlinkElementInfos(CreoleToken start, CreoleToken end)
        {
            var token = start;
            while (token != end)
            {
                var inlineHyperlinkElementInfo = _TryGetInlineHyperlinkElementInfoAt(token, end);
                if (inlineHyperlinkElementInfo != null)
                {
                    token = inlineHyperlinkElementInfo.End.Next;
                    yield return inlineHyperlinkElementInfo;
                }
                else
                    token = token.Next;
            }
        }

        private ElementInfo _TryGetInlineHyperlinkElementInfoAt(CreoleToken start, CreoleToken end)
        {
            var token = start;
            ElementInfo result;

            if (_Matches(start, end, Text, Punctuation, Slash, Slash, Text)
                && token.Next.Code == Punctuation && token.Next.Length == 1 && Context.Text[token.Next.StartIndex] == ':'
                && _IsInlineHyperlinkProtocol(token))
            {
                token = token.Next.Next.Next.Next;

                while (token.Next != end && token.Next.Code != WhiteSpace)
                    token = token.Next;
                while (token.Code != Text && token.Code != Tilde)
                    token = token.Previous;

                if (token.Next?.Code == Slash && (token.Next.Next?.Code != Slash || token.Next.Next.Next?.Code == Slash))
                    token = token.Next;

                result = new ElementInfo(InlineHyperlink)
                {
                    Start = (start.Previous?.Code == Tilde ? start.Previous : start),
                    End = token
                };
            }
            else
                result = null;

            return result;
        }

        private bool _Matches(CreoleToken start, CreoleToken end, params CreoleTokenCode[] tokenCodes)
        {
            var isMatch = (start != end);
            if (isMatch)
                using (var tokenCode = ((IEnumerable<CreoleTokenCode>)tokenCodes).GetEnumerator())
                {
                    var token = start;
                    while (isMatch && tokenCode.MoveNext())
                        if (token == null)
                            isMatch = false;
                        else
                        {
                            isMatch = (token != end && token.Code == tokenCode.Current);
                            token = token.Next;
                        }
                }
            return isMatch;
        }

        private bool _IsNumber(CreoleToken token)
        {
            var isDigit = true;

            var index = token.StartIndex;
            while (index < token.EndIndex && isDigit)
            {
                var character = Context.Text[index];
                if ('0' <= character && character <= '9')
                    index++;
                else
                    isDigit = false;
            }

            return isDigit;
        }

        private bool _IsEnglishLetter(CreoleToken token)
        {
            var isEnglishLetter = true;

            var index = token.StartIndex;
            while (index < token.EndIndex && isEnglishLetter)
            {
                var character = char.ToLowerInvariant(Context.Text[index]);
                if ('a' <= character && character <= 'z')
                    index++;
                else
                    isEnglishLetter = false;
            }

            return isEnglishLetter;
        }

        private bool _HasParentType(ElementInfo elementInfo, RichTextElementType type)
        {
            var elementInfoParent = elementInfo.Parent;
            while (elementInfoParent != null && elementInfoParent.Type != type)
                elementInfoParent = elementInfoParent.Parent;
            return (elementInfoParent != null);
        }

        private IEnumerable<ElementInfo> _LeftPrecedenceMerge(IEnumerable<ElementInfo> source, params IEnumerable<ElementInfo>[] otherSources)
        {
            var result = source;
            foreach (var otherSource in otherSources)
                result = _LeftPrecedenceMerge(result, otherSource);
            return result;
        }

        private IEnumerable<ElementInfo> _LeftPrecedenceMerge(IEnumerable<ElementInfo> leftSource, IEnumerable<ElementInfo> rightSource)
        {
            using (var leftEnumerator = leftSource.GetEnumerator())
            using (var rightEnumerator = rightSource.GetEnumerator())
            {
                var leftHasValue = leftEnumerator.MoveNext();
                var rightHasValue = rightEnumerator.MoveNext();

                while (leftHasValue && rightHasValue)
                {
                    var left = leftEnumerator.Current;
                    var right = rightEnumerator.Current;

                    var compareResult = _Compare(left, right);

                    if (compareResult < 0)
                    {
                        yield return left;
                        leftHasValue = leftEnumerator.MoveNext();
                    }
                    else if (compareResult > 0)
                    {
                        yield return right;
                        rightHasValue = rightEnumerator.MoveNext();
                    }
                    else if (left.Contains(right))
                    {
                        left.Children.AddLast(right);
                        right.Parent = left;
                        rightHasValue = rightEnumerator.MoveNext();
                    }
                    else if (right.Contains(left))
                    {
                        right.Children.AddLast(left);
                        left.Parent = right;
                        leftHasValue = leftEnumerator.MoveNext();
                    }
                    else
                    {
                        if (left.Start.StartIndex < right.Start.StartIndex)
                            foreach (var child in right.Children)
                                if (_Compare(child, left) < 0)
                                    yield return child;
                        yield return left;

                        leftHasValue = leftEnumerator.MoveNext();
                        rightHasValue = rightEnumerator.MoveNext();
                    }
                }

                while (leftHasValue)
                {
                    yield return leftEnumerator.Current;
                    leftHasValue = leftEnumerator.MoveNext();
                }

                while (rightHasValue)
                {
                    yield return rightEnumerator.Current;
                    rightHasValue = rightEnumerator.MoveNext();
                }
            }
        }

        private IEnumerable<ElementInfo> _OmitOverlapMerge(IEnumerable<ElementInfo> leftSource, IEnumerable<ElementInfo> rightSource)
        {
            using (var leftEnumerator = leftSource.GetEnumerator())
            using (var rightEnumerator = rightSource.GetEnumerator())
            {
                var leftHasValue = leftEnumerator.MoveNext();
                var rightHasValue = rightEnumerator.MoveNext();

                while (leftHasValue && rightHasValue)
                {
                    var left = leftEnumerator.Current;
                    var right = rightEnumerator.Current;

                    var compareResult = _Compare(left, right);

                    if (compareResult < 0)
                    {
                        yield return left;
                        leftHasValue = leftEnumerator.MoveNext();
                    }
                    else if (compareResult > 0)
                    {
                        yield return right;
                        rightHasValue = rightEnumerator.MoveNext();
                    }
                    else
                    {
                        if (left.Contains(right))
                        {
                            yield return left;
                            yield return right;
                        }
                        else if (right.Contains(left))
                        {
                            yield return right;
                            yield return left;
                        }

                        leftHasValue = leftEnumerator.MoveNext();
                        rightHasValue = rightEnumerator.MoveNext();
                    }
                }

                while (leftHasValue)
                {
                    yield return leftEnumerator.Current;
                    leftHasValue = leftEnumerator.MoveNext();
                }

                while (rightHasValue)
                {
                    yield return rightEnumerator.Current;
                    rightHasValue = rightEnumerator.MoveNext();
                }
            }
        }

        private static int _Compare(ElementInfo left, ElementInfo right)
        {
            if (left.End.StartIndex < right.Start.StartIndex)
                return -1;
            else if (left.Start.StartIndex > right.End.StartIndex)
                return 1;
            else
                return 0;
        }

        private IEnumerable<CreoleElement> _CreateElements(CreoleToken start, IEnumerable<ElementInfo> rootElementInfos, CreoleToken end)
        {
            var previousTextStart = start;
            var elements = new List<CreoleElement>();

            foreach (var elementInfo in rootElementInfos)
            {
                if (previousTextStart != elementInfo.Start)
                {
                    var plainText = Substring(Context.Text, previousTextStart, elementInfo.Start);
                    elements.Add(new CreoleTextElement(plainText));
                }

                var element = _GetElementFrom(elementInfo);
                if (element != null)
                    elements.Add(element);

                previousTextStart = elementInfo.End.Next;
            }
            if (previousTextStart != end.Next)
            {
                var plainText = Substring(Context.Text, previousTextStart, end.Next);
                elements.Add(new CreoleTextElement(plainText));
            }

            return elements;
        }

        private CreoleElement _GetElementFrom(ElementInfo elementInfo)
        {
            CreoleElement element = null;
            switch (elementInfo.Type)
            {
                case Hyperlink:
                    element = _CreateHyperlinElement(elementInfo);
                    break;

                case Image:
                    element = _CreateImageElement(elementInfo);
                    break;

                case InlineHyperlink:
                    element = _CreateInlineHyperlinkElement(elementInfo);
                    break;

                case Code:
                    element = _CreateCodeElement(elementInfo);
                    break;

                case Strong:
                    element = _CreateStrongElement(elementInfo);
                    break;

                case Emphasis:
                    element = _CreateEmphasisElement(elementInfo);
                    break;

                case LineBreak:
                    element = _CreateLineBreakElement(elementInfo);
                    break;

                case Plugin:
                    element = _CreatePluginElement(elementInfo);
                    break;
            }
            return element;
        }

        private CreoleElement _CreateHyperlinElement(ElementInfo elementInfo)
        {
            if (elementInfo.ContentStart != null)
            {
                var childElements = _CreateElements(elementInfo.ContentStart, elementInfo.Children, elementInfo.ContentEnd);
                var destination = Substring(Context.Text, elementInfo.Start.Next.Next, elementInfo.ContentStart.Previous);
                return new CreoleHyperlinkElement(destination, childElements);
            }
            else
            {
                var destination = Substring(Context.Text, elementInfo.Start.Next.Next, elementInfo.End.Previous);
                return new CreoleHyperlinkElement(destination, new[] { new CreoleTextElement(destination) });
            }
        }

        private CreoleElement _CreateImageElement(ElementInfo elementInfo)
        {
            if (elementInfo.ContentStart != null)
            {
                var alternativeText = Substring(Context.Text, elementInfo.ContentStart, elementInfo.ContentEnd.Next);
                var source = Substring(Context.Text, elementInfo.Start.Next.Next, elementInfo.ContentStart.Previous);
                return new CreoleImageElement(source, alternativeText);
            }
            else
            {
                var source = Substring(Context.Text, elementInfo.Start.Next.Next, elementInfo.End.Previous);
                return new CreoleImageElement(source);
            }
        }

        private CreoleElement _CreateInlineHyperlinkElement(ElementInfo elementInfo)
        {
            if (elementInfo.Start.Code == Tilde)
            {
                var escapedUrl = Substring(Context.Text, elementInfo.Start.Next, elementInfo.End.Next);
                return new CreoleTextElement(escapedUrl);
            }
            else
            {
                var destination = Substring(Context.Text, elementInfo.Start, elementInfo.End.Next);
                return new CreoleHyperlinkElement(destination, new[] { new CreoleTextElement(destination) });
            }
        }

        private CreoleElement _CreateCodeElement(ElementInfo elementInfo)
        {
            var code = Substring(Context.Text, elementInfo.ContentStart, elementInfo.ContentEnd.Next);
            return new CreoleCodeElement(code);
        }

        private CreoleElement _CreateStrongElement(ElementInfo elementInfo)
        {
            var childElements = _CreateElements(elementInfo.ContentStart, elementInfo.Children, elementInfo.ContentEnd);
            return new CreoleStrongElement(childElements);
        }

        private CreoleElement _CreateEmphasisElement(ElementInfo elementInfo)
        {
            var childElements = _CreateElements(elementInfo.ContentStart, elementInfo.Children, elementInfo.ContentEnd);
            return new CreoleEmphasisElement(childElements);
        }

        private CreoleElement _CreateLineBreakElement(ElementInfo elementInfo)
            => new CreoleLineBreakElement();

        private CreoleElement _CreatePluginElement(ElementInfo elementInfo)
        {
            var pluginText = Substring(Context.Text, elementInfo.ContentStart, elementInfo.ContentEnd.Next);
            return new CreolePluginElement(pluginText);
        }

        private delegate ElementInfo ElementInfoFactory(CreoleToken start, CreoleToken end);
        private delegate IEnumerable<ElementInfo> ElementInfosFactory(CreoleToken start, CreoleToken end);

        private sealed class ElementInfo
        {
            internal ElementInfo(RichTextElementType type)
            {
                Type = type;
                Children = new LinkedList<ElementInfo>();
            }

            internal RichTextElementType Type { get; }

            internal CreoleToken Start { get; set; }

            internal CreoleToken ContentStart { get; set; }

            internal CreoleToken End { get; set; }

            internal CreoleToken ContentEnd { get; set; }

            internal ElementInfo Parent { get; set; }

            internal LinkedList<ElementInfo> Children { get; }

            internal bool Contains(ElementInfo other)
                => (ContentStart != null
                && ContentEnd != null
                && ContentStart.StartIndex <= other.Start.StartIndex
                && ContentEnd.StartIndex >= other.End.StartIndex);
        }
    }
}