using System.Collections.Generic;

namespace Mup.Elements
{
    public class HyperlinkElement : Element
    {
        public HyperlinkElement(string destination, IEnumerable<Element> content)
        {
            Destination = destination;
            Content = content;
        }

        public string Destination { get; }

        public IEnumerable<Element> Content { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}