using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    public interface IParseResult
    {
        string Text { get; }

        Task AcceptAsync(ParseResultVisitor visitor);

        Task AcceptAsync(ParseResultVisitor visitor, CancellationToken cancellationToken);
    }
}