using System.Collections.Generic;
using System.Linq;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleListElementFactory : CreoleRichTextBlockElementFactory
    {
        internal CreoleListElementFactory(string text, IEnumerable<string> inlineHyperlinkProtocols)
            : base(text, inlineHyperlinkProtocols)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
        {
            CreoleFactoryResult result = null;

            if ((token.Code == Asterisk || token.Code == Hash) && token.Next != null && token.Next.Code != token.Code)
            {
                var start = token;
                var listElementInfos = new Stack<ListElementInfo>();
                var isValidList = true;

                while (isValidList)
                {
                    var listLevel = 0;
                    var itemStart = token;
                    var isOrderedList = (itemStart.Code == Hash);

                    while (token.Next != null && token.Code == itemStart.Code)
                    {
                        listLevel++;
                        token = token.Next;
                    }

                    if (listLevel > listElementInfos.Count + 1)
                    {
                        isValidList = false;
                        do
                            token = token.Previous;
                        while (token.Code != WhiteSpace);
                    }
                    else
                    {
                        if (listLevel > listElementInfos.Count)
                            listElementInfos.Push(new ListElementInfo(isOrderedList));
                        else
                            while (listElementInfos.Count > listLevel)
                            {
                                var listElement = _CreateListElement(listElementInfos.Pop());
                                listElementInfos.Peek().ItemInfos.Last().Content.Add(listElement);
                            }

                        if (listElementInfos.Peek().IsOrdered != isOrderedList)
                        {
                            isValidList = false;
                            token = itemStart.Previous;
                        }
                        else
                        {
                            var contentStart = token;

                            while (token.Next != null && !ContainsLineFeed(token))
                                token = token.Next;

                            listElementInfos.Peek().ItemInfos.Add(new ListItemElementInfo(_GetListItemContent(contentStart, token)));

                            if (token.Next != null && (token.Next.Code == Asterisk || token.Next.Code == Hash) && !FindLineFeeds(token).Skip(1).Any())
                            {
                                token = token.Next;
                                isValidList = true;
                            }
                            else
                                isValidList = false;
                        }
                    }
                }

                while (listElementInfos.Count > 1)
                {
                    var listElement = _CreateListElement(listElementInfos.Pop());
                    listElementInfos.Peek().ItemInfos.Last().Content.Add(listElement);
                }

                result = new CreoleFactoryResult(start, token, _CreateListElement(listElementInfos.Pop()));
            }

            return result;
        }

        private IEnumerable<CreoleElement> _GetListItemContent(CreoleToken start, CreoleToken end)
        {
            if (start.Code == WhiteSpace)
                start = start.Next;
            if (end.Code == WhiteSpace)
                end = end.Previous;

            if (start.StartIndex > end.StartIndex)
                return EmptyContent;
            else
                return CreateRichTextElementsFrom(start, end);
        }

        private CreoleListElement _CreateListElement(ListElementInfo listElementInfo)
        {
            CreoleListElement listElement;

            var items = _GetListItems(listElementInfo.ItemInfos);
            if (listElementInfo.IsOrdered)
                listElement = new CreoleOrderedListElement(items);
            else
                listElement = new CreoleUnorderedListElement(items);

            return listElement;
        }

        private IEnumerable<CreoleListItemElement> _GetListItems(IEnumerable<ListItemElementInfo> listItemsInfos)
        {
            var listItems = new List<CreoleListItemElement>();
            foreach (var listItemInfo in listItemsInfos)
                listItems.Add(new CreoleListItemElement(listItemInfo.Content));
            return listItems;
        }

        private class ListElementInfo
        {
            internal ListElementInfo(bool isOrdered)
            {
                IsOrdered = isOrdered;
            }

            internal bool IsOrdered { get; }

            internal ICollection<ListItemElementInfo> ItemInfos { get; } = new List<ListItemElementInfo>();
        }

        private class ListItemElementInfo
        {
            internal ListItemElementInfo()
            {
                Content = new List<CreoleElement>();
            }

            internal ListItemElementInfo(IEnumerable<CreoleElement> content)
            {
                Content = new List<CreoleElement>(content);
            }

            internal ICollection<CreoleElement> Content { get; }
        }
    }
}