namespace Mup.Elements
{
    public class CodeElement : Element
    {
        public CodeElement(string code)
            => Code = code;

        public string Code { get; }

        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}