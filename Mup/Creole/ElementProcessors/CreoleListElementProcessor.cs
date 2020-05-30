using Mup.Creole.Elements;
using Mup.Scanner;
using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleListElementProcessor : CreoleElementProcessor
    {
        private enum State
        {
            NotInListItem,
            ClearListInfos,
            InListItemLevel,
            ListItemNewLineContentStart,
            ListItemWhiteSpaceContentStart,
            ListItemContent,
            ListItemMayEndInWhiteSpace,
            ListItemMayEnd
        }

        private State _state = State.NotInListItem;
        private bool _listItemIsOrdered;
        private int _listItemContentStartIndex;
        private int _listItemEndIndex;
        private int _listItemLevel;
        private Stack<CreoleListInfo> _listInfos = new Stack<CreoleListInfo>();

        public CreoleListElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NotInListItem when (IsOnNewLine):
                    if (Token.Code == Asterisk)
                    {
                        if (_listInfos.Count == 0)
                            SetElementStartIndex();
                        _listItemIsOrdered = false;
                        _listItemLevel = 1;
                        _state = State.InListItemLevel;
                    }
                    else if (Token.Code == Hash)
                    {
                        if (_listInfos.Count == 0)
                            SetElementStartIndex();
                        _listItemIsOrdered = true;
                        _listItemLevel = 1;
                        _state = State.InListItemLevel;
                    }
                    break;

                case State.ClearListInfos:
                    if (_listInfos.Count > 0)
                        SetResult(_GetTopList());

                    if (IsOnNewLine)
                        if (Token.Code == Asterisk)
                        {
                            SetElementStartIndex();
                            _listItemIsOrdered = false;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else if (Token.Code == Hash)
                        {
                            SetElementStartIndex();
                            _listItemIsOrdered = true;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else
                            _state = State.NotInListItem;
                    else
                        _state = State.NotInListItem;
                    break;

                case State.InListItemLevel:
                    if ((!_listItemIsOrdered && Token.Code == Asterisk) || (_listItemIsOrdered && Token.Code == Hash))
                        _listItemLevel++;
                    else
                    {
                        if (_listInfos.Count + 1 < _listItemLevel)
                        {
                            if (_listInfos.Count > 0)
                                SetResult(_GetTopList());
                            _state = State.NotInListItem;
                        }
                        else
                        {
                            while (_listInfos.Count > _listItemLevel)
                                _PopList();

                            if (_listInfos.Count == _listItemLevel && _listInfos.Peek().IsOrdered != _listItemIsOrdered)
                                if (_listInfos.Count == 1) if (_listInfos.Count == 1)
                                        SetResult(_PopList());
                                    else
                                        _PopList();

                            if (Token.Code == BlankLine)
                            {
                                SetElementEndIndex();
                                var richText = GetRichText(Index, Index);
                                if (_listInfos.Count < _listItemLevel)
                                {
                                    var listInfo = new CreoleListInfo(_listItemIsOrdered);
                                    listInfo.AddItem(richText);
                                    _listInfos.Push(listInfo);
                                }
                                else
                                    _listInfos.Peek().AddItem(richText);

                                _state = State.ClearListInfos;
                            }
                            else if (Token.Code == NewLine)
                                _state = State.ListItemNewLineContentStart;
                            else if (Token.Code == WhiteSpace)
                                _state = State.ListItemWhiteSpaceContentStart;
                            else
                            {
                                _listItemContentStartIndex = Index;
                                _state = State.ListItemContent;
                            }
                        }
                    }
                    break;

                case State.ListItemNewLineContentStart:
                    if (Token.Code == Asterisk || Token.Code == Hash)
                    {
                        SetElementEndIndex();
                        var richText = GetRichText(Index, Index);
                        if (_listInfos.Count < _listItemLevel)
                        {
                            var listInfo = new CreoleListInfo(_listItemIsOrdered);
                            listInfo.AddItem(richText);
                            _listInfos.Push(listInfo);
                        }
                        else
                            _listInfos.Peek().AddItem(richText);

                        if (Token.Code == Asterisk)
                        {
                            if (_listInfos.Count == 0)
                                SetElementStartIndex();
                            _listItemIsOrdered = false;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else if (Token.Code == Hash)
                        {
                            if (_listInfos.Count == 0)
                                SetElementStartIndex();
                            _listItemIsOrdered = true;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else
                        {
                            _listItemContentStartIndex = Index;
                            _state = State.ListItemContent;
                        }
                    }
                    else
                    {
                        _listItemContentStartIndex = Index;
                        _state = State.ListItemContent;
                    }
                    break;

                case State.ListItemWhiteSpaceContentStart:
                    _listItemContentStartIndex = Index;
                    _state = State.ListItemContent;
                    break;

                case State.ListItemContent:
                    if (Token.Code == BlankLine)
                    {
                        _listItemEndIndex = Index;
                        SetElementEndIndex();
                        var richText = GetRichText(_listItemContentStartIndex, _listItemEndIndex);
                        if (_listInfos.Count < _listItemLevel)
                        {
                            var listInfo = new CreoleListInfo(_listItemIsOrdered);
                            listInfo.AddItem(richText);
                            _listInfos.Push(listInfo);
                        }
                        else
                            _listInfos.Peek().AddItem(richText);
                        SetResult(_GetTopList());

                        _state = State.NotInListItem;
                    }
                    else if (Token.Code == NewLine)
                    {
                        _listItemEndIndex = Index;
                        SetElementEndIndex();

                        _state = State.ListItemMayEnd;
                    }
                    else if (Token.Code == WhiteSpace)
                    {
                        _listItemEndIndex = Index;
                        SetElementEndIndex();

                        _state = State.ListItemMayEndInWhiteSpace;
                    }
                    break;

                case State.ListItemMayEndInWhiteSpace:
                    _state = State.ListItemContent;
                    break;

                case State.ListItemMayEnd:
                    if (Token.Code == Asterisk || Token.Code == Hash)
                    {
                        var richText = GetRichText(_listItemContentStartIndex, _listItemEndIndex);
                        if (_listInfos.Count < _listItemLevel)
                        {
                            var listInfo = new CreoleListInfo(_listItemIsOrdered);
                            listInfo.AddItem(richText);
                            _listInfos.Push(listInfo);
                        }
                        else
                            _listInfos.Peek().AddItem(richText);

                        if (Token.Code == Asterisk)
                        {
                            if (_listInfos.Count == 0)
                                SetElementStartIndex();
                            _listItemIsOrdered = false;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else if (Token.Code == Hash)
                        {
                            if (_listInfos.Count == 0)
                                SetElementStartIndex();
                            _listItemIsOrdered = true;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else
                        {
                            SetResult(_GetTopList());
                            _listItemLevel = 0;
                            _state = State.NotInListItem;
                        }
                    }
                    else
                        _state = State.ListItemContent;
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.InListItemLevel || _state == State.ListItemWhiteSpaceContentStart || _state == State.ListItemNewLineContentStart)
            {
                SetElementEndIndex();
                var richText = GetRichText(Index, Index);
                if (_listInfos.Count < _listItemLevel)
                {
                    var listInfo = new CreoleListInfo(_listItemIsOrdered);
                    listInfo.AddItem(richText);
                    _listInfos.Push(listInfo);
                }
                else
                    _listInfos.Peek().AddItem(richText);
            }
            else if (_state == State.ListItemContent)
            {
                _listItemEndIndex = Index;
                SetElementEndIndex();
                var richText = GetRichText(_listItemContentStartIndex, _listItemEndIndex);
                if (_listInfos.Count < _listItemLevel)
                {
                    var listInfo = new CreoleListInfo(_listItemIsOrdered);
                    listInfo.AddItem(richText);
                    _listInfos.Push(listInfo);
                }
                else
                    _listInfos.Peek().AddItem(richText);
            }
            else if (_state == State.ListItemMayEnd || _state == State.ListItemMayEndInWhiteSpace)
            {
                var richText = GetRichText(_listItemContentStartIndex, _listItemEndIndex);
                if (_listInfos.Count < _listItemLevel)
                {
                    var listInfo = new CreoleListInfo(_listItemIsOrdered);
                    listInfo.AddItem(richText);
                    _listInfos.Push(listInfo);
                }
                else
                    _listInfos.Peek().AddItem(richText);
            }

            if (_listInfos.Count > 0)
                SetResult(_GetTopList());
        }

        private CreoleListElement _GetTopList()
        {
            while (_listInfos.Count > 1)
                _PopList();
            return _PopList();
        }

        private CreoleListElement _PopList()
        {
            var list = _GetListFrom(_listInfos.Pop());
            if (_listInfos.Count > 0)
                _listInfos.Peek().LastItem.Elements.Add(list);

            return list;
        }

        private static CreoleListElement _GetListFrom(CreoleListInfo listInfo)
        {
            var listItems = new List<CreoleListItemElement>();
            foreach (var listItemInfo in listInfo.ListItems)
                listItems.Add(new CreoleListItemElement(listItemInfo.Elements));

            if (listInfo.IsOrdered)
                return new CreoleOrderedListElement(listItems);
            else
                return new CreoleUnorderedListElement(listItems);
        }

        private class CreoleListInfo
        {
            private readonly LinkedList<CreoleListItemInfo> _listItems;

            internal CreoleListInfo(bool isOrdered)
            {
                IsOrdered = isOrdered;
                _listItems = new LinkedList<CreoleListItemInfo>();
            }

            internal bool IsOrdered { get; }

            internal IEnumerable<CreoleListItemInfo> ListItems
                => _listItems;

            internal CreoleListItemInfo LastItem
                => _listItems.Last.Value;

            internal CreoleListItemInfo AddItem(IEnumerable<CreoleElement> elements)
            {
                var item = new CreoleListItemInfo(elements);
                _listItems.AddLast(item);
                return item;
            }
        }

        private class CreoleListItemInfo
        {
            internal CreoleListItemInfo(IEnumerable<CreoleElement> elements)
            {
                Elements = new List<CreoleElement>(elements);
            }

            internal ICollection<CreoleElement> Elements { get; }
        }
    }
}