namespace Mup.Elements
{
    public class ImageElement : Element
    {
        public ImageElement(string source)
            : this(source, null)
        {
        }

        public ImageElement(string source, string alternative)
        {
            Source = source;
            AlternativeText = alternative;
        }

        public string Source { get; }

        public string AlternativeText { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}