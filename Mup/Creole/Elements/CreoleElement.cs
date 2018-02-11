#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal abstract class CreoleElement
    {
#if net20
        internal abstract void Accept(ParseTreeVisitor visitor);
#endif

#if netstandard10
        internal abstract Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken);
#endif
    }
}