﻿using System.Threading;
using System.Threading.Tasks;

namespace Mup
{
    /// <summary>A common interface for the result of all markup parsers.</summary>
    public interface IParseResult
    {
        /// <summary>The text that was parsed.</summary>
        string Text { get; }

        /// <summary>Asynchronously acceps a <paramref name="visitor"/> which can be used to generate output from the parse result.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        Task AcceptAsync(ParseResultVisitor visitor);

        /// <summary>Asynchronously acceps a <paramref name="visitor"/> which can be used to generate output from the parse result.</summary>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        Task AcceptAsync(ParseResultVisitor visitor, CancellationToken cancellationToken);

        /// <summary>Asynchronously acceps a <paramref name="visitor"/> which can be used to generate output from the parse result.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <returns>Returns the result generated by the <paramref name="visitor"/>.</returns>
        Task<TResult> AcceptAsync<TResult>(ParseResultVisitor<TResult> visitor);

        /// <summary>Asynchronously acceps a <paramref name="visitor"/> which can be used to generate output from the parse result.</summary>
        /// <typeparam name="TResult">The result type that the <paramref name="visitor"/> generates.</typeparam>
        /// <param name="visitor">The visitor that will traverse the parse tree.</param>
        /// <param name="cancellationToken">A token that can be used to signal a cancellation request.</param>
        /// <returns>Returns the result generated by the <paramref name="visitor"/>.</returns>
        Task<TResult> AcceptAsync<TResult>(ParseResultVisitor<TResult> visitor, CancellationToken cancellationToken);
    }
}