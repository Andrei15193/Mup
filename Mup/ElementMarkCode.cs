namespace Mup
{
    internal enum ElementMarkCode
    {
        PlainText,

        HorizontalRule,

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

        PreformattedStart,
        PreformattedEnd,

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

        PreformattedBlockStart,
        PreformattedBlockEnd,

        TableStart,
        TableRowStart,
        TableHeaderCellStart,
        TableHeaderCellEnd,
        TableCellStart,
        TableCellEnd,
        TableRowEnd,
        TableEnd,

        UnorderedListStart,
        UnorderedListEnd,

        OrderedListStart,
        OrderedListEnd,

        ListItemStart,
        ListItemEnd,

        PluginStart,
        PluginEnd
    }
}