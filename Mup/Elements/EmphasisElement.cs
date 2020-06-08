using System.Collections.Generic;

namespace Mup.Elements
{
    public class EmphasisElement : Element
    {
        public EmphasisElement(IEnumerable<Element> content)
            => Content = content;

        public IEnumerable<Element> Content { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}