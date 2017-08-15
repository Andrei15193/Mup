using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    /// <summary>A common interface for each markup parser implementation.</summary>
    public interface IMarkupParser
    {
        /// <summary>Parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseResult"/> that can be used to generate other formats.</returns>
        IParseResult Parse(string text);

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseResult"/> that can be used to generate other formats.</returns>
        Task<IParseResult> ParseAsync(string text);

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseResult"/> that can be used to generate other formats.</returns>
        Task<IParseResult> ParseAsync(string text, CancellationToken cancellationToken);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseResult"/> that can be used to generate other formats.</returns>
        Task<IParseResult> ParseAsync(TextReader reader);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseResult"/> that can be used to generate other formats.</returns>
        Task<IParseResult> ParseAsync(TextReader reader, CancellationToken cancellationToken);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <returns>Returns an <see cref="IParseResult"/> that can be used to generate other formats.</returns>
        Task<IParseResult> ParseAsync(TextReader reader, int bufferSize);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the reader.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseResult"/> that can be used to generate other formats.</returns>
        Task<IParseResult> ParseAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken);
    }
}