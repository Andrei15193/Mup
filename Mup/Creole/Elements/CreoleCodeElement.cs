#if netstandard10
using System;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup.Creole.Elements
{
    internal class CreoleCodeElement : CreoleElement
    {
        internal CreoleCodeElement(string code)
        {
            Code = code;
        }

        internal string Code { get; }

#if net20
        internal override void Accept(ParseTreeVisitor visitor)
        {
            visitor.VisitCodeFragment(Code);
        }
#endif

#if netstandard10
        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitCodeFragmentAsync(Code, cancellationToken);
#endif
    }
}