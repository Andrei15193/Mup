namespace Mup.Elements
{
    /// <summary>Represents an inline code snippet.</summary>
    public class CodeElement : Element
    {
        /// <summary>Initializes a new instance of the <see cref="CodeElement"/> class.</summary>
        /// <param name="code">The code snippet represented by the new instance.</param>
        public CodeElement(string code)
            => Code = code;

        /// <summary>The code snippet represented by the current instace.</summary>
        public string Code { get; }

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor used to traverse the parse tree.</param>
        /// <exception cref="System.NullReferenceException">Thrown when <paramref name="visitor"/> is <c>null</c>.</exception>
        public override void Accept(ParseTreeVisitor visitor)
            => visitor.Visit(this);
    }
}