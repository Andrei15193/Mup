﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleListItemElement : CreoleElement
    {
        internal CreoleListItemElement(IEnumerable<CreoleElement> content)
        {
            Content = content;
        }

        internal IEnumerable<CreoleElement> Content { get; }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitListItemBeginning();
            foreach (var element in Content)
                element.Accept(visitor);
            visitor.VisitListItemEnding();
        }
    }
}