#if netstandard10
using System;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleTextElement : CreoleElement
    {
        internal CreoleTextElement(string text)
        {
            Text = text;
        }

        internal string Text { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
            => visitor.VisitText(Text);
#endif

#if netstandard10
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitTextAsync(Text, cancellationToken);
#endif
    }
}