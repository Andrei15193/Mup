using Mup.Elements;

namespace Mup
{
    /// <summary>Base class of all parse tree visitors.</summary>
    public abstract class ParseTreeVisitor
    {
        /// <summary>Initializes a new instance of the <see cref="ParseTreeVisitor" /> class.</summary>
        protected ParseTreeVisitor()
        {
        }

        /// <summary>Visits the provided <paramref name="rootElement"/>.</summary>
        /// <param name="rootElement">The <see cref="ParseTreeRootElement"/> to visit.</param>
        protected internal abstract void Visit(ParseTreeRootElement rootElement);

        /// <summary>Visits the provided <paramref name="heading1"/> element.</summary>
        /// <param name="heading1">The <see cref="Heading1Element"/> to visit.</param>
        protected internal abstract void Visit(Heading1Element heading1);

        /// <summary>Visits the provided <paramref name="heading2"/> element.</summary>
        /// <param name="heading2">The <see cref="Heading2Element"/> to visit.</param>
        protected internal abstract void Visit(Heading2Element heading2);

        /// <summary>Visits the provided <paramref name="heading3"/> element.</summary>
        /// <param name="heading3">The <see cref="Heading3Element"/> to visit.</param>
        protected internal abstract void Visit(Heading3Element heading3);

        /// <summary>Visits the provided <paramref name="heading4"/> element.</summary>
        /// <param name="heading4">The <see cref="Heading4Element"/> to visit.</param>
        protected internal abstract void Visit(Heading4Element heading4);

        /// <summary>Visits the provided <paramref name="heading5"/> element.</summary>
        /// <param name="heading5">The <see cref="Heading5Element"/> to visit.</param>
        protected internal abstract void Visit(Heading5Element heading5);

        /// <summary>Visits the provided <paramref name="heading6"/> element.</summary>
        /// <param name="heading6">The <see cref="Heading6Element"/> to visit.</param>
        protected internal abstract void Visit(Heading6Element heading6);

        /// <summary>Visits the provided <paramref name="paragraph"/> element.</summary>
        /// <param name="paragraph">The <see cref="ParagraphElement"/> to visit.</param>
        protected internal abstract void Visit(ParagraphElement paragraph);

        /// <summary>Visits the provided <paramref name="unorderedListElement"/> element.</summary>
        /// <param name="unorderedListElement">The <see cref="UnorderedListElement"/> to visit.</param>
        protected internal abstract void Visit(UnorderedListElement unorderedListElement);

        /// <summary>Visits the provided <paramref name="orderedList"/> element.</summary>
        /// <param name="orderedList">The <see cref="OrderedListElement"/> to visit.</param>
        protected internal abstract void Visit(OrderedListElement orderedList);

        /// <summary>Visits the provided <paramref name="listItem"/> element.</summary>
        /// <param name="listItem">The <see cref="ListItemElement"/> to visit.</param>
        protected internal abstract void Visit(ListItemElement listItem);

        /// <summary>Visits the provided <paramref name="tableElement"/> element.</summary>
        /// <param name="tableElement">The <see cref="TableElement"/> to visit.</param>
        protected internal abstract void Visit(TableElement tableElement);

        /// <summary>Visits the provided <paramref name="tableRow"/> element.</summary>
        /// <param name="tableRow">The <see cref="TableRowElement"/> to visit.</param>
        protected internal abstract void Visit(TableRowElement tableRow);

        /// <summary>Visits the provided <paramref name="tableHeaderCell"/> element.</summary>
        /// <param name="tableHeaderCell">The <see cref="TableHeaderCellElement"/> to visit.</param>
        protected internal abstract void Visit(TableHeaderCellElement tableHeaderCell);

        /// <summary>Visits the provided <paramref name="tableDataCell"/> element.</summary>
        /// <param name="tableDataCell">The <see cref="TableDataCellElement"/> to visit.</param>
        protected internal abstract void Visit(TableDataCellElement tableDataCell);

        /// <summary>Visits the provided <paramref name="text"/> element.</summary>
        /// <param name="text">The <see cref="TextElement"/> to visit.</param>
        protected internal abstract void Visit(TextElement text);

        /// <summary>Visits the provided <paramref name="emphasis"/> element.</summary>
        /// <param name="emphasis">The <see cref="EmphasisElement"/> to visit.</param>
        protected internal abstract void Visit(EmphasisElement emphasis);

        /// <summary>Visits the provided <paramref name="strong"/> element.</summary>
        /// <param name="strong">The <see cref="StrongElement"/> to visit.</param>
        protected internal abstract void Visit(StrongElement strong);

        /// <summary>Visits the provided <paramref name="hyperlink"/> element.</summary>
        /// <param name="hyperlink">The <see cref="HyperlinkElement"/> to visit.</param>
        protected internal abstract void Visit(HyperlinkElement hyperlink);

        /// <summary>Visits the provided <paramref name="image"/> element.</summary>
        /// <param name="image">The <see cref="ImageElement"/> to visit.</param>
        protected internal abstract void Visit(ImageElement image);

        /// <summary>Visits the provided <paramref name="code"/> element.</summary>
        /// <param name="code">The <see cref="CodeElement"/> to visit.</param>
        protected internal abstract void Visit(CodeElement code);

        /// <summary>Visits the provided <paramref name="lineBreak"/> element.</summary>
        /// <param name="lineBreak">The <see cref="LineBreakElement"/> to visit.</param>
        protected internal abstract void Visit(LineBreakElement lineBreak);

        /// <summary>Visits the provided <paramref name="preformattedBlock"/> element.</summary>
        /// <param name="preformattedBlock">The <see cref="PreformattedBlockElement"/> to visit.</param>
        protected internal abstract void Visit(PreformattedBlockElement preformattedBlock);

        /// <summary>Visits the provided <paramref name="horizontalRule"/> element.</summary>
        /// <param name="horizontalRule">The <see cref="HorizontalRuleElement"/> to visit.</param>
        protected internal abstract void Visit(HorizontalRuleElement horizontalRule);

        /// <summary>Visits the provided <paramref name="plugin"/> element.</summary>
        /// <param name="plugin">The <see cref="PluginElement"/> to visit.</param>
        protected internal abstract void Visit(PluginElement plugin);
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