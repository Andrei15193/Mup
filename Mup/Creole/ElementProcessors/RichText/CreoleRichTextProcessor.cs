using Mup.Elements;
using Mup.Scanner;
using System;
using System.Collections.Generic;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleRichTextProcessor
    {
        private delegate CreoleStyleElementProcessor CreoleStyleElementProcessorFactory(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> baseElementsData);
        private static readonly IDictionary<CreoleRichTextElementType, CreoleStyleElementProcessorFactory> _styleElementFactories = new Dictionary<CreoleRichTextElementType, CreoleStyleElementProcessorFactory>
        {
            { CreoleRichTextElementType.Emphasis, (context, tokens, baseElementsData) => new CreoleEmphasisElementProcessor(context, tokens, baseElementsData) },
            { CreoleRichTextElementType.Strong, (context, tokens, baseElementsData) => new CreoleStrongElementProcessor(context, tokens, baseElementsData) }
        };
        private static LineBreakElement _creoleLineBreakElement = new LineBreakElement();

        private readonly CreoleParserContext _context;

        internal CreoleRichTextProcessor(CreoleParserContext context)
        {
            _context = context;
        }

        internal IEnumerable<Element> Process(TokenRange<CreoleTokenCode> tokens)
        {
            var baseElementsData = new List<CreoleRichTextElementData>();
            using (var baseElementProcessor = _GetBaseElementProcessor(_context, tokens))
                while (baseElementProcessor.MoveNext())
                    baseElementsData.Add(baseElementProcessor.Current);

            var elementsData = new List<CreoleRichTextElementData>();
            using (var elementDataIterator = new MergeElementDataIterator(_GetStyleElementIterator(_context, tokens, baseElementsData), new ElementDataCollectionIteratorAdapter(baseElementsData)))
                while (elementDataIterator.MoveNext())
                    elementsData.Add(elementDataIterator.Current);

            return _GetCreoleElements(tokens, elementsData);
        }

        private CreoleRichTextElementProcessor _GetBaseElementProcessor(CreoleParserContext contenxt, TokenRange<CreoleTokenCode> tokens)
            => new CreoleLineBreakElementProcessor(
                _context,
                tokens,
                new CreoleInlineHyperlinkProcessor(
                    _context,
                    tokens,
                    new CreolePluginElementProcessor(
                        _context,
                        tokens,
                        new CreoleImageElementProcessor(
                            _context,
                            tokens,
                            new CreoleHyperlinkElementProcessor(
                                _context,
                                tokens,
                                new CreoleCodeElementProcessor(
                                    _context,
                                    tokens)
                            )
                        )
                    )
                )
            );

        private CreoleRichTextElementProcessor _GetHyperlinkBaseElementProcessor(CreoleParserContext contenxt, TokenRange<CreoleTokenCode> tokens)
            => new CreoleLineBreakElementProcessor(
                _context,
                tokens,
                new CreoleImageElementProcessor(
                    _context,
                    tokens,
                    new CreoleCodeElementProcessor(
                        _context,
                        tokens)
                )
            );

        private IEnumerable<Element> _GetCreoleElements(TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> elementsData)
            => _GetCreoleElements(tokens, _GetRootNodesFrom(tokens, elementsData));

        private IEnumerable<Element> _GetCreoleElements(TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementNodeData> nodesData)
            => _GetCreoleElements(tokens, 0, tokens.Count, nodesData);

        private IEnumerable<Element> _GetCreoleElements(TokenRange<CreoleTokenCode> tokens, int startIndex, int endIndex, IEnumerable<CreoleRichTextElementNodeData> nodesData)
        {
            var elements = new List<Element>();
            var subRangeStartIndex = startIndex;
            foreach (var nodeData in nodesData)
            {
                if (subRangeStartIndex < nodeData.StartIndex)
                    elements.Add(new TextElement(TokenRangeHelper.GetPlainText(tokens.SubRange(subRangeStartIndex, (nodeData.StartIndex - subRangeStartIndex)))));

                elements.Add(_GetCreoleElementFrom(tokens, nodeData));
                subRangeStartIndex = nodeData.EndIndex;
            }
            if (subRangeStartIndex < endIndex)
            {
                var subRange = tokens.SubRange(subRangeStartIndex, (endIndex - subRangeStartIndex));
                elements.Add(new TextElement(TokenRangeHelper.GetPlainText(subRange)));
            }

            return elements;
        }

        private IEnumerable<CreoleRichTextElementNodeData> _GetRootNodesFrom(TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> elementsData)
        {
            var rootNodes = new List<CreoleRichTextElementNodeData>();
            var visitedElementTypes = new List<CreoleRichTextElementType>(10);

            foreach (var elementData in elementsData)
            {
                var node = new CreoleRichTextElementNodeData(elementData);

                IList<CreoleRichTextElementNodeData> siblingNodes = rootNodes;
                while (siblingNodes.Count > 0 && _Contains(siblingNodes[siblingNodes.Count - 1], node))
                {
                    var parentNode = siblingNodes[siblingNodes.Count - 1];
                    siblingNodes = parentNode.ChildNodes;
                    visitedElementTypes.Add(parentNode.ElementType);
                }

                if (node.ElementType == CreoleRichTextElementType.Hyperlink)
                {
                    var contentRange = tokens.SubRange(node.ContentStartIndex, (node.ContentEndIndex - node.ContentStartIndex));
                    foreach (var hyperlinkContentElementNodeData in _GetHyperlinkContentElementNodesData(contentRange, visitedElementTypes))
                        node.ChildNodes.Add(_AddIndexOffset(node.ContentStartIndex, hyperlinkContentElementNodeData));
                }

                siblingNodes.Add(node);
                visitedElementTypes.Clear();
            }

            return rootNodes;
        }

        private CreoleRichTextElementNodeData _AddIndexOffset(int offset, CreoleRichTextElementNodeData nodeData)
            => new CreoleRichTextElementNodeData(
                new CreoleRichTextElementData(
                    elementType: nodeData.ElementType,
                    startIndex: (offset + nodeData.StartIndex),
                    endIndex: (offset + nodeData.EndIndex),
                    urlStartIndex: (offset + nodeData.UrlStartIndex),
                    urlEndIndex: (offset + nodeData.UrlEndIndex),
                    contentStartIndex: (offset + nodeData.ContentStartIndex),
                    contentEndIndex: (offset + nodeData.ContentEndIndex)
                ),
                nodeData.ChildNodes
            );

        private IEnumerable<CreoleRichTextElementNodeData> _GetHyperlinkContentElementNodesData(TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementType> excludedElementTypes)
        {
            var baseElementsData = new List<CreoleRichTextElementData>();
            using (var baseElementProcessor = _GetHyperlinkBaseElementProcessor(_context, tokens))
                while (baseElementProcessor.MoveNext())
                    baseElementsData.Add(baseElementProcessor.Current);

            IElementDataIterator elementDataIterator;
            var styleElementsIterator = _GetStyleElementIterator(_context, tokens, baseElementsData, excludedElementTypes);
            if (styleElementsIterator != null)
                elementDataIterator = new MergeElementDataIterator(styleElementsIterator, new ElementDataCollectionIteratorAdapter(baseElementsData));
            else
                elementDataIterator = new ElementDataCollectionIteratorAdapter(baseElementsData);

            var elementsData = new List<CreoleRichTextElementData>();
            using (elementDataIterator)
                while (elementDataIterator.MoveNext())
                    elementsData.Add(elementDataIterator.Current);

            return _GetRootNodesFrom(tokens, elementsData);
        }

        private Element _GetCreoleElementFrom(TokenRange<CreoleTokenCode> tokens, CreoleRichTextElementNodeData nodeData)
        {
            Element result = null;

            switch (nodeData.ElementType)
            {
                case CreoleRichTextElementType.Code:
                    result = new CodeElement(
                        _GetContentFrom(tokens, nodeData)
                    );
                    break;

                case CreoleRichTextElementType.Hyperlink:
                    result = new HyperlinkElement(
                        _GetUrlFrom(tokens, nodeData),
                        _GetCreoleElements(tokens, nodeData.ContentStartIndex, nodeData.ContentEndIndex, nodeData.ChildNodes)
                    );
                    break;

                case CreoleRichTextElementType.Image:
                    if (nodeData.UrlEndIndex < nodeData.ContentStartIndex)
                        result = new ImageElement(
                            _GetUrlFrom(tokens, nodeData),
                            _GetContentFrom(tokens, nodeData)
                        );
                    else
                        result = new ImageElement(
                            _GetUrlFrom(tokens, nodeData)
                        );
                    break;

                case CreoleRichTextElementType.Plugin:
                    result = new PluginElement(
                        _GetContentFrom(tokens, nodeData)
                    );
                    break;

                case CreoleRichTextElementType.LineBreak:
                    result = _creoleLineBreakElement;
                    break;

                case CreoleRichTextElementType.InlineHyperlink:
                    var url = _GetUrlFrom(tokens, nodeData);
                    result = new HyperlinkElement(url, new[] { new TextElement(url) });
                    break;

                case CreoleRichTextElementType.EscapedInlineHyperlink:
                    result = new TextElement(
                        _GetUrlFrom(tokens, nodeData)
                    );
                    break;

                case CreoleRichTextElementType.Emphasis:
                    result = new EmphasisElement(
                        _GetCreoleElements(tokens, nodeData.ContentStartIndex, nodeData.ContentEndIndex, nodeData.ChildNodes)
                    );
                    break;

                case CreoleRichTextElementType.Strong:
                    result = new StrongElement(
                        _GetCreoleElements(tokens, nodeData.ContentStartIndex, nodeData.ContentEndIndex, nodeData.ChildNodes)
                    );
                    break;
            }

            return result;
        }

        private string _GetUrlFrom(TokenRange<CreoleTokenCode> tokens, CreoleRichTextElementNodeData richTextElementData)
        {
            var urlStartIndex = richTextElementData.UrlStartIndex;
            var urlEndIdnex = richTextElementData.UrlEndIndex;
            var urlRange = tokens.SubRange(urlStartIndex, (urlEndIdnex - urlStartIndex));
            return TokenRangeHelper.GetPlainText(urlRange);
        }

        private string _GetContentFrom(TokenRange<CreoleTokenCode> tokens, CreoleRichTextElementNodeData richTextElementData)
        {
            var contentStartIndex = richTextElementData.ContentStartIndex;
            var contentEndIdnex = richTextElementData.ContentEndIndex;
            var contentRange = tokens.SubRange(contentStartIndex, (contentEndIdnex - contentStartIndex));
            return TokenRangeHelper.GetPlainText(contentRange);
        }

        private static bool _Contains(CreoleRichTextElementData parent, CreoleRichTextElementData child)
            => (parent.ContentStartIndex <= child.StartIndex && child.EndIndex <= parent.ContentEndIndex);

        private static bool _Contains(CreoleRichTextElementNodeData parent, CreoleRichTextElementNodeData child)
            => (parent.ContentStartIndex <= child.StartIndex && child.EndIndex <= parent.ContentEndIndex);

        private IElementDataIterator _GetStyleElementIterator(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> baseElementsData)
            => _GetStyleElementIterator(context, tokens, baseElementsData, null);

        private IElementDataIterator _GetStyleElementIterator(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> baseElementsData, IEnumerable<CreoleRichTextElementType> excludedElementTypes)
        {
            var processors = new List<IElementDataIterator>();
            if (excludedElementTypes == null)
                foreach (var styleElementFactory in _styleElementFactories)
                {
                    var elementType = styleElementFactory.Key;
                    processors.Add(new ElementProcessorFilterIterator(styleElementFactory.Value(_context, tokens, baseElementsData), elementType));
                }
            else
                foreach (var styleElementFactory in _styleElementFactories)
                {
                    var elementType = styleElementFactory.Key;
                    if (!_Contains(excludedElementTypes, elementType))
                        processors.Add(new ElementProcessorFilterIterator(styleElementFactory.Value(_context, tokens, baseElementsData), elementType));
                }

            IElementDataIterator styleElementIterator = null;
            using (var processor = processors.GetEnumerator())
                if (processor.MoveNext())
                {
                    styleElementIterator = processor.Current;
                    while (processor.MoveNext())
                        styleElementIterator = new MergeElementDataIterator(styleElementIterator, processor.Current);
                }
            return styleElementIterator;
        }

        private static bool _Contains(IEnumerable<CreoleRichTextElementType> collection, CreoleRichTextElementType item)
        {
            var found = false;
            using (var excludedElementType = collection.GetEnumerator())
                while (!found && excludedElementType.MoveNext())
                    found = (excludedElementType.Current == item);
            return found;
        }

        private interface IElementDataIterator : IDisposable
        {
            bool MoveNext();

            CreoleRichTextElementData Current { get; }
        }

        private sealed class MergeElementDataIterator : IElementDataIterator
        {
            private readonly IElementDataIterator _left;
            private readonly IElementDataIterator _right;
            private bool _hasCurrent = false;
            private CreoleRichTextElementData _current;
            private bool _leftHasValue;
            private bool _rightHasValue;

            internal MergeElementDataIterator(IElementDataIterator left, IElementDataIterator right)
            {
                _left = left;
                _leftHasValue = left.MoveNext();
                _right = right;
                _rightHasValue = right.MoveNext();
            }

            public void Dispose()
            {
                _left.Dispose();
                _right.Dispose();
            }

            public bool MoveNext()
            {
                _hasCurrent = false;

                while (!_hasCurrent && _leftHasValue && _rightHasValue)
                    if (_Contains(_left.Current, _right.Current))
                    {
                        _SetCurrent(_left.Current);
                        _leftHasValue = _left.MoveNext();
                    }
                    else if (_Contains(_right.Current, _left.Current))
                    {
                        _SetCurrent(_right.Current);
                        _rightHasValue = _right.MoveNext();
                    }
                    else if (_ElementsOverlap())
                    {
                        _leftHasValue = _left.MoveNext();
                        _rightHasValue = _right.MoveNext();
                    }
                    else if (_left.Current.StartIndex < _right.Current.StartIndex)
                    {
                        _SetCurrent(_left.Current);
                        _leftHasValue = _left.MoveNext();
                    }
                    else
                    {
                        _SetCurrent(_right.Current);
                        _rightHasValue = _right.MoveNext();
                    }

                if (!_hasCurrent && _leftHasValue)
                {
                    _SetCurrent(_left.Current);
                    _leftHasValue = _left.MoveNext();
                }
                else if (!_hasCurrent && _rightHasValue)
                {
                    _SetCurrent(_right.Current);
                    _rightHasValue = _right.MoveNext();
                }

                return _hasCurrent;
            }

            public CreoleRichTextElementData Current
            {
                get
                {
                    if (!_hasCurrent)
                        throw new InvalidOperationException("The last call to MoveNext() method must return true for this property to be accessible.");
                    return _current;
                }
            }

            private bool _ElementsOverlap()
                => (_left.Current.EndIndex >= _right.Current.StartIndex && _right.Current.EndIndex >= _left.Current.StartIndex);

            private void _SetCurrent(CreoleRichTextElementData value)
            {
                _hasCurrent = true;
                _current = value;
            }
        }

        private sealed class ElementDataCollectionIteratorAdapter : IElementDataIterator
        {
            private readonly IEnumerator<CreoleRichTextElementData> _elementData;

            internal ElementDataCollectionIteratorAdapter(IEnumerable<CreoleRichTextElementData> elementsData)
            {
                _elementData = elementsData.GetEnumerator();
            }

            public void Dispose()
                => _elementData.Dispose();

            public bool MoveNext()
                => _elementData.MoveNext();

            public CreoleRichTextElementData Current
                => _elementData.Current;
        }

        private sealed class ElementProcessorIteratorAdapter : IElementDataIterator
        {
            private readonly CreoleRichTextElementProcessor _processor;

            internal ElementProcessorIteratorAdapter(CreoleRichTextElementProcessor processor)
            {
                _processor = processor;
            }

            public void Dispose()
                => _processor.Dispose();

            public bool MoveNext()
                => _processor.MoveNext();

            public CreoleRichTextElementData Current
                => _processor.Current;
        }

        private sealed class ElementProcessorFilterIterator : IElementDataIterator
        {
            private readonly CreoleRichTextElementProcessor _processor;
            private readonly CreoleRichTextElementType _elementType;

            internal ElementProcessorFilterIterator(CreoleRichTextElementProcessor processor, CreoleRichTextElementType elementType)
            {
                _processor = processor;
                _elementType = elementType;
            }

            public void Dispose()
                => _processor.Dispose();

            public bool MoveNext()
            {
                var hasValue = _processor.MoveNext();
                while (hasValue && _processor.Current.ElementType != _elementType)
                    hasValue = _processor.MoveNext();
                return hasValue;
            }

            public CreoleRichTextElementData Current
                => _processor.Current;
        }

        private class CreoleRichTextElementNodeData
        {
            private readonly CreoleRichTextElementData _elementData;

            internal CreoleRichTextElementNodeData(CreoleRichTextElementData elementData)
                : this(elementData, new List<CreoleRichTextElementNodeData>())
            {
            }

            internal CreoleRichTextElementNodeData(CreoleRichTextElementData elementData, IList<CreoleRichTextElementNodeData> children)
            {
                _elementData = elementData;
                ChildNodes = children;
            }

            internal CreoleRichTextElementType ElementType
                => _elementData.ElementType;

            internal int StartIndex
                => _elementData.StartIndex;

            internal int EndIndex
                => _elementData.EndIndex;

            internal int UrlStartIndex
                => _elementData.UrlStartIndex;

            internal int UrlEndIndex
                => _elementData.UrlEndIndex;

            internal int ContentStartIndex
                => _elementData.ContentStartIndex;

            internal int ContentEndIndex
                => _elementData.ContentEndIndex;

            internal IList<CreoleRichTextElementNodeData> ChildNodes { get; }
        }
    }
}