namespace Mup.Elements
{
    public class TextElement : Element
    {
        public TextElement(string text)
            => Text = text;

        public string Text { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}