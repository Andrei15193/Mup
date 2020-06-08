namespace Mup
{
    /// <summary>Specifies options for an <see cref="HtmlWriterVisitor"/>.</summary>
    public class HtmlWriterVisitorOptions
    {
        /// <summary>Initializes a new instance of the <see cref="HtmlWriterVisitorOptions"/> class.</summary>
        public HtmlWriterVisitorOptions()
        {
        }

        /// <summary>
        /// When provided, the given sequence is used to indent new elements.
        /// If the value is <c>null</c> then no indentation is done and elements
        /// will not begin on new lines unless a <see cref="NewLine"/> is specified.</summary>
        public string Indent { get; set; }

        /// <summary>
        /// The offset of the indentation. In case the HTML that is being written by the visitor is contained in an element
        /// a global indent can be applied to all of them allowing the resulting HTML to be formatted accordingly.
        /// </summary>
        public int IndentOffset { get; set; }

        /// <summary>
        /// When provided, the given sequence is used for breaking elements on separate lines otherwise no new line is used
        /// and implicitly no indentantion is applied.
        /// </summary>
        public string NewLine { get; set; }
    }
}