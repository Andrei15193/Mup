namespace Mup
{
    /// <summary>Base class of all parse tree visitors.</summary>
    public abstract class ParseTreeVisitor
    {
        /// <summary>Initializes a new instance of the <see cref="ParseTreeVisitor" /> class.</summary>
        protected ParseTreeVisitor()
        {
        }

        /// <summary>Initializes the visitor. This method is called before any visit method.</summary>
        protected internal virtual void BeginVisit()
        {
        }

        /// <summary>Completes the visit operation. This method is called after all visit methods.</summary>
        protected internal virtual void EndVisit()
        {
        }

        /// <summary>Visits the beginning of a level 1 heading.</summary>
        protected internal virtual void VisitHeading1Beginning()
        {
        }

        /// <summary>Visits the ending of a level 1 heading.</summary>
        protected internal virtual void VisitHeading1Ending()
        {
        }

        /// <summary>Visits the beginning of a level 2 heading.</summary>
        protected internal virtual void VisitHeading2Beginning()
        {
        }

        /// <summary>Visits the ending of a level 2 heading.</summary>
        protected internal virtual void VisitHeading2Ending()
        {
        }

        /// <summary>Visits the beginning of a level 3 heading.</summary>
        protected internal virtual void VisitHeading3Beginning()
        {
        }

        /// <summary>Visits the ending of a level 3 heading.</summary>
        protected internal virtual void VisitHeading3Ending()
        {
        }

        /// <summary>Visits the beginning of a level 4 heading.</summary>
        protected internal virtual void VisitHeading4Beginning()
        {
        }

        /// <summary>Visits the ending of a level 4 heading.</summary>
        protected internal virtual void VisitHeading4Ending()
        {
        }

        /// <summary>Visits the beginning of a level 5 heading.</summary>
        protected internal virtual void VisitHeading5Beginning()
        {
        }

        /// <summary>Visits the ending of a level 5 heading.</summary>
        protected internal virtual void VisitHeading5Ending()
        {
        }

        /// <summary>Visits the beginning of a level 6 heading.</summary>
        protected internal virtual void VisitHeading6Beginning()
        {
        }

        /// <summary>Visits the ending of a level 6 heading.</summary>
        protected internal virtual void VisitHeading6Ending()
        {
        }

        /// <summary>Visits the beginning of a paragraph.</summary>
        protected internal virtual void VisitParagraphBeginning()
        {
        }

        /// <summary>Visits the ending of a paragraph.</summary>
        protected internal virtual void VisitParagraphEnding()
        {
        }

        /// <summary>Visits a preformatted block.</summary>
        /// <param name="preformattedText">The preformatted text.</param>
        protected internal virtual void VisitPreformattedBlock(string preformattedText)
        {
        }

        /// <summary>Visits a horizontal rule.</summary>
        protected internal virtual void VisitHorizontalRule()
        {
        }

        /// <summary>Visits a plugin.</summary>
        /// <param name="text">The plugin text.</param>
        protected internal virtual void VisitPlugin(string text)
        {
        }

        /// <summary>Visits the beginning of a table.</summary>
        protected internal virtual void VisitTableBeginning()
        {
        }

        /// <summary>Visits the ending of a table.</summary>
        protected internal virtual void VisitTableEnding()
        {
        }

        /// <summary>Visits the beginning of a table row.</summary>
        protected internal virtual void VisitTableRowBeginning()
        {
        }

        /// <summary>Visits the ending of a table row.</summary>
        protected internal virtual void VisitTableRowEnding()
        {
        }

        /// <summary>Visits the beginning of a table header cell.</summary>
        protected internal virtual void VisitTableHeaderCellBeginning()
        {
        }

        /// <summary>Visits the ending of a table header cell.</summary>
        protected internal virtual void VisitTableHeaderCellEnding()
        {
        }

        /// <summary>Visits the beginning of a table cell.</summary>
        protected internal virtual void VisitTableCellBeginning()
        {
        }

        /// <summary>Visits the ending of a table cell.</summary>
        protected internal virtual void VisitTableCellEnding()
        {
        }

        /// <summary>Visits the beginning of an unordered list.</summary>
        protected internal virtual void VisitUnorderedListBeginning()
        {
        }

        /// <summary>Visits the ending of an unordered list.</summary>
        protected internal virtual void VisitUnorderedListEnding()
        {
        }

        /// <summary>Visits the beginning of an ordered list.</summary>
        protected internal virtual void VisitOrderedListBeginning()
        {
        }

        /// <summary>Visits the ending of an ordered list.</summary>
        protected internal virtual void VisitOrderedListEnding()
        {
        }

        /// <summary>Visits the beginning of a list item.</summary>
        protected internal virtual void VisitListItemBeginning()
        {
        }

        /// <summary>Visits the ending of a list item.</summary>
        protected internal virtual void VisitListItemEnding()
        {
        }

        /// <summary>Visits the beginning of a strong element.</summary>
        protected internal virtual void VisitStrongBeginning()
        {
        }

        /// <summary>Visits the ending of a strong element.</summary>
        protected internal virtual void VisitStrongEnding()
        {
        }

        /// <summary>Visits the beginning of an emphasised element.</summary>
        protected internal virtual void VisitEmphasisBeginning()
        {
        }

        /// <summary>Visits the ending of an emphasised element.</summary>
        protected internal virtual void VisitEmphasisEnding()
        {
        }

        /// <summary>Visits the beginning of a hyperlink.</summary>
        /// <param name="destination">The hyperlink destination.</param>
        protected internal virtual void VisitHyperlinkBeginning(string destination)
        {
        }

        /// <summary>Visits the ending of a hyperlink.</summary>
        protected internal virtual void VisitHyperlinkEnding()
        {
        }

        /// <summary>Visits an image.</summary>
        /// <param name="source">The source of the image.</param>
        /// <param name="alternativeText">The alternative text for the image.</param>
        protected internal virtual void VisitImage(string source, string alternativeText)
        {
        }

        /// <summary>Visits a line break.</summary>
        protected internal virtual void VisitLineBreak()
        {
        }

        /// <summary>Visits a code fragment inside a block (e.g.: paragraph, list item or table).</summary>
        /// <param name="fragment">The code fragment.</param>
        protected internal virtual void VisitCodeFragment(string fragment)
        {
        }

        /// <summary>Visits plain text. This method may be called multiple times consecutively.</summary>
        /// <param name="text">The plain text.</param>
        protected internal virtual void VisitText(string text)
        {
        }
    }

    /// <summary>Base class for all parse tree visitors that eventually provide a result that is stored in
    /// memory (e.g. a <see cref="string"/> or a <see cref="System.IO.MemoryStream"/>).</summary>
    /// <typeparam name="TResult">The type which is constructed after visitng a parse tree.</typeparam>
    public abstract class ParseTreeVisitor<TResult>
        : ParseTreeVisitor
    {
        /// <summary>Initializes a new instance of the <see cref="ParseTreeVisitor{TResult}" /> class.</summary>
        protected ParseTreeVisitor()
        {
        }

        /// <summary>Gets the visitor result. This method is called only after the visit operation completes.</summary>
        /// <returns>Returns the result after the entire parse tree has been visited.</returns>
        protected internal abstract TResult GetResult();
    }
}