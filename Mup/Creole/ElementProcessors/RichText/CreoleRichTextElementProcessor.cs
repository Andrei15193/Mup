using Mup.Scanner;
using System;
using System.Collections.Generic;

namespace Mup.Creole.ElementProcessors.RichText
{
    internal abstract class CreoleRichTextElementProcessor : IDisposable
    {
        private readonly TokenRange<CreoleTokenCode> _tokens;
        private int _index;
        private int _startIndex = 0;
        private CreoleRichTextElementData _result = null;
        private ICreoleRichTextElementDataIterator _element;
        private IEnumerator<Token<CreoleTokenCode>> _token;

        internal CreoleRichTextElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : this(context, tokens, elementIterator: null)
        {
        }

        internal CreoleRichTextElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, CreoleRichTextElementProcessor elementProcessor)
            : this(context, tokens, (elementProcessor != null ? new CreoleRichTextElementProcessorIteratorAdapter(elementProcessor) : null))
        {
        }

        internal CreoleRichTextElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, IEnumerable<CreoleRichTextElementData> elements)
            : this(context, tokens, (elements != null ? new CreoleRichTextElementDataCollectionAdapter(elements) : null))
        {
        }

        private CreoleRichTextElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens, ICreoleRichTextElementDataIterator elementIterator)
        {
            _tokens = tokens;
            Context = context;
            _element = elementIterator;
            if (_element != null && _element.MoveNext())
            {
                _token = _tokens.SubRange(_startIndex, (_element.Current.StartIndex - _startIndex)).GetEnumerator();
                _startIndex = _element.Current.EndIndex;
            }
            else
            {
                _element?.Dispose();
                _element = null;
                _token = _tokens.GetEnumerator();
                _startIndex = _tokens.Count;
            }
        }

        public void Dispose()
            => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _element?.Dispose();
                _token?.Dispose();
            }
        }

        protected void SetResult(CreoleRichTextElementData result)
        {
            _result = result;
        }

        internal bool MoveNext()
        {
            _result = null;
            if (_token == null && _element != null)
            {
                SetResult(_element.Current);
                _MoveToNextElement();
            }
            else
                while (_result == null && _token != null)
                    if (_token.MoveNext())
                    {
                        Process();
                        _index++;
                    }
                    else
                    {
                        Complete();

                        _token.Dispose();
                        _token = null;
                        if (_result == null && _element != null)
                        {
                            SetResult(_element.Current);
                            _MoveToNextElement();
                        }
                    }
            return (_result != null);
        }

        internal CreoleRichTextElementData Current
        {
            get
            {
                if (_result == null)
                    throw new InvalidOperationException("The last call to MoveNext() method must return true for this property to be accessible.");
                return _result;
            }
        }

        protected CreoleParserContext Context { get; }

        protected Token<CreoleTokenCode> Token
            => _token.Current;

        protected int Index
            => _index;

        protected abstract void Process();

        protected abstract void Complete();

        private void _MoveToNextElement()
        {
            if (_element.MoveNext())
            {
                if (_startIndex < _element.Current.StartIndex)
                    _token = _tokens.SubRange(_startIndex, (_element.Current.StartIndex - _startIndex)).GetEnumerator();
                _index = _startIndex;
                _startIndex = _element.Current.EndIndex;
            }
            else
            {
                _element.Dispose();
                _element = null;
                _token = _tokens.SubRange(_startIndex, (_tokens.Count - _startIndex)).GetEnumerator();
                _index = _startIndex;
                _startIndex = _tokens.Count;
            }
        }

        private interface ICreoleRichTextElementDataIterator : IDisposable
        {
            bool MoveNext();

            CreoleRichTextElementData Current { get; }
        }

        private sealed class CreoleRichTextElementProcessorIteratorAdapter : ICreoleRichTextElementDataIterator
        {
            private readonly CreoleRichTextElementProcessor _processor;

            internal CreoleRichTextElementProcessorIteratorAdapter(CreoleRichTextElementProcessor processor)
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

        private sealed class CreoleRichTextElementDataCollectionAdapter : ICreoleRichTextElementDataIterator
        {
            private readonly IEnumerator<CreoleRichTextElementData> _elementData;

            internal CreoleRichTextElementDataCollectionAdapter(IEnumerable<CreoleRichTextElementData> creoleRichTextElementsData)
            {
                _elementData = creoleRichTextElementsData.GetEnumerator();
            }

            public void Dispose()
                => _elementData.Dispose();

            public bool MoveNext()
                => _elementData.MoveNext();

            public CreoleRichTextElementData Current
                => _elementData.Current;
        }
    }
}