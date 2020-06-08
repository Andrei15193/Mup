namespace Mup.Elements
{
    public class Heading6Element : HeadingElement
    {
        public Heading6Element(string text)
            : base(text)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}