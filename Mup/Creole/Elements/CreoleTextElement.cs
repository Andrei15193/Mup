using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleTextElement : CreoleElement
    {
        internal CreoleTextElement(string text)
        {
            Text = text;
        }

        internal string Text { get; }

        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitTextAsync(Text, cancellationToken);
    }
}