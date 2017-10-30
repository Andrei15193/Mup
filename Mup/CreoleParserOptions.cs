using System;
using System.Collections.Generic;

namespace Mup
{
    /// <summary>Specifies options for the <see cref="CreoleParser"/>.</summary>
    public sealed class CreoleParserOptions
    {
        private static IReadOnlyCollection<string> _defaultInlineHyperlinkProtocols = new[] { "http", "https", "ftp" };

        private IReadOnlyCollection<string> _inlineHyperlinkProtocols;

        /// <summary>Initializes a new instance of the <see cref="CreoleParserOptions"/> class.</summary>
        public CreoleParserOptions()
        {
            _inlineHyperlinkProtocols = _defaultInlineHyperlinkProtocols;
        }

        /// <summary>The protocols to consider when parsing inline hyperlinks (e.g.: http, https and so on). The defauts are <c>http</c>, <c>https</c> and <c>ftp</c>.</summary>
        /// <exception cref="ArgumentNullException">Thrown when the value is set to <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when the given collection contains <c>null</c>, empty or white space elements.</exception>
        public IEnumerable<string> InlineHyperlinkProtocols
        {
            get
            {
                return _inlineHyperlinkProtocols;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(InlineHyperlinkProtocols));

                var copy = new List<string>(value);
                if (copy.FindIndex(string.IsNullOrWhiteSpace) >= 0)
                    throw new ArgumentException("Cannot contain null, empty or white space values.", nameof(InlineHyperlinkProtocols));
                _inlineHyperlinkProtocols = copy;
            }
        }
    }
}