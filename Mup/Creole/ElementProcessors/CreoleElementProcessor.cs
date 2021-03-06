using Mup.Creole.ElementProcessors.RichText;
using Mup.Elements;
using Mup.Scanner;
using System;
using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal abstract class CreoleElementProcessor : IDisposable
    {
        private static readonly IEnumerable<Element> _emptyCreoleElements = new Element[0];
        private readonly TokenRange<CreoleTokenCode> _tokens;
        private int _index = 0;
        private bool _isOnNewLine = true;
        private int _elementStartIndex = 0;
        private int _elementEndIndex = 0;
        private readonly IEnumerator<Token<CreoleTokenCode>> _token;
        private bool _tokenHasValue;
        private CreoleElementInfo _result = null;
        private bool _isCompleted = false;

        protected CreoleElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
        {
            Context = context;
            _tokens = tokens;

            _token = tokens.GetEnumerator();
            _tokenHasValue = _token.MoveNext();
        }

        public void Dispose()
            => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _token.Dispose();
        }

        internal CreoleElementInfo Current
        {
            get
            {
                if (_result == null)
                    throw new InvalidOperationException("The last call to MoveNext() method must return true for this property to be accessible.");
                return _result;
            }
        }

        internal bool MoveNext()
        {
            if (_isCompleted)
            {
                _result = null;
                return false;
            }

            if (_tokenHasValue)
            {
                _result = null;
                while (_result == null && _tokenHasValue)
                {
                    Process();
                    _isOnNewLine = (Token.Code == NewLine || Token.Code == BlankLine || (_index == 0 && Token.Code == WhiteSpace));
                    _tokenHasValue = _token.MoveNext();
                    _index++;
                }
                if (_result == null && !_tokenHasValue)
                {
                    Complete();
                    _isCompleted = true;
                }
            }
            else if (_result != null)
            {
                _result = null;
                Complete();
                _isCompleted = true;
            }

            return (_result != null);
        }

        protected CreoleParserContext Context { get; }

        protected Token<CreoleTokenCode> Token
            => _token.Current;

        protected int Index
            => _index;

        protected bool IsOnNewLine
            => _isOnNewLine;

        protected abstract void Process();

        protected abstract void Complete();

        protected void SetElementStartIndex()
        {
            _elementStartIndex = Index;
        }

        protected void SetElementEndIndex()
        {
            _elementEndIndex = Index;
        }

        protected void SetResult(Element creoleElement)
        {
            if (creoleElement != null)
            {
                _result = new CreoleElementInfo(_elementStartIndex, _elementEndIndex, creoleElement);
                _elementStartIndex = _elementEndIndex;
            }
        }

        protected string GetPlainText()
            => GetPlainText(_elementStartIndex, _elementEndIndex);

        protected string GetPlainText(int startIndex, int endIndex)
            => TokenRangeHelper.GetPlainText(GetTokens(startIndex, endIndex));

        protected IEnumerable<Element> GetRichText()
            => GetRichText(_elementStartIndex, _elementEndIndex);

        protected IEnumerable<Element> GetRichText(int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
                return _emptyCreoleElements;

            var richTextProcessor = new CreoleRichTextProcessor(Context);
            var tokens = GetTokens(startIndex, endIndex);
            return richTextProcessor.Process(tokens);
        }

        protected TokenRange<CreoleTokenCode> GetTokens(int startIndex, int endIndex)
        {
            if (startIndex < _elementStartIndex)
                throw new ArgumentException("Content cannot begin before the element starts.", nameof(startIndex));
            if (endIndex > _elementEndIndex)
                throw new ArgumentException("Content cannot end after the element ends.", nameof(startIndex));
            return _tokens.SubRange(startIndex, (endIndex - startIndex));
        }
    }
}