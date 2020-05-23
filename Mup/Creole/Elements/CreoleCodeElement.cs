using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mup.Creole.Elements
{
    internal class CreoleCodeElement : CreoleElement
    {
        internal CreoleCodeElement(string code)
        {
            Code = code;
        }

        internal string Code { get; }

        internal override Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)
            => visitor.VisitCodeFragmentAsync(Code, cancellationToken);
    }
}