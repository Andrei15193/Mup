namespace Mup.Elements
{
    public class PreformattedBlockElement : Element
    {
        public PreformattedBlockElement(string preformattedText)
            => PreformattedText = preformattedText;

        public string PreformattedText { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}