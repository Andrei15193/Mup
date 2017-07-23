using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    public interface IMarkupParser
    {
        IParseResult Parse(string text);

        Task<IParseResult> ParseAsync(string text);

        Task<IParseResult> ParseAsync(string text, CancellationToken cancellationToken);

        Task<IParseResult> ParseAsync(TextReader reader);

        Task<IParseResult> ParseAsync(TextReader reader, CancellationToken cancellationToken);

        Task<IParseResult> ParseAsync(TextReader reader, int bufferSize);

        Task<IParseResult> ParseAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken);
    }
}