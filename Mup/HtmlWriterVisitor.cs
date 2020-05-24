using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.String;

namespace Mup
{
    /// <summary>A <see cref="ParseTreeVisitor{TResult}"/> implementation for generating HTML from an <see cref="IParseTree"/>.</summary>
    public class HtmlWriterVisitor : ParseTreeVisitor<string>
    {
        private static readonly HtmlWriterVisitorOptions _defaultOptions = new HtmlWriterVisitorOptions();
        private static readonly List<string> _voidElements = new List<string>
        {
            "area",
            "base",
            "br",
            "col",
            "embed",
            "hr",
            "img",
            "input",
            "link",
            "meta",
            "param",
            "source",
            "track",
            "wbr"
        };

        private static bool _IsVoidElement(string elementName)
            => _voidElements.Exists(voidElement => StringComparer.OrdinalIgnoreCase.Equals(voidElement, elementName));

        private readonly StringBuilder _wrappedBuilder = null;
        private readonly Stack<string> _openElements = new Stack<string>();
        private string _previousElement = null;
        private string _result = null;
        private bool _elementEnded = true;
        private StringBuilder _htmlStringBuilder = null;

        /// <summary>Initializes a new instance of the <see cref="HtmlWriterVisitor"/> class.</summary>
        public HtmlWriterVisitor()
        {
            _wrappedBuilder = null;
            Options = _defaultOptions;
        }

        /// <summary>Initializes a new instance of the <see cref="HtmlWriterVisitor"/> class.</summary>
        /// <param name="options">The <see cref="HtmlWriterVisitorOptions"/> to use when writing HTML.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="options"/> are <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <see cref="HtmlWriterVisitorOptions.IndentOffset"/> is less than 0 (zero).
        /// </exception>
        public HtmlWriterVisitor(HtmlWriterVisitorOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.IndentOffset < 0)
                throw new ArgumentException("The IndentOffset cannot be negative.", nameof(options));

            _wrappedBuilder = null;
            Options = options;
        }

        /// <summary>Initializes a new instance of the <see cref="HtmlWriterVisitor"/> class.</summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> to which to write the result.</param>
        /// <param name="options">The <see cref="HtmlWriterVisitorOptions"/> to use when writing HTML.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="stringBuilder"/> or <paramref name="options"/> are <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the <see cref="HtmlWriterVisitorOptions.IndentOffset"/> is less than 0 (zero).
        /// </exception>
        public HtmlWriterVisitor(StringBuilder stringBuilder, HtmlWriterVisitorOptions options)
        {
            if (stringBuilder == null)
                throw new ArgumentNullException(nameof(stringBuilder));
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.IndentOffset < 0)
                throw new ArgumentException("The IndentOffset cannot be negative.", nameof(options));

