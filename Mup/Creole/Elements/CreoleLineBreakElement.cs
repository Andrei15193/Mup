﻿using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal sealed class CreoleLineBreakElement : CreoleElement
    {
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitLineBreak();
    }
}