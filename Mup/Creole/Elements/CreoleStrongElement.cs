﻿using System.Collections.Generic;

namespace Mup.Creole.Elements
{
    internal class CreoleStrongElement : CreoleElement
    {
        internal CreoleStrongElement(IEnumerable<CreoleElement> children)
        {
            Children = children;
        }

        internal IEnumerable<CreoleElement> Children { get; }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitStrongBeginning();
            foreach (var child in Children)
                child.Accept(visitor);
            visitor.VisitStrongEnding();
        }
    }
}