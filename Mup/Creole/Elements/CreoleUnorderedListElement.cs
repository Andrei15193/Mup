﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleUnorderedListElement : CreoleListElement
    {
        internal CreoleUnorderedListElement(IEnumerable<CreoleListItemElement> items)
            : base(items)
        {
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitUnorderedListBeginning();
            foreach (var item in Items)
                item.Accept(visitor);
            visitor.VisitUnorderedListEnding();
        }
    }
}