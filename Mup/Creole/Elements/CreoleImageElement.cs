namespace Mup.Creole.Elements
{
    internal class CreoleImageElement : CreoleElement
    {
        internal CreoleImageElement(string source, string alternative)
        {
            Source = source;
            AlternativeText = alternative;
        }

        internal CreoleImageElement(string source)
        {
            Source = source;
            AlternativeText = null;
        }

        internal string Source { get; }

        internal string AlternativeText { get; }

        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitImage(Source, AlternativeText);
    }
}