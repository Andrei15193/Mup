using Mup.Elements;
using Mup.Scanner;
using System.Collections.Generic;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementProcessors
{
    internal class CreoleTableElementProcessor : CreoleElementProcessor
    {
        private enum State : byte
        {
            NotInTable,
            InTableCellStart,
            InTableCellWhiteSpaceStart,
            InCellContent,
            TableMayEnd,
            CellContentMayEnd
        }

        private State _state = State.NotInTable;
        private int _cellStartIndex;
        private int _cellEndIndex;
        private bool _isHeaderCell;
        private List<TableRowElement> _rows;
        private List<TableCellElement> _cells;

        internal CreoleTableElementProcessor(CreoleParserContext context, TokenRange<CreoleTokenCode> tokens)
            : base(context, tokens)
        {
        }

        protected override void Process()
        {
            switch (_state)
            {
                case State.NotInTable when (Token.Code == Pipe && IsOnNewLine):
                    SetElementStartIndex();
                    _rows = new List<TableRowElement>();
                    _cells = new List<TableCellElement>();
                    _isHeaderCell = false;
                    _state = State.InTableCellStart;
                    break;

                case State.InTableCellStart:
                    if (Token.Code == Equal)
                        _isHeaderCell = true;
                    else if (Token.Code == Pipe)
                    {
                        _cellStartIndex = Index;
                        _cellEndIndex = Index;
                        _cells.Add(_GetTableCellElement());
                    }
                    else if (Token.Code == NewLine)
                    {
                        if (_isHeaderCell || _cells.Count == 0)
                        {
                            _cellStartIndex = Index;
                            _cellEndIndex = Index;
                            _cells.Add(_GetTableCellElement());
                        }
                        _rows.Add(new TableRowElement(_cells));
                        _cells = null;
                        _state = State.TableMayEnd;
                    }
                    else if (Token.Code == BlankLine)
                    {
                        if (_isHeaderCell || _cells.Count == 0)
                        {
                            _cellStartIndex = Index;
                            _cellEndIndex = Index;
                            _cells.Add(_GetTableCellElement());
                        }
                        _rows.Add(new TableRowElement(_cells));
                        SetElementEndIndex();
                        SetResult(new TableElement(_rows));

                        _rows = null;
                        _cells = null;
                        _state = State.NotInTable;
                    }
                    else if (Token.Code == WhiteSpace)
                        _state = State.InTableCellWhiteSpaceStart;
                    else
                    {
                        _cellStartIndex = Index;
                        _state = State.InCellContent;
                    }
                    break;

                case State.InTableCellWhiteSpaceStart:
                    _cellStartIndex = Index;
                    if (Token.Code == Pipe)
                    {
                        _cellEndIndex = Index;
                        _cells.Add(_GetTableCellElement());
                        _isHeaderCell = false;
                        _state = State.InTableCellStart;
                    }
                    else
                        _state = State.InCellContent;
                    break;

                case State.InCellContent:
                    if (Token.Code == Pipe)
                    {
                        _cellEndIndex = Index;
                        _cells.Add(_GetTableCellElement());
                        _isHeaderCell = false;
                        _state = State.InTableCellStart;
                    }
                    else if (Token.Code == NewLine)
                    {
                        _cellEndIndex = Index;
                        _cells.Add(_GetTableCellElement());
                        _rows.Add(new TableRowElement(_cells));
                        _cells = null;
                        _state = State.TableMayEnd;
                    }
                    else if (Token.Code == BlankLine)
                    {
                        _cellEndIndex = Index;
                        _cells.Add(_GetTableCellElement());
                        _rows.Add(new TableRowElement(_cells));
                        SetElementEndIndex();
                        SetResult(new TableElement(_rows));

                        _rows = null;
                        _cells = null;
                        _state = State.NotInTable;
                    }
                    else if (Token.Code == WhiteSpace)
                    {
                        _cellEndIndex = Index;
                        _state = State.CellContentMayEnd;
                    }
                    break;

                case State.CellContentMayEnd:
                    if (Token.Code == Pipe)
                    {
                        _cells.Add(_GetTableCellElement());
                        _isHeaderCell = false;
                        _state = State.InTableCellStart;
                    }
                    else
                        _state = State.InCellContent;
                    break;

                case State.TableMayEnd:
                    if (Token.Code == Pipe)
                    {
                        _cells = new List<TableCellElement>();
                        _isHeaderCell = false;
                        _state = State.InTableCellStart;
                    }
                    else
                    {
                        SetElementEndIndex();
                        SetResult(new TableElement(_rows));

                        _rows = null;
                        _cells = null;
                        _state = State.NotInTable;
                    }
                    break;
            }
        }

        protected override void Complete()
        {
            switch (_state)
            {
                case State.InTableCellStart:
                    if (_cells.Count == 0)
                    {
                        _cellStartIndex = Index;
                        _cellEndIndex = Index;
                        _cells.Add(_GetTableCellElement());
                    }
                    SetElementEndIndex();
                    _rows.Add(new TableRowElement(_cells));
                    SetResult(new TableElement(_rows));
                    break;

                case State.InTableCellWhiteSpaceStart:
                    _cellStartIndex = Index;
                    _cellEndIndex = Index;
                    _cells.Add(_GetTableCellElement());
                    _rows.Add(new TableRowElement(_cells));
                    SetElementEndIndex();
                    SetResult(new TableElement(_rows));
                    break;

                case State.InCellContent:
                    _cellEndIndex = Index;
                    _cells.Add(_GetTableCellElement());
                    _rows.Add(new TableRowElement(_cells));
                    SetElementEndIndex();
                    SetResult(new TableElement(_rows));
                    break;

                case State.CellContentMayEnd:
                    _cells.Add(_GetTableCellElement());
                    _rows.Add(new TableRowElement(_cells));
                    SetElementEndIndex();
                    SetResult(new TableElement(_rows));
                    break;

                case State.TableMayEnd:
                    SetElementEndIndex();
                    SetResult(new TableElement(_rows));
                    break;
            }
        }

        private TableCellElement _GetTableCellElement()
        {
            SetElementEndIndex();
            var richText = GetRichText(_cellStartIndex, _cellEndIndex);
            if (_isHeaderCell)
                return new TableHeaderCellElement(richText);
            else
                return new TableDataCellElement(richText);
        }
    }
}