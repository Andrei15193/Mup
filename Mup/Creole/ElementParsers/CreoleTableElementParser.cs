using System.Collections.Generic;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementParsers
{
    internal class CreoleTableElementParser : CreoleRichTextBlockElementParser
    {
        internal CreoleTableElementParser(CreoleParserContext context)
            : base(context)
        {
        }

        internal override CreoleElementParserResult TryCreateFrom(CreoleToken start, CreoleToken end)
        {
            CreoleElementParserResult result = null;

            if (_IsTableBeginning(start, end))
            {
                var token = start;
                var tableStart = token;
                var tableEnd = token;
                var rows = new List<CreoleTableRowElement>();
                do
                {
                    var cells = new List<CreoleTableCellElement>();
                    var cellStart = token;
                    token = token.Next;
                    while (token.Next != end && !ContainsLineFeed(token.Next))
                    {
                        if (token.Code == Pipe)
                        {
                            cells.Add(_CreateCell(cellStart, token));
                            cellStart = token;
                        }

                        token = token.Next;
                    }
                    cells.Add(_CreateCell(cellStart, token));
                    rows.Add(new CreoleTableRowElement(cells));

                    tableEnd = token;
                    if (token.Next?.Next != end)
                        token = token.Next.Next;
                } while (_IsTableRowBeginning(token, end));
                result = new CreoleElementParserResult(tableStart, tableEnd, new CreoleTableElement(rows));
            }
            else if (start.Code == Pipe && start.Next == null)
            {
                var cells = new[] { new CreoleTableDataCellElement(EmptyContent) };
                var rows = new[] { new CreoleTableRowElement(cells) };
                var table = new CreoleTableElement(rows);
                result = new CreoleElementParserResult(start, start, table);
            }

            return result;
        }

        private CreoleTableCellElement _CreateCell(CreoleToken cellStart, CreoleToken cellEnd)
        {
            CreoleTableCellElement cell;
            if (cellEnd.Code == Pipe)
                cellEnd = cellEnd.Previous;
            if (cellStart.Next.Code == Equal)
            {
                var content = _GetCellContent(cellStart.Next.Next, cellEnd);
                cell = new CreoleTableHeaderCellElement(content);
            }
            else
            {
                var content = _GetCellContent(cellStart.Next, cellEnd);
                cell = new CreoleTableDataCellElement(content);
            }
            return cell;
        }

        private IEnumerable<CreoleElement> _GetCellContent(CreoleToken contentStart, CreoleToken contentEnd)
        {
            if (contentStart == null)
                return EmptyContent;

            if (contentStart.Code == WhiteSpace && contentStart.Next != null)
                contentStart = contentStart.Next;
            if (contentEnd.Code == WhiteSpace)
                contentEnd = contentEnd.Previous;

            if (contentStart.StartIndex > contentEnd.StartIndex)
                return EmptyContent;
            else
                return CreateRichTextElementsFrom(contentStart, contentEnd);
        }

        private bool _IsTableBeginning(CreoleToken token, CreoleToken end)
            => (token.Code == Pipe && token.Next != end && !ContainsLineFeed(token.Next));

        private bool _IsTableRowBeginning(CreoleToken token, CreoleToken end)
            => (_IsTableBeginning(token, end) && HasOneElement(FindLineFeeds(token.Previous)));
    }
}