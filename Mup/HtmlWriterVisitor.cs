using Mup.Elements;
using System;
using System.Collections.Generic;
using System.Text;
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

        /// <summary>Gets the <see cref="HtmlWriterVisitorOptions"/> to use when writing HTML to the result.</summary>
        protected HtmlWriterVisitorOptions Options { get; }

        /// <summary>Gets the visitor result. This method is called only after the visit operation completes.</summary>
        /// <returns>Returns the result after the entire parse tree has been visited.</returns>
        protected internal override string GetResult()
            => _result = _result ?? _htmlStringBuilder.ToString();

        /// <summary>Visits the provided <paramref name="rootElement"/>.</summary>
        /// <param name="rootElement">The <see cref="ParseTreeRootElement"/> to visit.</param>
        protected internal override void Visit(ParseTreeRootElement rootElement)
        {
            _result = null;
            _elementEnded = true;
            _openElements.Clear();

            foreach (var element in rootElement.Elements)
                element.Accept(this);

            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
            _result = _htmlStringBuilder.ToString();
            _htmlStringBuilder = null;
        }

        /// <summary>Visits the provided <paramref name="heading1"/> element.</summary>
        /// <param name="heading1">The <see cref="Heading1Element"/> to visit.</param>
        protected internal override void Visit(Heading1Element heading1)
        {
            BeginElement("h1");
            Write(heading1.Text);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="heading2"/> element.</summary>
        /// <param name="heading2">The <see cref="Heading2Element"/> to visit.</param>
        protected internal override void Visit(Heading2Element heading2)
        {
            BeginElement("h2");
            Write(heading2.Text);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="heading3"/> element.</summary>
        /// <param name="heading3">The <see cref="Heading3Element"/> to visit.</param>
        protected internal override void Visit(Heading3Element heading3)
        {
            BeginElement("h3");
            Write(heading3.Text);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="heading4"/> element.</summary>
        /// <param name="heading4">The <see cref="Heading4Element"/> to visit.</param>
        protected internal override void Visit(Heading4Element heading4)
        {
            BeginElement("h4");
            Write(heading4.Text);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="heading5"/> element.</summary>
        /// <param name="heading5">The <see cref="Heading5Element"/> to visit.</param>
        protected internal override void Visit(Heading5Element heading5)
        {
            BeginElement("h5");
            Write(heading5.Text);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="heading6"/> element.</summary>
        /// <param name="heading6">The <see cref="Heading6Element"/> to visit.</param>
        protected internal override void Visit(Heading6Element heading6)
        {
            BeginElement("h6");
            Write(heading6.Text);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="paragraph"/> element.</summary>
        /// <param name="paragraph">The <see cref="ParagraphElement"/> to visit.</param>
        protected internal override void Visit(ParagraphElement paragraph)
        {
            BeginElement("p");
            foreach (var element in paragraph.Content)
                element.Accept(this);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="unorderedListElement"/> element.</summary>
        /// <param name="unorderedListElement">The <see cref="UnorderedListElement"/> to visit.</param>
        protected internal override void Visit(UnorderedListElement unorderedListElement)
        {
            BeginElement("ul");
            foreach (var item in unorderedListElement.Items)
                item.Accept(this);
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="orderedList"/> element.</summary>
        /// <param name="orderedList">The <see cref="OrderedListElement"/> to visit.</param>
        protected internal override void Visit(OrderedListElement orderedList)
        {
            BeginElement("ol");
            foreach (var item in orderedList.Items)
                item.Accept(this);
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="listItem"/> element.</summary>
        /// <param name="listItem">The <see cref="ListItemElement"/> to visit.</param>
        protected internal override void Visit(ListItemElement listItem)
        {
            BeginElement("li");
            foreach (var element in listItem.Content)
                element.Accept(this);
            if (StringComparer.OrdinalIgnoreCase.Equals("ul", _previousElement) || StringComparer.OrdinalIgnoreCase.Equals("ol", _previousElement))
                EndElement();
            else
                EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="tableElement"/> element.</summary>
        /// <param name="tableElement">The <see cref="TableElement"/> to visit.</param>
        protected internal override void Visit(TableElement tableElement)
        {
            BeginElement("table");
            BeginElement("tbody");
            foreach (var row in tableElement.Rows)
                row.Accept(this);
            EndElement();
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="tableRow"/> element.</summary>
        /// <param name="tableRow">The <see cref="TableRowElement"/> to visit.</param>
        protected internal override void Visit(TableRowElement tableRow)
        {
            BeginElement("tr");
            foreach (var cell in tableRow.Cells)
                cell.Accept(this);
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="tableHeaderCell"/> element.</summary>
        /// <param name="tableHeaderCell">The <see cref="TableHeaderCellElement"/> to visit.</param>
        protected internal override void Visit(TableHeaderCellElement tableHeaderCell)
        {
            BeginElement("th");
            foreach (var element in tableHeaderCell.Content)
                element.Accept(this);
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="tableDataCell"/> element.</summary>
        /// <param name="tableDataCell">The <see cref="TableDataCellElement"/> to visit.</param>
        protected internal override void Visit(TableDataCellElement tableDataCell)
        {
            BeginElement("td");
            foreach (var element in tableDataCell.Content)
                element.Accept(this);
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="text"/> element.</summary>
        /// <param name="text">The <see cref="TextElement"/> to visit.</param>
        protected internal override void Visit(TextElement text)
        {
            Write(text.Text);
        }

        /// <summary>Visits the provided <paramref name="emphasis"/> element.</summary>
        /// <param name="emphasis">The <see cref="EmphasisElement"/> to visit.</param>
        protected internal override void Visit(EmphasisElement emphasis)
        {
            BeginElementWithoutIndent("em");
            foreach (var element in emphasis.Content)
                element.Accept(this);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="strong"/> element.</summary>
        /// <param name="strong">The <see cref="StrongElement"/> to visit.</param>
        protected internal override void Visit(StrongElement strong)
        {
            BeginElementWithoutIndent("strong");
            foreach (var element in strong.Content)
                element.Accept(this);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="hyperlink"/> element.</summary>
        /// <param name="hyperlink">The <see cref="HyperlinkElement"/> to visit.</param>
        protected internal override void Visit(HyperlinkElement hyperlink)
        {
            BeginElementWithoutIndent("a");
            WriteAttribute("href", hyperlink.Destination);
            foreach (var element in hyperlink.Content)
                element.Accept(this);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="image"/> element.</summary>
        /// <param name="image">The <see cref="ImageElement"/> to visit.</param>
        protected internal override void Visit(ImageElement image)
        {
            BeginElementWithoutIndent("img");
            WriteAttribute("src", image.Source);
            if (!IsNullOrWhiteSpace(image.AlternativeText))
                WriteAttribute("alt", image.AlternativeText);
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="code"/> element.</summary>
        /// <param name="code">The <see cref="CodeElement"/> to visit.</param>
        protected internal override void Visit(CodeElement code)
        {
            BeginElementWithoutIndent("code");
            Write(code.Code);
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="lineBreak"/> element.</summary>
        /// <param name="lineBreak">The <see cref="LineBreakElement"/> to visit.</param>
        protected internal override void Visit(LineBreakElement lineBreak)
        {
            BeginElementWithoutIndent("br");
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="preformattedBlock"/> element.</summary>
        /// <param name="preformattedBlock">The <see cref="PreformattedBlockElement"/> to visit.</param>
        protected internal override void Visit(PreformattedBlockElement preformattedBlock)
        {
            BeginElement("pre");
            BeginElementWithoutIndent("code");
            Write(preformattedBlock.PreformattedText);
            EndElementWithoutIndent();
            EndElementWithoutIndent();
        }

        /// <summary>Visits the provided <paramref name="horizontalRule"/> element.</summary>
        /// <param name="horizontalRule">The <see cref="HorizontalRuleElement"/> to visit.</param>
        protected internal override void Visit(HorizontalRuleElement horizontalRule)
        {
            BeginElement("hr");
            EndElement();
        }

        /// <summary>Visits the provided <paramref name="plugin"/> element.</summary>
        /// <param name="plugin">The <see cref="PluginElement"/> to visit.</param>
        protected internal override void Visit(PluginElement plugin)
        {
            WriteComment($" {plugin.PluginText} ");
        }

        /// <summary>Begins an HTML element.</summary>
        /// <param name="elementName">The name of the HTML element.</param>
        protected void BeginElement(string elementName)
        {
            _CompleteElementBeginning();
            _Indent();

            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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

            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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

            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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

            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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

            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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

            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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

            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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

        private void _AppendHtmlSafe(string text)
        {
            foreach (var character in text)
                _AppendHtmlSafe(character);
        }

        private void _Indent()
        {
            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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
                _result = null;
                _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
                _htmlStringBuilder.Append('>');
                _elementEnded = true;
            }
        }

        private void _AppendHtmlSafe(char character)
        {
            _result = null;
            _htmlStringBuilder = _htmlStringBuilder ?? _wrappedBuilder ?? new StringBuilder();
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