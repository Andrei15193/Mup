using System.Collections.Generic;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleListElementProcessor : CreoleElementProcessor
    {
        private enum State
        {
            NotInListItem,
            EmptyListItemLine,
            InListItemLevel,
            InListItemWhiteSpace,
            InListItem,
            ListItemMayEndInWhiteSpace,
            ListItemMayEnd
        }

        private State _state = State.NotInListItem;
        private int _listItemStartIndex;
        private int _listItemEndIndex;
        private int _listItemContentStartIndex;
        private int _listItemLevel;
        private bool _listItemIsOrdered;
        private Stack<CreoleListInfo> _listInfos = new Stack<CreoleListInfo>();

        public CreoleListElementProcessor(CreoleParserContext context, CreoleTokenRange tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NotInListItem:
                    if (IsOnNewLine)
                    {
                        if (Token.Code == Asterisk)
                        {
                            _listItemIsOrdered = false;
                            _SetItemStartIndex();
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else if (Token.Code == Hash)
                        {
                            _listItemIsOrdered = true;
                            _SetItemStartIndex();
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                    }
                    else if ((Token.Code == NewLine || Token.Code == BlankLine) && _listInfos.Count > 0)
                        SetResult(_GetTopList());
                    break;

                case State.EmptyListItemLine:
                    if (_listInfos.Count > 0)
                        SetResult(_GetTopList());

                    if (IsOnNewLine)
                        if (Token.Code == Asterisk)
                        {
                            _listItemIsOrdered = false;
                            _SetItemStartIndex();
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else if (Token.Code == Hash)
                        {
                            _listItemIsOrdered = true;
                            _SetItemStartIndex();
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
                        if (_listInfos.Count == 1 && _listItemLevel == 1 && _listInfos.Peek().IsOrdered != _listItemIsOrdered)
                            SetResult(_PopList());

                        if (Token.Code == BlankLine)
                        {
                            _listItemEndIndex = Index;
                            _listItemContentStartIndex = Index;
                            _ProcessListItem();
                            _state = State.EmptyListItemLine;
                        }
                        else if (Token.Code == NewLine)
                        {
                            _listItemEndIndex = Index;
                            _state = State.ListItemMayEnd;
                        }
                        else if (Token.Code == WhiteSpace)
                            _state = State.InListItemWhiteSpace;
                        else
                        {
                            _listItemContentStartIndex = Index;
                            _state = State.InListItem;
                        }
                    }
                    break;

                case State.InListItemWhiteSpace:
                    _listItemContentStartIndex = Index;
                    _state = State.InListItem;
                    break;

                case State.InListItem:
                    _listItemEndIndex = Index;
                    if (Token.Code == BlankLine)
                    {
                        _ProcessListItem();
                        _state = State.EmptyListItemLine;
                    }
                    else if (Token.Code == NewLine)
                        _state = State.ListItemMayEnd;
                    else if (Token.Code == WhiteSpace)
                        _state = State.ListItemMayEndInWhiteSpace;
                    break;

                case State.ListItemMayEndInWhiteSpace:
                    _state = State.InListItem;
                    break;

                case State.ListItemMayEnd:
                    if (Token.Code == Asterisk || Token.Code == Hash)
                    {
                        _ProcessListItem();

                        if (Token.Code == Asterisk)
                        {
                            _listItemIsOrdered = false;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else if (Token.Code == Hash)
                        {
                            _listItemIsOrdered = true;
                            _listItemLevel = 1;
                            _state = State.InListItemLevel;
                        }
                        else
                        {
                            _listItemLevel = 0;
                            _state = State.NotInListItem;
                        }
                    }
                    else
                        _state = State.InListItem;
                    break;
            }
        }

        protected override void Complete()
        {
            if (_state == State.InListItemLevel || _state == State.InListItemWhiteSpace)
            {
                _listItemContentStartIndex = Index;
                _listItemEndIndex = Index;
                _ProcessListItem();
            }
            else if (_state == State.InListItem)
            {
                _listItemEndIndex = Index;
                _ProcessListItem();
            }
            if (_state == State.ListItemMayEndInWhiteSpace)
                _ProcessListItem();

            if (_listInfos.Count > 0)
                SetResult(_GetTopList());
        }

        private void _SetItemStartIndex()
        {
            _listItemStartIndex = Index;
            if (_listInfos.Count == 0)
                SetElementStartIndex();
        }

        private void _ProcessListItem()
        {
            if (_listItemLevel == 0)
            {
                if (_listInfos.Count > 0)
                    SetResult(_GetTopList());
            }
            else if (_listInfos.Count == 0 && _listItemLevel == 1)
            {
                SetElementEndIndex();
                var listInfo = new CreoleListInfo(_listItemIsOrdered);
                listInfo.AddItem(GetRichText(_listItemContentStartIndex, _listItemEndIndex));
                _listInfos.Push(listInfo);
            }
            else if (_listItemLevel == _listInfos.Count + 1)
            {
                SetElementEndIndex();
                var listInfo = new CreoleListInfo(_listItemIsOrdered);
                listInfo.AddItem(GetRichText(_listItemContentStartIndex, _listItemEndIndex));
                _listInfos.Push(listInfo);
            }
            else if (_listItemLevel <= _listInfos.Count)
            {
                while (_listItemLevel < _listInfos.Count)
                    _PopList();

                if (_listInfos.Peek().IsOrdered != _listItemIsOrdered)
                {
                    _PopList();

                    SetElementEndIndex();
                    var listInfo = new CreoleListInfo(_listItemIsOrdered);
                    listInfo.AddItem(GetRichText(_listItemContentStartIndex, _listItemEndIndex));
                    _listInfos.Push(listInfo);
                }
                else
                {
                    SetElementEndIndex();
                    _listInfos.Peek().AddItem(GetRichText(_listItemContentStartIndex, _listItemEndIndex));
                }
            }
            else if (_listInfos.Count > 0)
                SetResult(_GetTopList());
            SetElementEndIndex();
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