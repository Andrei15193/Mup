namespace Mup.Creole.Elements
{
    internal class CreolePreformattedBlockElement : CreoleElement
    {
        internal CreolePreformattedBlockElement(string preformattedText)
        {
            PreformattedText = preformattedText;
        }

        internal string PreformattedText { get; }

        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitPreformattedBlock(PreformattedText);
    }
}