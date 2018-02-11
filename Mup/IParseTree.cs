﻿using System;
#if netstandard10
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Mup
{
    /// <summary>A common interface for the result of any markup parser.</summary>
    public interface IParseTree
    {
        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        void Accept(ParseTreeVisitor visitor);

        /// <summary>Accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <returns>Returns the result generated by the <paramref name="visitor"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        TResult Accept<TResult>(ParseTreeVisitor<TResult> visitor);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept(ParseTreeVisitor visitor);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept(ParseTreeVisitor visitor, object asyncState);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept(ParseTreeVisitor visitor, AsyncCallback asyncCallback);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept(ParseTreeVisitor visitor, AsyncCallback asyncCallback, object asyncState);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor, object asyncState);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor, AsyncCallback asyncCallback);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="asyncCallback">A callback to invoke when the asynchronous operation completes.</param>
        /// <param name="asyncState">A state object to associate with the resulting <see cref="IAsyncResult"/>.</param>
        /// <returns>Returns an <see cref="IAsyncResult"/> that can used to wait for the asynchronous operation to complete.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        IAsyncResult BeginAccept<TResult>(ParseTreeVisitor<TResult> visitor, AsyncCallback asyncCallback, object asyncState);

        /// <summary>Waits for the pending asynchronous parse operation to complete.</summary>
        /// <param name="asyncResult">The pending asynchronous operation to wait for.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="asyncResult"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="asyncResult"/> was not returned by one of the Begin methods of the current instance.</exception>
        void EndAccept(IAsyncResult asyncResult);


        /// <summary>Waits for the pending asynchronous parse operation to complete.</summary>
        /// <typeparam name="TResult">The result type that the visitor generates.</typeparam>
        /// <param name="asyncResult">The pending asynchronous operation to wait for.</param>
        /// <returns>Returns the result generated by the visitor.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="asyncResult"/> is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <paramref name="asyncResult"/> was not returned by one of the Begin methods of the current instance.</exception>
        TResult EndAccept<TResult>(IAsyncResult asyncResult);

#if netstandard10
        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AcceptAsync(ParseTreeVisitor visitor);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        /// <returns>Returns a <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <returns>Returns the result generated by the <paramref name="visitor"/> wrapped in a <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor);

        /// <summary>Asynchronously accepts a <paramref name="visitor"/> which can be used to generate output from the parse tree.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns the result generated by the <paramref name="visitor"/> wrapped in a <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="visitor"/> is null.</exception>
        Task<TResult> AcceptAsync<TResult>(ParseTreeVisitor<TResult> visitor, CancellationToken cancellationToken);
#endif
    }
}