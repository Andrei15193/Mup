namespace Mup.Elements
{
    public class Heading3Element : HeadingElement
    {
        public Heading3Element(string text)
            : base(text)
        {
        }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}