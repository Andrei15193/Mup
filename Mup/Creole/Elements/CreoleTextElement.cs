namespace Mup.Creole.Elements
{
    internal class CreoleTextElement : CreoleElement
    {
        internal CreoleTextElement(string text)
        {
            Text = text;
        }

        internal string Text { get; }

        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitText(Text);
    }
}