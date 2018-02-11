using System;
using System.Collections.Generic;
#if net20
using static Mup.StringHelper;
#else
using static System.String;
#endif

namespace Mup
{
    /// <summary>Specifies options for the <see cref="CreoleParser"/>.</summary>
    public sealed class CreoleParserOptions
    {
#if netstandard10
        private static IReadOnlyCollection<string> _defaultInlineHyperlinkProtocols
#else
        private static IEnumerable<string> _defaultInlineHyperlinkProtocols
#endif
            = new[] { "http", "https", "ftp", "ftps" };

#if netstandard10
        private IReadOnlyCollection<string>
#else
        private IEnumerable<string>
#endif
            _inlineHyperlinkProtocols;

        /// <summary>Initializes a new instance of the <see cref="CreoleParserOptions"/> class.</summary>
        public CreoleParserOptions()
        {
            _inlineHyperlinkProtocols = _defaultInlineHyperlinkProtocols;
        }

        /// <summary>The protocols to consider when parsing inline hyperlinks (e.g.: http, https and so on). The defauts are <c>http</c>, <c>https</c>, <c>ftp</c> and <c>ftps</c>.</summary>
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
                if (copy.FindIndex(IsNullOrWhiteSpace) >= 0)
                    throw new ArgumentException("Cannot contain null, empty or white space values.", nameof(InlineHyperlinkProtocols));
                _inlineHyperlinkProtocols = copy;
            }
        }
    }
}