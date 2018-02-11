#if netstandard10
using System;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleBlankElement : CreoleElement
    {
#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
        }
#endif

#if netstandard10
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => Task.FromResult<object>(null);
#endif
    }
}