            _wrappedBuilder = stringBuilder;
            Options = options;
        }

        /// <summary>Gets the <see cref="StringBuilder"/> where the HTML is being written.</summary>
        [Obsolete("The use of this property is discouraged and will be removed in Mup 2.0. Please use the BeginElement, EndElement, WriteAttribute methods.")]
        protected StringBuilder HtmlStringBuilder
            => _htmlStringBuilder;

        /// <summary>Gets the <see cref="HtmlWriterVisitorOptions"/> to use when writing HTML to the result.</summary>
        protected HtmlWriterVisitorOptions Options { get; }

        /// <summary>Gets the visitor result. This method is called only after the visit operation completes.</summary>
        /// <returns>Returns the result after the entire parse tree has been visited.</returns>
        protected internal override string GetResult()
            => _result;

        /// <summary>Initializes the visitor. This method is called before any visit method.</summary>
        protected internal sealed override void BeginVisit()
        {
            _result = null;
            _elementEnded = true;
            _openElements.Clear();
            _htmlStringBuilder = (_wrappedBuilder ?? new StringBuilder());
        }

        /// <summary>Completes the visit operation. This method is called after all visit methods.</summary>
        protected internal sealed override void EndVisit()
        {
            _result = _htmlStringBuilder.ToString();
            _htmlStringBuilder = null;
        }

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected internal override void VisitHeading1Beginning()
            => BeginElement("h1");

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected internal override void VisitHeading1Ending()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected internal override void VisitHeading2Beginning()
            => BeginElement("h2");

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected internal override void VisitHeading2Ending()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected internal override void VisitHeading3Beginning()
            => BeginElement("h3");

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected internal override void VisitHeading3Ending()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected internal override void VisitHeading4Beginning()
            => BeginElement("h4");

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected internal override void VisitHeading4Ending()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected internal override void VisitHeading5Beginning()
            => BeginElement("h5");

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected internal override void VisitHeading5Ending()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected internal override void VisitHeading6Beginning()
            => BeginElement("h6");

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected internal override void VisitHeading6Ending()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected internal override void VisitParagraphBeginning()
            => BeginElement("p");

        /// <summary>Visits the ending of a paragraph.</summary>
        protected internal override void VisitParagraphEnding()
            => EndElementWithoutIndent();

        /// <summary>Visits a preformatted block.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        protected internal override void VisitPreformattedBlock(string preformattedText)
        {
            BeginElement("pre");
            BeginElementWithoutIndent("code");
            Write(preformattedText);
            EndElementWithoutIndent();
            EndElementWithoutIndent();
        }

        /// <summary>Visits the beginning of a table.</summary>
        protected internal override void VisitTableBeginning()
        {
            BeginElement("table");
            BeginElement("tbody");
        }

        /// <summary>Visits the ending of a table.</summary>
        protected internal override void VisitTableEnding()
        {
            EndElement();
            EndElement();
        }

        /// <summary>Visits the beginning of a table row.</summary>
        protected internal override void VisitTableRowBeginning()
            => BeginElement("tr");

        /// <summary>Visits the ending of a table row.</summary>
        protected internal override void VisitTableRowEnding()
            => EndElement();

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected internal override void VisitTableHeaderCellBeginning()
            => BeginElement("th");

        /// <summary>Visits the ending of a table header cell.</summary>
        protected internal override void VisitTableHeaderCellEnding()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a table cell.</summary>
        protected internal override void VisitTableCellBeginning()
            => BeginElement("td");

        /// <summary>Visits the ending of a table cell.</summary>
        protected internal override void VisitTableCellEnding()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected internal override void VisitUnorderedListBeginning()
            => BeginElement("ul");

        /// <summary>Visits the ending of an unordered list.</summary>
        protected internal override void VisitUnorderedListEnding()
            => EndElement();

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected internal override void VisitOrderedListBeginning()
            => BeginElement("ol");

        /// <summary>Visits the ending of an ordered list.</summary>
        protected internal override void VisitOrderedListEnding()
            => EndElement();

        /// <summary>Visits the beginning of a list item.</summary>
        protected internal override void VisitListItemBeginning()
            => BeginElement("li");

        /// <summary>Visits the ending of a list item.</summary>
        protected internal override void VisitListItemEnding()
        {
            if (StringComparer.OrdinalIgnoreCase.Equals("ul", _previousElement) || StringComparer.OrdinalIgnoreCase.Equals("ol", _previousElement))
                EndElement();
            else
                EndElementWithoutIndent();
        }

        /// <summary>Visits a plugin.</summary>
        /// <param name="text">The plugin text.</param>
        protected internal override void VisitPlugin(string text)
            => WriteComment($" {text} ");

        /// <summary>Visits the beginning of a strong element.</summary>
        protected internal override void VisitStrongBeginning()
            => BeginElementWithoutIndent("strong");

        /// <summary>Visits the ending of a strong element.</summary>
        protected internal override void VisitStrongEnding()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected internal override void VisitEmphasisBeginning()
            => BeginElementWithoutIndent("em");

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected internal override void VisitEmphasisEnding()
            => EndElementWithoutIndent();

        /// <summary>Visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        protected internal override void VisitHyperlinkBeginning(string destination)
        {
            BeginElementWithoutIndent("a");
            WriteAttribute("href", destination);
        }

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected internal override void VisitHyperlinkEnding()
            => EndElementWithoutIndent();

        /// <summary>Visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternativeText">The alternative text for the image.</param>
        protected internal override void VisitImage(string source, string alternativeText)
        {
            BeginElementWithoutIndent("img");
            WriteAttribute("src", source);
            if (!IsNullOrWhiteSpace(alternativeText))
                WriteAttribute("alt", alternativeText);
            EndElement();
        }

        /// <summary>Visits a line break.</summary>
        protected internal override void VisitLineBreak()
        {
            BeginElementWithoutIndent("br");
            EndElement();
        }

        /// <summary>Visits a code fragment inside a block (e.g.: paragraph, list item or table).</summary>
        /// <param name="fragment">The preformatted text.</param>
        protected internal override void VisitCodeFragment(string fragment)
        {
            BeginElementWithoutIndent("code");
            Write(fragment);
            EndElementWithoutIndent();
        }

        /// <summary>Visits a horizontal rule.</summary>
        protected internal override void VisitHorizontalRule()
        {
            BeginElement("hr");
            EndElement();
        }

        /// <summary>Visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        protected internal override void VisitText(string text)
            => Write(text);

        /// <summary>Begins an HTML element.</summary>
        /// <param name="elementName">The name of the HTML element.</param>
        protected void BeginElement(string elementName)
        {
            _CompleteElementBeginning();
            _Indent();

            _htmlStringBuilder.Append('<');
            _AppendHtmlSafe(elementName);
            _elementEnded = false;
            _openElements.Push(elementName);
            _previousElement = elementName;
        }

        /// <summary>Begins an HTML element without indentation.</summary>
        /// <param name="elementName">The name of the HTML element.</param>
        protected void BeginElementWithoutIndent(string elementName)
        {
            _CompleteElementBeginning();

            _htmlStringBuilder.Append('<');
            _AppendHtmlSafe(elementName);
            _elementEnded = false;
            _openElements.Push(elementName);
            _previousElement = elementName;
        }

        /// <summary>Ends a previously started HTML element.</summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when there was is no HTML element to end.
        /// Either all of them were previously ended or none were started.
        /// </exception>
        protected void EndElement()
        {
            if (_openElements.Count == 0)
                throw new InvalidOperationException("There are no HTML elements to end.");

            var elementName = _openElements.Pop();
            if (!_elementEnded)
            {
                if (_IsVoidElement(elementName))
                    _htmlStringBuilder.Append(">");
                else
                {
                    _htmlStringBuilder.Append("></");
                    _AppendHtmlSafe(elementName);
                    _htmlStringBuilder.Append('>');
                }
                _elementEnded = true;
            }
            else
            {
                _Indent();

                _htmlStringBuilder.Append("</");
                _AppendHtmlSafe(elementName);
                _htmlStringBuilder.Append('>');
            }
            _previousElement = elementName;
        }

        /// <summary>Ends a previously started HTML element without indentation.</summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when there was is no HTML element to end.
        /// Either all of them were previously ended or none were started.
        /// </exception>
        protected void EndElementWithoutIndent()
        {
            if (_openElements.Count == 0)
                throw new InvalidOperationException("There are no HTML elements to end.");

            var elementName = _openElements.Pop();
            if (!_elementEnded)
            {
                if (_IsVoidElement(elementName))
                    _htmlStringBuilder.Append(">");
                else
                {
                    _htmlStringBuilder.Append("></");
                    _AppendHtmlSafe(elementName);
                    _htmlStringBuilder.Append('>');
                }
                _elementEnded = true;
            }
            else
            {
                _htmlStringBuilder.Append("</");
                _AppendHtmlSafe(elementName);
                _htmlStringBuilder.Append('>');
            }
            _previousElement = elementName;
        }

        /// <summary>Writes an HTML attribute to the result.</summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when content has been written inside the HTML element or there is no HTML element started.
        /// </exception>
        protected void WriteAttribute(string attributeName)
        {
            if (_openElements.Count == 0)
                throw new InvalidOperationException("There are no elements started.");
            if (_elementEnded)
                throw new InvalidOperationException("Element content has already been written.");

            _htmlStringBuilder.Append(' ');
            _AppendHtmlSafe(attributeName);
        }

        /// <summary>Writes an HTML attribute to the result.</summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value of the attribute.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when content has been written inside the HTML element or there is no HTML element started.
        /// </exception>
        protected void WriteAttribute(string attributeName, string attributeValue)
        {
            if (_openElements.Count == 0)
                throw new InvalidOperationException("There are no elements started.");
            if (_elementEnded)
                throw new InvalidOperationException("Element content has already been written.");

            _htmlStringBuilder.Append(' ');
            _AppendHtmlSafe(attributeName);
            _htmlStringBuilder.Append("=\"");
            _AppendHtmlSafe(attributeValue);
            _htmlStringBuilder.Append('"');
        }

        /// <summary>Writes an HTML comment containing the given <paramref name="text"/> to the result. Encoding is done for special characters only.</summary>
        /// <param name="text">The text to write in the comment.</param>
        protected void WriteComment(string text)
        {
            _CompleteElementBeginning();
            if (_openElements.Count == 0)
                _Indent();

            _htmlStringBuilder.Append("<!--");
            _AppendHtmlSafe(text);
            _htmlStringBuilder.Append("-->");
        }

        /// <summary>Writes the given <paramref name="text"/> to the result. Encoding is done for special characters only.</summary>
        /// <param name="text">The text to write.</param>
        protected void Write(string text)
        {
            if (_openElements.Count > 0 && _IsVoidElement(_openElements.Peek()))
                throw new InvalidOperationException("The current element is a void HTML element. It cannot have any content.");

            _CompleteElementBeginning();
            _AppendHtmlSafe(text);
        }

        /// <summary>Writes the given <paramref name="character"/> to the result. Encoding is done for special characters only.</summary>
        /// <param name="character">The character to write.</param>
        protected void Write(char character)
        {
            if (_openElements.Count > 0 && _IsVoidElement(_openElements.Peek()))
                throw new InvalidOperationException("The current element is a void HTML element. It cannot have any content.");

            _CompleteElementBeginning();
            _AppendHtmlSafe(character);
        }

        /// <summary>Appends the HTML encoded <paramref name="text"/>. Encoding is done only for special characters.</summary>
        /// <param name="text">The text to append to <see cref="HtmlStringBuilder"/>.</param>
        [Obsolete("The use of this method is discouraged and will be removed in Mup 2.0. Please use the Write methods instead.")]
        protected void AppendHtmlSafe(string text)
        {
            foreach (var character in text)
                AppendHtmlSafe(character);
        }

        /// <summary>Appends the HTML encoded <paramref name="character"/>. Encoding is done only for special characters.</summary>
        /// <param name="character">The character to append to <see cref="HtmlStringBuilder"/>.</param>
        [Obsolete("The use of this method is discouraged and will be removed in Mup 2.0. Please use the Write methods instead.")]
        protected void AppendHtmlSafe(char character)
        {
            switch (character)
            {
                case '&':
                    HtmlStringBuilder.Append("&amp;");
                    break;

                case '<':
                    HtmlStringBuilder.Append("&lt;");
                    break;

                case '>':
                    HtmlStringBuilder.Append("&gt;");
                    break;

                case '"':
                    HtmlStringBuilder.Append("&quot;");
                    break;

                case '\'':
                    HtmlStringBuilder.Append("&#39;");
                    break;

                default:
                    HtmlStringBuilder.Append(character);
                    break;
            }
        }

        private void _AppendHtmlSafe(string text)
        {
            foreach (var character in text)
                _AppendHtmlSafe(character);
        }

        private void _Indent()
        {
            if (Options.Indent != null)
            {
                if (_htmlStringBuilder.Length > 0)
                    _htmlStringBuilder.Append(Options.NewLine ?? Environment.NewLine);
                for (var indentCount = 0; indentCount < Options.IndentOffset + _openElements.Count; indentCount++)
                    _htmlStringBuilder.Append(Options.Indent);
            }
            else if (Options.NewLine != null && _htmlStringBuilder.Length > 0)
                _htmlStringBuilder.Append(Options.NewLine);
        }

        private void _CompleteElementBeginning()
        {
            if (!_elementEnded)
            {
                _htmlStringBuilder.Append('>');
                _elementEnded = true;
            }
        }

        private void _AppendHtmlSafe(char character)
        {
            switch (character)
            {
                case '&':
                    _htmlStringBuilder.Append("&amp;");
                    break;

                case '<':
                    _htmlStringBuilder.Append("&lt;");
                    break;

                case '>':
                    _htmlStringBuilder.Append("&gt;");
                    break;

                case '"':
                    _htmlStringBuilder.Append("&quot;");
                    break;

                case '\'':
                    _htmlStringBuilder.Append("&#39;");
                    break;

                default:
                    _htmlStringBuilder.Append(character);
                    break;
            }
        }
    }
}