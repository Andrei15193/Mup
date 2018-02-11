using System;
using System.IO;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup
{
    /// <summary>A common interface for each markup parser implementation.</summary>
    public interface IMarkupParser
    {
        /// <summary>Parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        IParseTree Parse(string text);

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        IParseTree Parse(TextReader reader);

        /// <summary>Parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        IParseTree Parse(TextReader reader, int bufferSize);

        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        IAsyncResult BeginParse(string text);


        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        IAsyncResult BeginParse(string text, object asyncState);

        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        IAsyncResult BeginParse(string text, AsyncCallback asyncCallback);

        /// <summary>Begins to asynchronously parse the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        IAsyncResult BeginParse(string text, AsyncCallback asyncCallback, object asyncState);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        IAsyncResult BeginParse(TextReader reader);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        IAsyncResult BeginParse(TextReader reader, object asyncState);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        IAsyncResult BeginParse(TextReader reader, AsyncCallback asyncCallback);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        IAsyncResult BeginParse(TextReader reader, AsyncCallback asyncCallback, object asyncState);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        IAsyncResult BeginParse(TextReader reader, int bufferSize);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        IAsyncResult BeginParse(TextReader reader, int bufferSize, object asyncState);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        IAsyncResult BeginParse(TextReader reader, int bufferSize, AsyncCallback asyncCallback);

        /// <summary>Begins to asynchronously parse text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        IAsyncResult BeginParse(TextReader reader, int bufferSize, AsyncCallback asyncCallback, object asyncState);

        /// <summary>Waits for the pending asynchronous parse operation to complete.</summary>
        /// <param name="asyncResult">The pending asynchronous operation to wait for.</param>
        /// <returns>Returns an <see cref="IParseTree"/> that can be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="asyncResult"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="asyncResult"/> was not returned by one of the Begin methods of the current instance.</exception>
        IParseTree EndParse(IAsyncResult asyncResult);

#if netstandard10
        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        Task<IParseTree> ParseAsync(string text);

        /// <summary>Asynchronously parses the given <paramref name="text"/>.</summary>
        /// <param name="text">The text to parse.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
        Task<IParseTree> ParseAsync(string text, CancellationToken cancellationToken);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        Task<IParseTree> ParseAsync(TextReader reader);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        Task<IParseTree> ParseAsync(TextReader reader, CancellationToken cancellationToken);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        Task<IParseTree> ParseAsync(TextReader reader, int bufferSize);

        /// <summary>Asynchronously parses text from the given <paramref name="reader"/>.</summary>
        /// <param name="reader">A text reader from which to parse text.</param>
        /// <param name="bufferSize">The buffer size to use when reading text from the <paramref name="reader"/>.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns an <see cref="IParseTree"/> wrapped in a <see cref="Task{TResult}"/> that can eventually be traversed using a <see cref="ParseTreeVisitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="reader"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="bufferSize"/> is negative or 0 (zero).</exception>
        Task<IParseTree> ParseAsync(TextReader reader, int bufferSize, CancellationToken cancellationToken);
#endif
    }
}