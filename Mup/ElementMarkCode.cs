namespace Mup
{
    internal enum ElementMarkCode
    {
        PlainText,

        HorizontalLine,

        HyperlinkStart,
        HyperlinkDestination,
        HyperlinkTextSeparator,
        HyperlinkEnd,

        StrongStart,
        StrongEnd,

        EmphasisStart,
        EmphasisEnd,

        ImageStart,
        ImageSource,
        ImageTitleSeparator,
        ImageEnd,

        LineBreak,

        NoWikiStart,
        NoWikiEnd,

        Heading1Start,
        Heading1End,

        Heading2Start,
        Heading2End,

        Heading3Start,
        Heading3End,

        Heading4Start,
        Heading4End,

        Heading5Start,
        Heading5End,

        Heading6Start,
        Heading6End,

        ParagraphStart,
        ParagraphEnd,

        TableStart,
        TableRowStart,
        TableHeaderCellStart,
        TableHeaderCellEnd,
        TableCellStart,
        TableCellEnd,
        TableCellSeparator,
        TableRowEnd,
        TableEnd,

        BulletListStart,
        BulletListItemStart,
        BulletListItemEnd,
        BulletListEnd,

        OrderedListStart,
        OrderedListItemStart,
        OrderedListItemEnd,
        OrderedListEnd,

        PluginStart,
        PluginText,
        PluginEnd
    }
}