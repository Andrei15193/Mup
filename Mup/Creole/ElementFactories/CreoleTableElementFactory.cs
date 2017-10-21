using System.Collections.Generic;
using System.Linq;
using Mup.Creole.Elements;
using static Mup.Creole.CreoleTokenCode;

namespace Mup.Creole.ElementFactories
{
    internal class CreoleTableElementFactory : CreoleRichTextBlockElementFactory
    {
        internal CreoleTableElementFactory(string text, IEnumerable<string> inlineHyperlinkProtocols)
            : base(text, inlineHyperlinkProtocols)
        {
        }

        internal override CreoleFactoryResult TryCreateFrom(CreoleToken token)
        {
            CreoleFactoryResult result = null;

            if (_IsTableBeginning(token))
            {
                var tableStart = token;
                var rows = new List<CreoleTableRowElement>();
                do
                {
                    var cells = new List<CreoleTableCellElement>();
                    var cellStart = token;
                    token = token.Next;
                    while (token.Next != null && !ContainsLineFeed(token.Next))
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

                    if (token.Next?.Next != null)
                        token = token.Next.Next;
                } while (_IsTableRowBeginning(token));

                var tableEnd = (token.Next != null ? token.Previous : token);
                result = new CreoleFactoryResult(tableStart, tableEnd, new CreoleTableElement(rows));
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
            if (contentStart.Code == WhiteSpace)
                contentStart = contentStart.Next;
            if (contentEnd.Code == WhiteSpace)
                contentEnd = contentEnd.Previous;

            if (contentStart.StartIndex > contentEnd.StartIndex)
                return EmptyContent;
            else
                return CreateRichTextElementsFrom(contentStart, contentEnd);
        }

        private bool _IsTableBeginning(CreoleToken token)
            => (token.Code == Pipe && token.Next != null && !ContainsLineFeed(token.Next));

        private bool _IsTableRowBeginning(CreoleToken token)
            => (_IsTableBeginning(token) && (FindLineFeeds(token.Previous).Take(2).Count() == 1));
    }
}