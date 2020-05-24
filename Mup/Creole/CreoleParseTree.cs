using System;
using System.Collections.Generic;
using Mup.Creole.Elements;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole
{
    internal class CreoleParseTree : IParseTree
    {
        private readonly IEnumerable<CreoleElement> _elements;

        internal CreoleParseTree(IEnumerable<CreoleElement> elements)
        {
            _elements = elements;
        }

        public void Accept(ParseTreeVisitor visitor)
        {
            visitor.BeginVisit();
            foreach (var blockElement in _elements)
                blockElement.Accept(visitor);
            visitor.EndVisit();
        }

        public TResult Accept<TResult>(ParseTreeVisitor<TResult> visitor)
        {
            Accept((ParseTreeVisitor)visitor);
            return visitor.GetResult();
        }
    }
}