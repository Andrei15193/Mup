using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleParagraphElement : CreoleElement
    {
        private readonly IEnumerable<CreoleElement> _richTextElements;

        public CreoleParagraphElement(IEnumerable<CreoleElement> richTextElements)
        {
            _richTextElements = richTextElements;
        }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitParagraphBeginning();
            foreach (var richTextElement in _richTextElements)
                richTextElement.Accept(visitor);
            visitor.VisitParagraphEnding();
        }
    }
}