using System.Collections.Generic;

namespace Mup.Creole.Elements
{
    internal class CreoleHyperlinkElement : CreoleElement
    {
        internal CreoleHyperlinkElement(string destination, IEnumerable<CreoleElement> content)
        {
            Destination = destination;
            Content = content;
        }

        internal string Destination { get; }

        internal IEnumerable<CreoleElement> Content { get; }

        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitHyperlinkBeginning(Destination);
            foreach (var element in Content)
                element.Accept(visitor);
            visitor.VisitHyperlinkEnding();
        }
    }
}