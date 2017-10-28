using System.Collections.Generic;
using System.Linq;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleListElementFactory : CreoleRichTextBlockElementFactory
    {
        internal CreoleListElementFactory(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken start, CreoleToken end)
        {
            CreoleFactoryResult result = null;

            if ((start.Code == Asterisk || start.Code == Hash) && start.Next != end && start.Next.Code != start.Code)
            {
                var startToken = start;
                var listElementInfos = new Stack<ListElementInfo>();
                var isValidList = true;

                while (isValidList)
                {
                    var listLevel = 0;
                    var itemStart = start;
                    var isOrderedList = (itemStart.Code == Hash);

                    while (start.Next != end && start.Code == itemStart.Code)
                    {
                        listLevel++;
                        start = start.Next;
                    }

                    if (listLevel > listElementInfos.Count + 1)
                    {
                        isValidList = false;
                        do
                            start = start.Previous;
                        while (start.Code != WhiteSpace);
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
                            start = itemStart.Previous;
                        }
                        else
                        {
                            var contentStart = start;

                            var isInListItem = true;
                            while (start.Next != end && isInListItem)
                            {
                                switch (FindLineFeeds(start).Take(2).Count())
                                {
                                    case 1:
                                        if (start.Next.Code != Asterisk && start.Next.Code != Hash)
                                            start = start.Next;
                                        else
                                            isInListItem = false;
                                        break;

                                    case 2:
                                        isInListItem = false;
                                        break;

                                    default:
                                        start = start.Next;
                                        break;
                                }
                            }

                            listElementInfos.Peek().ItemInfos.Add(new ListItemElementInfo(_GetListItemContent(contentStart, start)));

                            if (start.Next != end && (start.Next.Code == Asterisk || start.Next.Code == Hash) && !FindLineFeeds(start).Skip(1).Any())
                            {
                                start = start.Next;
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

                result = new CreoleFactoryResult(startToken, start, _CreateListElement(listElementInfos.Pop()));
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