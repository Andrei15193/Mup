namespace Mup.Creole.ElementProcessors.RichText
{
    internal class CreoleRichTextElementData
    {
        internal CreoleRichTextElementData(CreoleRichTextElementType elementType, int startIndex, int endIndex, int contentStartIndex, int contentEndIndex)
            : this(elementType, startIndex, endIndex, endIndex, endIndex, contentStartIndex, contentEndIndex)
        {
        }

        internal CreoleRichTextElementData(CreoleRichTextElementType elementType, int startIndex, int endIndex)
            : this(elementType, startIndex, endIndex, endIndex, endIndex, endIndex, endIndex)
        {
        }

        internal CreoleRichTextElementData(CreoleRichTextElementType elementType, int startIndex, int endIndex, int urlStartIndex, int urlEndIndex, int contentStartIndex, int contentEndIndex)
        {
            ElementType = elementType;
            StartIndex = startIndex;
            EndIndex = endIndex;
            UrlStartIndex = urlStartIndex;
            UrlEndIndex = urlEndIndex;
            ContentStartIndex = contentStartIndex;
            ContentEndIndex = contentEndIndex;
        }

        internal CreoleRichTextElementType ElementType { get; }

        internal int StartIndex { get; }

        internal int EndIndex { get; }

        internal int UrlStartIndex { get; }

        internal int UrlEndIndex { get; }

        internal int ContentStartIndex { get; }

        internal int ContentEndIndex { get; }
    }
}